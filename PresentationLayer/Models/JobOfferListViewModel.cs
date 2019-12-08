using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using System.Web.Mvc;
using X.PagedList;

namespace PresentationLayer.Models
{
    public class JobOfferListViewModel
    {
        public string[] JobOfferSortCriteria => new[] { nameof(JobOfferDTO.Salary), nameof(JobOfferDTO.Date) };

        public IPagedList<JobOfferDTO> JobOffers { get; set; }

        public JobOfferFilterDTO Filter { get; set; }

        public SelectList AllSortCriteria => new SelectList(JobOfferSortCriteria);
    }
}