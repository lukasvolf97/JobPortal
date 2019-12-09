using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.Models
{
    public class CompanyProfileModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
    }
}