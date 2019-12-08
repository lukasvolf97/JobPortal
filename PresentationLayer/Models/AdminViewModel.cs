using System.Web;
using System.Web.Mvc;
using X.PagedList;
using BusinessLayer.DataTransferObjects;

namespace PresentationLayer.Models
{
    public class AdminViewModel
    {
        public IPagedList<CompanyDTO> Companies { get; set; }

        public IPagedList<JobOfferDTO> JobOffers { get; set; }

        public IPagedList<JobApplicationDTO> JobApplications { get; set; }


    }
}