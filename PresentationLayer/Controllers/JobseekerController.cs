using BusinessLayer.DataTransferObjects;
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

        public JobseekerController(JobseekerFacade jobseekerFacade)
        {
            this.jobseekerFacade = jobseekerFacade;
        }

        public async Task<ActionResult> Index()
        {
            var userJobseeker = await jobseekerFacade.GetUserAccordingToUsernameAsync(User.Identity.Name.Split('@')[0]);
            var jobseeker = await jobseekerFacade.GetJobseekerById(userJobseeker.Id);
            var model = InitializeModel(jobseeker);
            return View("JobseekerProfileView", model);
        }

        private JobseekerProfileModel InitializeModel(JobseekerDTO jobseeker)
        {
            return new JobseekerProfileModel
            {
                TitlesBeforeName = jobseeker.TitlesBeforeName,
                TitlesAfterName = jobseeker.TitlesAfterName, 
                FirstName = jobseeker.FirstName,
                LastName = jobseeker.LastName, 
                MobilePhoneNumber = jobseeker.MobilePhoneNumber,
                Address = jobseeker.Address
            };
        }
    }
}