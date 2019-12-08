using System;
using System.Collections.Generic;
using DataAccessLayer.Enums;

namespace BusinessLayer.DataTransferObjects
{
    public class JobseekerDTO : UserDTO
    {
        public string TitlesBeforeName { get; set; }

        public string TitlesAfterName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MobilePhoneNumber { get; set; }

        public string Address { get; set; }

        public DateTime BirthDate { get; set; } = new DateTime(1950, 1, 1);

        public EducationType HighestEducation { get; set; }

        public ICollection<JobApplicationDTO> JobApplications { get; set; }       
    }
}
