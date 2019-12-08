using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Enums;
using System.IO;
using Infrastructure;

namespace DataAccessLayer.Entities
{
    public class Jobseeker : User, IEntity
    {
        [NotMapped]
        public new string TableName { get; } = nameof(JobPortalDbContext.Jobseekers);

        [MaxLength(32)]
        public string TitlesBeforeName { get; set; }

        [MaxLength(32)]
        public string TitlesAfterName { get; set; }

        [MaxLength(64)]
        public string FirstName { get; set; }

        [MaxLength(64)]
        public string LastName { get; set; }

        [Phone]
        public string MobilePhoneNumber { get; set; }

        [MaxLength(1024)]
        public string Address { get; set; }

        public DateTime BirthDate { get; set; } = new DateTime(1950, 1, 1);

        public EducationType HighestEducation { get; set; }

        public virtual ICollection<JobApplication> JobApplications { get; set; }       
    }
}
