using DataAccessLayer.Enums;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class JobApplication : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(JobPortalDbContext.JobApplications);

        public ApplicationStatus ApplicationStatus { get; set; }

        [ForeignKey(nameof(JobOffer))]
        public Guid JobOfferId { get; set; }

        public virtual JobOffer JobOffer { get; set; }
        /*
        [ForeignKey(nameof(Jobseeker))]
        public Guid JobseekerId { get; set; }

        public virtual Jobseeker Jobseeker { get; set; }*/
    }
}
