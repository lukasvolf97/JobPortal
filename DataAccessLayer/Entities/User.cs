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
    public abstract class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(DataAccessLayer.Entities.User);

        [MaxLength(64)]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100)]
        public string PasswordSalt { get; set; }

        [StringLength(100)]
        public string PasswordHash { get; set; }

        /// <summary>
        /// String with , delimiter.
        /// For example: "Admin,User"
        /// </summary>
        public string Roles { get; set; }
    }
}
