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
    [Authorize(Roles = "Company")]
    public class CompanyController : Controller
    {
        private CompanyFacade companyFacade;

        public CompanyController(CompanyFacade companyFacade)
        {
            this.companyFacade = companyFacade;
        }

        public async Task<ActionResult> Index()
        {
            var userCompany = await companyFacade.GetUserAccordingToUsernameAsync(User.Identity.Name.Split('@')[0]);
            var company = await companyFacade.GetCompanyById(userCompany.Id);
            var model = InitializeModel(company);
            return View("CompanyProfileView",model);
        }

        private CompanyProfileModel InitializeModel(CompanyDTO company)
        {
            return new CompanyProfileModel
            {
                Name = company.Name,
                Description = company.Description,
                PhoneNumber = company.PhoneNumber,
                Location = company.Location
            };
        }
    }
}