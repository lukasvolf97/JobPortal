using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades;
using PresentationLayer.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using X.PagedList;

namespace PresentationLayer.Controllers
{
    public class CompanyController : Controller
    {
        public const int PageSize = 9;

        private const string FilterSessionKey = "filter";

        private CompanyFacade companyFacade;

        public CompanyController(CompanyFacade companyFacade)
        {
            this.companyFacade = companyFacade;
        }

        public async Task<ActionResult> Index(int page = 1)
        {
            var filter = Session[FilterSessionKey] as CompanyFilterDTO ?? new CompanyFilterDTO { PageSize = PageSize };
            filter.RequestedPageNumber = page;

            var allJobOffers = await companyFacade.ListAllCompanies();
            var result = await companyFacade.ListAllCompanies();

            var model = InitializeProductListViewModel(result, (int)allJobOffers.TotalItemsCount);
            return View("CompanyListView", model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(CompanyListViewModel model)
        {
            model.Filter.PageSize = PageSize;
            Session[FilterSessionKey] = model.Filter;

            var allJobOffers = await companyFacade.ListAllCompanies();
            var result = await companyFacade.ListAllCompanies();
            var newModel = InitializeProductListViewModel(result, (int)allJobOffers.TotalItemsCount);
            return View("CompanyListView", newModel);
        }

        private CompanyListViewModel InitializeProductListViewModel(QueryResultDto<CompanyDTO, CompanyFilterDTO> result, int totalItemsCount)
        {
            return new CompanyListViewModel
            {
                JobOffers = new StaticPagedList<CompanyDTO>(result.Items, result.RequestedPageNumber ?? 1, PageSize, totalItemsCount),
                Filter = result.Filter
            };
        }
        public ActionResult ClearFilter()
        {
            Session[FilterSessionKey] = null;
            return RedirectToAction(nameof(Index));
        }
    }
}