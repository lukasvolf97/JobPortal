using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace PresentationLayer.Models
{
    public class CustomJobOfferModel
    {
        public string[] JobOfferSortCriteria => new[] { nameof(JobOfferDTO.Salary), nameof(JobOfferDTO.Date) };

        public IPagedList<JobOfferDTO> JobOffers { get; set; }

        public JobOfferFilterDTO Filter { get; set; }

        public SelectList AllSortCriteria => new SelectList(JobOfferSortCriteria);

        public JobOfferDTO NewJobOffer { get; set; }
    }
}