﻿using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades;
using DataAccessLayer.Enums;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace PresentationLayer.Controllers
{
    public class JobApplicationController : Controller
    {
        public const int PageSize = 9;

        private const string FilterSessionKey = "filter";

        private JobApplicationFacade jobApplicationFacade;
        private JobOfferFacade jobOfferFacade;
        private JobseekerFacade jobseekerFacade;
        private CompanyFacade companyFacade;

        public JobApplicationController(JobApplicationFacade jobApplicationFacade, JobOfferFacade jobOfferFacade, JobseekerFacade jobseekerFacade, CompanyFacade companyFacade)
        {
            this.jobApplicationFacade = jobApplicationFacade;
            this.jobOfferFacade = jobOfferFacade;
            this.jobseekerFacade = jobseekerFacade;
            this.companyFacade = companyFacade;
        }

        public async Task<ActionResult> Index(int page = 1)
        {
            JobApplicationFilterDTO filter;
            if (User.IsInRole("Admin"))
                filter = Session[FilterSessionKey] as JobApplicationFilterDTO ?? new JobApplicationFilterDTO { PageSize = PageSize };
            else
            {
                var user = await companyFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
                filter = Session[FilterSessionKey] as JobApplicationFilterDTO ?? new JobApplicationFilterDTO { CompanyId = user.Id, PageSize = PageSize };
            }

            filter.RequestedPageNumber = page;

            var allJobApplications = await jobApplicationFacade.GetJobApplicationsAsync(new JobApplicationFilterDTO());

            var result = await jobApplicationFacade.GetJobApplicationsAsync(filter);
            var model = InitializeProductListViewModel(result, (int)allJobApplications.TotalItemsCount);
            return View("JobApplicationListView", model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(JobApplicationListViewModel model)
        {
            model.Filter.PageSize = PageSize;
            Session[FilterSessionKey] = model.Filter;

            var allJobOffers = await jobApplicationFacade.GetJobApplicationsAsync(new JobApplicationFilterDTO());
            var result = await jobApplicationFacade.GetJobApplicationsAsync(model.Filter);
            var newModel = InitializeProductListViewModel(result, (int)allJobOffers.TotalItemsCount);
            return View("JobApplicationListView", newModel);
        }

        private JobApplicationListViewModel InitializeProductListViewModel(QueryResultDto<JobApplicationDTO, JobApplicationFilterDTO> result, int totalItemsCount)
        {
            return new JobApplicationListViewModel
            {
                JobApplications = new StaticPagedList<JobApplicationDTO>(result.Items, result.RequestedPageNumber ?? 1, PageSize, totalItemsCount),
                Filter = result.Filter
            };
        }
        public ActionResult ClearFilter()
        {
            Session[FilterSessionKey] = null;
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var model = await jobApplicationFacade.GetJobApplicationById(id);
            return View("JobOfferDetailView", model);
        }

        public async Task<ActionResult> ChangeStatus(Guid id, bool accepted)
        {
            var jobApplication = await jobApplicationFacade.GetJobApplicationById(id);
            jobApplication.ApplicationStatus = accepted ? ApplicationStatus.Accepted : ApplicationStatus.Declined;
            await jobApplicationFacade.UpdateJobApplication(jobApplication);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Cancel( Guid jobApplicationId)
        {
            await jobApplicationFacade.DeleteJobApplication(jobApplicationId);
            return View("_JobApplicationListJobseeker");
        }

        [HttpPost]
        public async Task<ActionResult> Apply(Guid jobOfferId)
        {
            var jobOffer = await jobOfferFacade.GetJobOfferById(jobOfferId);
            var user = await jobseekerFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            var jobseeker = await jobseekerFacade.GetJobseekerById(user.Id);
            await jobApplicationFacade.CreateJobApplication(
                new JobApplicationDTO
                {
                    ApplicationStatus = ApplicationStatus.Undecided,
                    JobOfferId = jobOffer.Id,
                    JobOffer = jobOffer,
                    CompanyId = jobOffer.Company.Id,
                    Company = jobOffer.Company,
                    Jobseeker = jobseeker,
                    JobseekerId = jobseeker.Id
                });
            return RedirectToAction("Index", "JobOffer");
        }
    }
}