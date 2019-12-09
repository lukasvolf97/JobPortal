using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.Models
{
    public class JobseekerProfileModel
    {
        public string TitlesBeforeName { get; set; }
        public string TitlesAfterName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string Address { get; set; }
    }
}