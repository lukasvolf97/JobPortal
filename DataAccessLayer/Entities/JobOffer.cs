using DataAccessLayer.Enums;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class JobOffer : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(JobPortalDbContext.JobOffers);

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [MaxLength(512)]
        public string Description { get; set; }

        public List<string> EntryQuestions { get; set; }

        [Required]
        public int Salary { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationTime { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(64)]
        public string Location { get; set; }

        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
