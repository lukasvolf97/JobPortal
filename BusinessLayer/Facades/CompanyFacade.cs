using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.Companies;
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
        public CompanyFacade(IUnitOfWorkProvider unitOfWorkProvider, ICompanyService companyService) : base(unitOfWorkProvider)
        {
            this.companyService = companyService;
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
    }
}
