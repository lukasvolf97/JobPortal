using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using DataAccessLayer.Entities;
using Infrastructure;
using Infrastructure.Query;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Companies
{
    public class CompanyService : CrudQueryServiceBase<Company, CompanyDTO, CompanyFilterDTO>, ICompanyService
    {
        public CompanyService(IMapper mapper, IRepository<Company> jobseekerRepository, QueryObjectBase<CompanyDTO, Company, CompanyFilterDTO, IQuery<Company>> companyQuery)
          : base(mapper, jobseekerRepository, companyQuery) { }

        public async Task<CompanyDTO> GetCompanyAccordingToLocationAsync(string location)
        {
            var queryResult = await Query.ExecuteQuery(new CompanyFilterDTO { Location = location });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<CompanyDTO> GetCompanyAccordingToNameAsync(string name)
        {
            var queryResult = await Query.ExecuteQuery(new CompanyFilterDTO { Name = name });
            return queryResult.Items.SingleOrDefault();
        }

        protected override async Task<Company> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId/*,nameof(Company.JobApplications)*/);
        }
    }
}
