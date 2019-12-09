using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using System.Web.Mvc;
using X.PagedList;

namespace PresentationLayer.Models
{
    public class JobApplicationListViewModel
    {
        public string[] JobApplicationSortCriteria => new[] { nameof(JobApplicationDTO.ApplicationStatus), nameof(JobApplicationDTO.Company), nameof(JobApplicationDTO.JobOffer), nameof(JobApplicationDTO.Jobseeker) };

        public IPagedList<JobApplicationDTO> JobApplications { get; set; }

        public JobApplicationFilterDTO Filter { get; set; }

        public SelectList AllSortCriteria => new SelectList(JobApplicationSortCriteria);
    }
}