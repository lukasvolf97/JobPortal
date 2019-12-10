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

        public JobOfferController(JobOfferFacade jobOfferFacade)
        {
            this.jobOfferFacade = jobOfferFacade;
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
            
            var allJobOffers = await jobOfferFacade.GetJobOffersAsync(new JobOfferFilterDTO());
            var result = await jobOfferFacade.GetJobOffersAsync(model.Filter);
            var newModel = InitializeJobOfferListViewModel(result, (int)allJobOffers.TotalItemsCount);
            return View("JobOfferListView", newModel);
        }

        private JobOfferListViewModel InitializeJobOfferListViewModel(QueryResultDto<JobOfferDTO, JobOfferFilterDTO> result, int totalItemsCount)
        {
            return new JobOfferListViewModel
            {
                JobOffers = new StaticPagedList<JobOfferDTO>(result.Items, result.RequestedPageNumber ?? 1, PageSize, totalItemsCount),
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
            var model = InitializeJobOfferDetailsModel(await jobOfferFacade.GetJobOfferById(id));
            return View("JobOfferDetailView", model);
        }

        private JobOfferDetailsModel InitializeJobOfferDetailsModel(JobOfferDTO joboffer)
        {
            return new JobOfferDetailsModel
            {
                Id = joboffer.Id,
                Name = joboffer.Name,
                Description = joboffer.Description,
                EntryQuestions = joboffer.EntryQuestions,
                Location = joboffer.Location,
                Company = joboffer.Company,
                Date = joboffer.Date,
                Salary = joboffer.Salary
            };
        }
    }
}