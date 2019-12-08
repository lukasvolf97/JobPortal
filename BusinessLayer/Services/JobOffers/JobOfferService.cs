using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using DataAccessLayer.Entities;
using Infrastructure;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services.JobOffers
{
    public class JobOfferService : CrudQueryServiceBase<JobOffer, JobOfferDTO, JobOfferFilterDTO>, IJobOfferService
    {
        public JobOfferService(IMapper mapper, IRepository<JobOffer> jobOfferRepository, QueryObjectBase<JobOfferDTO, JobOffer, JobOfferFilterDTO, IQuery<JobOffer>> jobOfferQuery)
            : base(mapper, jobOfferRepository, jobOfferQuery) { }

        public async Task<IEnumerable<JobOfferDTO>> GetJobOfferAccordingToCompanyAsync(Guid companyId)
        {
            var queryResult = await Query.ExecuteQuery(new JobOfferFilterDTO { CompanyId = companyId });
            return queryResult.Items;
        }

        public async Task<IEnumerable<JobOfferDTO>> GetJobOfferAccordingToLocationAsync(string location)
        {
            var queryResult = await Query.ExecuteQuery(new JobOfferFilterDTO { Location = location });
            return queryResult.Items;
        }

        public async Task<JobOfferDTO> GetJobOfferAccordingToNameAsync(string name)
        {
            var queryResult = await Query.ExecuteQuery(new JobOfferFilterDTO { Name = name });
            return queryResult.Items.SingleOrDefault();
        }

        protected override async Task<JobOffer> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(JobOffer.Company)/*, nameof(JobOffer.JobApplications)*/);
        }

        public async Task<QueryResultDto<JobOfferDTO, JobOfferFilterDTO>> ListJobOffersAsync(JobOfferFilterDTO filter)
        {
            return await Query.ExecuteQuery(filter);
        }
    }
}
