using BusinessLayer.DataTransferObjects.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DataTransferObjects
{
    public class UserDTO : DtoBase
    {
        [Required]
        public string Username { get; set; }

        [Required, StringLength(100)]
        public string PasswordSalt { get; set; }

        [Required, StringLength(100)]
        public string PasswordHash { get; set; }

        public string Roles { get; set; }
    }
}
