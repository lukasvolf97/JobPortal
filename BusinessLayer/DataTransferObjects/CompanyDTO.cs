using System.Collections.Generic;

namespace BusinessLayer.DataTransferObjects
{
    public class CompanyDTO : UserDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<JobOfferDTO> JobOffers { get; set; }

        public ICollection<JobApplicationDTO> JobApplications { get; set; }
    }
}
