using BusinessLayer.DataTransferObjects.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DataTransferObjects
{
    public class JobOfferDTO : DtoBase
    {
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Location is required!")]
        public string Location { get; set; }

        public CompanyDTO Company { get; set; } = null;

        public Guid CompanyId { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Salary is required!")]
        public int Salary { get; set; }

        public ICollection<JobApplicationDTO> JobApplications { get; set; }
    }
}
