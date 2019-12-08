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
    public class CompanyListViewModel
    {
        public string[] CompanySortCriteria => new[] { nameof(CompanyDTO.Name), nameof(CompanyDTO.Location) };

        public IPagedList<CompanyDTO> JobOffers { get; set; }

        public CompanyFilterDTO Filter { get; set; }

        public SelectList AllSortCriteria => new SelectList(CompanySortCriteria);
    }
}