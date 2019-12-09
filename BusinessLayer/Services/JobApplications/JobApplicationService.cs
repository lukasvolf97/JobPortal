using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using BusinessLayer.Services.JobApplications;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using Infrastructure;
using Infrastructure.Query;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services.JobOffers
{
    public class JobApplicationService : CrudQueryServiceBase<JobApplication, JobApplicationDTO, JobApplicationFilterDTO>, IJobApplicationService
    {
        public JobApplicationService(IMapper mapper, IRepository<JobApplication> jobApplicationRepository, QueryObjectBase<JobApplicationDTO, JobApplication, JobApplicationFilterDTO, IQuery<JobApplication>> jobApplicationQuery)
            : base(mapper, jobApplicationRepository, jobApplicationQuery) { }

        public async Task<JobApplicationDTO> GetJobApplicationAccordingToCompanyAsync(Guid companyId)
        {
            var queryResult = await Query.ExecuteQuery(new JobApplicationFilterDTO { CompanyId = companyId });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<JobApplicationDTO> GetJobApplicationAccordingToJobOfferAsync(Guid jobOfferId)
        {
            var queryResult = await Query.ExecuteQuery(new JobApplicationFilterDTO { JobOfferId = jobOfferId });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<JobApplicationDTO> GetJobApplicationAccordingToJobseekerAsync(Guid jobseekerId)
        {
            var queryResult = await Query.ExecuteQuery(new JobApplicationFilterDTO { JobseekerId = jobseekerId });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<JobApplicationDTO> GetJobApplicationAccordingToStatusAsync(ApplicationStatus status)
        {
            var queryResult = await Query.ExecuteQuery(new JobApplicationFilterDTO { ApplicationStatus = status });
            return queryResult.Items.SingleOrDefault();
        }

        protected override async Task<JobApplication> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, /*nameof(JobApplication.Company),*/ nameof(JobApplication.JobOffer)/*, nameof(JobApplication.Jobseeker)*/);
        }

        public async Task<QueryResultDto<JobApplicationDTO, JobApplicationFilterDTO>> ListJobApplicationsAsync(JobApplicationFilterDTO filter)
        {
            var result = await Query.ExecuteQuery(filter);
            var items = result.Items.ToList();
            for (int i = 0; i < result.Items.Count(); i++)
            {
                items[i] = await GetAsync(items[i].Id, true);
            }
            result.Items = items;
            return result;
        }
    }
}
