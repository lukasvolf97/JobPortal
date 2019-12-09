using BusinessLayer.DataTransferObjects.Common;
using DataAccessLayer.Enums;
using System;

namespace BusinessLayer.DataTransferObjects
{
    public class JobApplicationDTO : DtoBase
    {

        public string TableName { get; } = nameof(JobApplicationDTO);

        public ApplicationStatus ApplicationStatus { get; set; }

        public Guid JobOfferId { get; set; }

        public JobOfferDTO JobOffer { get; set; }

        public Guid CompanyId { get; set; }

        public CompanyDTO Company { get; set; }

        public Guid JobseekerId { get; set; }

        public JobseekerDTO Jobseeker { get; set; }
    }
}
