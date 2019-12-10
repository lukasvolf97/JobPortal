using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.Jobseekers;
using BusinessLayer.Services.Users;
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
        private readonly IUserService userService;
        public JobseekerFacade(IUnitOfWorkProvider unitOfWorkProvider, IJobseekerService jobseekerService, IUserService userService) : base(unitOfWorkProvider)
        {
            this.jobseekerService = jobseekerService;
            this.userService = userService;
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

        public async Task<Guid> RegisterJobSeeker(JobseekerRegistrationDTO jobseekerRegistrationDTO)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                try
                {
                    var id = await userService.RegisterUserAsync(jobseekerRegistrationDTO);
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
