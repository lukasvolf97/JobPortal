using BusinessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.Models
{
    public class JobOfferDetailsModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> EntryQuestions { get; set; }

        public string Location { get; set; }

        public CompanyDTO Company { get; set; }

        public DateTime Date { get; set; }

        public int Salary { get; set; }
    }
}