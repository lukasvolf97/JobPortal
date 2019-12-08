using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using Infrastructure;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Jobseekers
{
    public class JobseekerService : CrudQueryServiceBase<Jobseeker, JobseekerDTO, JobseekerFilterDTO>, IJobseekerService
    {
        public JobseekerService(IMapper mapper, IRepository<Jobseeker> jobseekerRepository, QueryObjectBase<JobseekerDTO, Jobseeker, JobseekerFilterDTO, IQuery<Jobseeker>> jobseekerQuery)
          : base(mapper, jobseekerRepository, jobseekerQuery) { }

        public async Task<JobseekerDTO> GetJobseekerAccordingToEducationAsync(EducationType educationType)
        {
            var queryResult = await Query.ExecuteQuery(new JobseekerFilterDTO { HighestEducation = educationType });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<JobseekerDTO> GetJobseekerAccordingToEmailAsync(string email)
        {
            var queryResult = await Query.ExecuteQuery(new JobseekerFilterDTO { Email = email });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<JobseekerDTO> GetJobseekerAccordingToId(Guid jobseekerId)
        {
            var queryResult = await Query.ExecuteQuery(new JobseekerFilterDTO { Id = jobseekerId });
            return queryResult.Items.SingleOrDefault();
        }

        protected override async Task<Jobseeker> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, /*nameof(Jobseeker.JobApplications),*/ nameof(Jobseeker.HighestEducation));
        }
    }
}
