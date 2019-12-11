using BusinessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.Models
{
    public class JobseekerProfileModel
    {
        public JobseekerDTO Jobseeker { get; set; }
        public ICollection<JobApplicationDTO> JobApplications { get; set; }
    }
}