using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PresentationLayer.Controllers
{
    public class JobseekerController : Controller
    {
        private JobseekerFacade jobseekerFacade;
        private JobApplicationFacade jobApplicationFacade;

        public JobseekerController(JobseekerFacade jobseekerFacade, JobApplicationFacade jobApplicationFacade)
        {
            this.jobseekerFacade = jobseekerFacade;
            this.jobApplicationFacade = jobApplicationFacade;
        }

        public async Task<ActionResult> Index()
        {
            var userJobseeker = await jobseekerFacade.GetUserAccordingToUsernameAsync(User.Identity.Name.Split('@')[0]);
            var jobseeker = await jobseekerFacade.GetJobseekerById(userJobseeker.Id);
            return View("JobseekerProfileView", jobseeker);
        }

        [HttpPost]
        public async Task<ActionResult> Index(JobseekerDTO jobseeker)
        {
            var userJobseeker = await jobseekerFacade.GetUserAccordingToUsernameAsync(User.Identity.Name.Split('@')[0]);
            KeepUsersData(jobseeker, userJobseeker);
            await jobseekerFacade.UpdateJobOffer(jobseeker);
            return  View("JobseekerProfileView", await jobseekerFacade.GetJobseekerById(userJobseeker.Id));
        }

        private void KeepUsersData (JobseekerDTO jobseeker, UserDTO userJobseeker)
        {
            jobseeker.Id = userJobseeker.Id;
            jobseeker.Username = userJobseeker.Username;
            jobseeker.PasswordHash = userJobseeker.PasswordHash;
            jobseeker.PasswordSalt = userJobseeker.PasswordSalt;
            jobseeker.Roles = userJobseeker.Roles;
        }
    }
}