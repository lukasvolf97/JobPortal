using BusinessLayer.DataTransferObjects.Common;
using System;
using System.Collections.Generic;

namespace BusinessLayer.DataTransferObjects
{
    public class JobOfferDTO : DtoBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> EntryQuestions { get; set; }

        public string Location { get; set; }

        public CompanyDTO Company { get; set; }

        public DateTime CreationTime { get; set; }

        public int Salary { get; set; }

        public ICollection<JobApplicationDTO> JobApplications { get; set; }
    }
}
