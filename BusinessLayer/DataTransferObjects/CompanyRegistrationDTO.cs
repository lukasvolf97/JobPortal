using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DataTransferObjects
{
    public class CompanyRegistrationDTO : UserRegistrationDTO
    {
        [Required(ErrorMessage = "Name is required!")]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Location is required!")]
        [MaxLength(64)]
        public string Location { get; set; }

        [Required(ErrorMessage = "Phone number is required!")]
        [Phone]
        public string PhoneNumber { get; set; }

    }
}
