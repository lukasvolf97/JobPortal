using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades;
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

        public JobApplicationController(JobApplicationFacade jobApplicationFacade)
        {
            this.jobApplicationFacade = jobApplicationFacade;
        }

        public async Task<ActionResult> Index(int page = 1)
        {
            var filter = Session[FilterSessionKey] as JobApplicationFilterDTO ?? new JobApplicationFilterDTO { PageSize = PageSize };
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
            return View("JobOfferListView", newModel);
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
    }
}