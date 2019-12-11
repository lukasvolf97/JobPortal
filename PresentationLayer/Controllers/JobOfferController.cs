using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using X.PagedList;

namespace PresentationLayer.Controllers
{
    public class JobOfferController : Controller
    {
        public const int PageSize = 9;

        private const string FilterSessionKey = "filter";

        private JobOfferFacade jobOfferFacade;
        private CompanyFacade companyFacade;

        public JobOfferController(JobOfferFacade jobOfferFacade, CompanyFacade companyFacade)
        {
            this.jobOfferFacade = jobOfferFacade;
            this.companyFacade = companyFacade;
        }

        public async Task<ActionResult> Index(int page = 1)
        {
            var filter = Session[FilterSessionKey] as JobOfferFilterDTO ?? new JobOfferFilterDTO { PageSize = PageSize };
            filter.RequestedPageNumber = page;

            var allJobOffers = await jobOfferFacade.GetJobOffersAsync(new JobOfferFilterDTO());

            var result = await jobOfferFacade.GetJobOffersAsync(filter);
            var model = InitializeJobOfferListViewModel(result, (int)allJobOffers.TotalItemsCount);
            return View("JobOfferListView", model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(JobOfferListViewModel model)
        {
            model.Filter.PageSize = PageSize;
            Session[FilterSessionKey] = model.Filter;
            
            return View("JobOfferListView", await GetModel(model.Filter));
        }

        [Authorize(Roles = "Company,Admin")]
        public async Task<ActionResult> CustomJobOffer(int page = 1)
        {
            JobOfferFilterDTO filter;
            if (User.IsInRole("Admin"))
                filter = Session[FilterSessionKey] as JobOfferFilterDTO ?? new JobOfferFilterDTO { PageSize = PageSize };
            else
            {
                var user = await companyFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
                filter = new JobOfferFilterDTO { CompanyId = user.Id, PageSize = PageSize };
            }

            filter.RequestedPageNumber = page;
            return View("JobOfferCustomView", await GetModel(filter));
        }

        [Authorize(Roles = "Company,Admin")]
        [HttpPost]
        public async Task<ActionResult> CustomJobOffer(JobOfferListViewModel model)
        {
            model.Filter.PageSize = PageSize;
            if (User.IsInRole("Admin"))
                Session[FilterSessionKey] = model.Filter;
            else
            {
                var user = await companyFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
                model.Filter.CompanyId = user.Id;
            }

            return View("JobOfferCustomView", await GetModel(model.Filter));
        }

        [Authorize(Roles = "Company,Admin")]
        public ActionResult Create()
        {
            return View("CreateJobOfferView");
        }


        public ActionResult ClearFilter()
        {
            Session[FilterSessionKey] = null;
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var model = InitializeJobOfferDetailsModel(await jobOfferFacade.GetJobOfferById(id));
            return View("JobOfferDetailView", model);
        }

        [Authorize(Roles = "Company,Admin")]
        [HttpPost]
        public async Task<ActionResult> Create(JobOfferDTO jobOffer)
        {
            var user = await companyFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            jobOffer.CompanyId = user.Id;
            await jobOfferFacade.CreateJobOffer(jobOffer);
            return RedirectToAction("CustomJobOffer", "JobOffer");
        }

        [Authorize(Roles = "Company,Admin")]
        [HttpPost]
        public async Task<ActionResult> Delete(Guid jobOfferId)
        {
            await jobOfferFacade.DeleteJobOffer(jobOfferId);
            return RedirectToAction("CustomJobOffer", "JobOffer");
        }

        private JobOfferDetailsModel InitializeJobOfferDetailsModel(JobOfferDTO joboffer)
        {
            return new JobOfferDetailsModel
            {
                Id = joboffer.Id,
                Name = joboffer.Name,
                Description = joboffer.Description,
                Location = joboffer.Location,
                Company = joboffer.Company,
                Date = joboffer.Date,
                Salary = joboffer.Salary
            };
        }
        private JobOfferListViewModel InitializeJobOfferListViewModel(QueryResultDto<JobOfferDTO, JobOfferFilterDTO> result, int totalItemsCount)
        {
            return new JobOfferListViewModel
            {
                JobOffers = new StaticPagedList<JobOfferDTO>(result.Items, result.RequestedPageNumber ?? 1, PageSize, totalItemsCount),
                Filter = result.Filter
            };
        }

        private async Task<JobOfferListViewModel> GetModel(JobOfferFilterDTO filter)
        {
            var allJobOffers = await jobOfferFacade.GetJobOffersAsync(new JobOfferFilterDTO());
            var result = await jobOfferFacade.GetJobOffersAsync(filter);
            return InitializeJobOfferListViewModel(result, (int)allJobOffers.TotalItemsCount);
        }
    }
}