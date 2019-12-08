using Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Company : User
    {

        [NotMapped]
        public new string TableName { get; } = nameof(JobPortalDbContext.Companies);

        [MaxLength(64)]
        public string Name { get; set; }

        [MaxLength(512)]
        public string Description { get; set; }
        
        [MaxLength(64)]
        public string Location { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        public virtual ICollection<JobOffer> JobOffers { get; set; }
    }
}
