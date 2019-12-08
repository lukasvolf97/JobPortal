using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.Companies;
using BusinessLayer.Services.Users;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Facades
{
    public class CompanyFacade : FacadeBase
    {

        private readonly ICompanyService companyService;
        private readonly IUserService userService;
        public CompanyFacade(IUnitOfWorkProvider unitOfWorkProvider, IUserService userService, ICompanyService companyService) : base(unitOfWorkProvider)
        {
            this.companyService = companyService;
            this.userService = userService;
        }

        public async Task<QueryResultDto<CompanyDTO, CompanyFilterDTO>> ListAllCompanies()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await companyService.ListAllAsync();
            }
        }

        public async Task UpdateCompany(CompanyDTO company)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                await companyService.Update(company);
                await uow.Commit();
            }
        }

        public async Task<Guid> CreateCompany(CompanyDTO company)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var jobOfferId = companyService.Create(company);
                await uow.Commit();
                return jobOfferId;
            }
        }

        public async Task DeleteCompany(Guid companyId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                companyService.Delete(companyId);
                await uow.Commit();
            }
        }
        public async Task<Guid> RegisterCompany(CompanyRegistrationDTO companyRegistrationDTO)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                try
                {
                    var id = await userService.RegisterUserAsync(companyRegistrationDTO);
                    await uow.Commit();
                    return id;
                }
                catch (ArgumentException)
                {
                    throw;
                }
            }
        }

        public async Task<(bool success, string roles)> Login(string username, string password)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await userService.AuthorizeUserAsync(username, password);
            }
        }

        public async Task<UserDTO> GetUserAccordingToUsernameAsync(string username)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await userService.GetUserAccordingToUsernameAsync(username);
            }
        }

    }
}
