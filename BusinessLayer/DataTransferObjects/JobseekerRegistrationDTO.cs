﻿using BusinessLayer.DataTransferObjects.Common;
using DataAccessLayer.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DataTransferObjects
{
    public class JobseekerRegistrationDTO : UserRegistrationDTO
    {

        [MaxLength(64)]
        public string TitlesBeforeName { get; set; }

        [MaxLength(64)]
        public string TitlesAfterName { get; set; }

        [Required(ErrorMessage = "First name is required!")]
        [MaxLength(64)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [MaxLength(64)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required!")]
        [Phone]
        public string MobilePhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        [MaxLength(1024)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Birthdate is required!")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public EducationType HighestEducation { get; set; }
    }
}
