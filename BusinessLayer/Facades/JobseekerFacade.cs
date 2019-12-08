using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.Jobseekers;
using DataAccessLayer.Enums;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Facades
{
    public class JobseekerFacade : FacadeBase
    {

        private readonly IJobseekerService jobseekerService;
        public JobseekerFacade(IUnitOfWorkProvider unitOfWorkProvider, IJobseekerService jobseekerService) : base(unitOfWorkProvider)
        {
            this.jobseekerService = jobseekerService;
        }

        public async Task<QueryResultDto<JobseekerDTO, JobseekerFilterDTO>> ListAllJobseekers()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobseekerService.ListAllAsync();
            }
        }

        public async Task<JobseekerDTO> GetJobseekerByEducation(EducationType educationType)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobseekerService.GetJobseekerAccordingToEducationAsync(educationType);
            }
        }

        public async Task<JobseekerDTO> GetJobseekerById(Guid jobseekerId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobseekerService.GetJobseekerAccordingToId(jobseekerId);
            }
        }



        public async Task UpdateJobOffer(JobseekerDTO jobseeker)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                await jobseekerService.Update(jobseeker);
                await uow.Commit();
            }
        }

        public async Task<Guid> CreateJobOffer(JobseekerDTO jobseeker)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var jobOfferId = jobseekerService.Create(jobseeker);
                await uow.Commit();
                return jobOfferId;
            }
        }

        public async Task DeleteJobOffer(Guid jobseekerId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                jobseekerService.Delete(jobseekerId);
                await uow.Commit();
            }
        }
    }
}
