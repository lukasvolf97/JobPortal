using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.JobApplications;
using DataAccessLayer.Enums;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Facades
{
    public class JobApplicationFacade : FacadeBase
    {

        private readonly IJobApplicationService jobApplicationService;
        public JobApplicationFacade(IUnitOfWorkProvider unitOfWorkProvider, IJobApplicationService jobApplicationService) : base(unitOfWorkProvider)
        {
            this.jobApplicationService = jobApplicationService;
        }

        public async Task<JobApplicationDTO> ListJobApplicationsByCompany(Guid companyId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobApplicationService.GetJobApplicationAccordingToCompanyAsync(companyId);
            }
        }

        public async Task<JobApplicationDTO> ListJobApplicationsByJobseeker(Guid jobseekerId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobApplicationService.GetJobApplicationAccordingToJobseekerAsync(jobseekerId);
            }
        }

        public async Task<JobApplicationDTO> ListJobApplicationsByStatus(ApplicationStatus applicationStatus)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobApplicationService.GetJobApplicationAccordingToStatusAsync(applicationStatus);
            }
        }

        public async Task<QueryResultDto<JobApplicationDTO, JobApplicationFilterDTO>> GetJobApplicationsAsync(JobApplicationFilterDTO filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobApplicationService.ListJobApplicationsAsync(filter);
            }
        }

        public async Task<QueryResultDto<JobApplicationDTO, JobApplicationFilterDTO>> ListAllJobApplications()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobApplicationService.ListAllAsync();
            }
        }

        public async Task<JobApplicationDTO> GetJobApplicationById(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobApplicationService.GetAsync(id);
            }
        }

        public async Task UpdateJobApplication(JobApplicationDTO jobApplication)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                await jobApplicationService.Update(jobApplication);
                await uow.Commit();
            }
        }

        public async Task<Guid> CreateJobApplication(JobApplicationDTO jobApplication)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var jobOfferId = jobApplicationService.Create(jobApplication);
                await uow.Commit();
                return jobOfferId;
            }
        }

        public async Task DeleteJobApplication(Guid jobApplicationId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                jobApplicationService.Delete(jobApplicationId);
                await uow.Commit();
            }
        }
    }
}
