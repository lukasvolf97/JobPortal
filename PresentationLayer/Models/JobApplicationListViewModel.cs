using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using System.Web.Mvc;
using X.PagedList;

namespace PresentationLayer.Models
{
    public class JobApplicationListViewModel
    {
        public string[] JobApplicationSortCriteria => new[] { nameof(JobApplicationDTO.ApplicationStatus), nameof(JobApplicationDTO.CompanyId), nameof(JobApplicationDTO.JobOfferId), nameof(JobApplicationDTO.JobseekerId) };

        public IPagedList<JobApplicationDTO> JobApplications { get; set; }

        public JobApplicationFilterDTO Filter { get; set; }

        public SelectList AllSortCriteria => new SelectList(JobApplicationSortCriteria);
    }
}