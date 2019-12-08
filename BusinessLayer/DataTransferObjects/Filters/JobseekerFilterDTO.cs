using BusinessLayer.DataTransferObjects.Common;
using DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class JobseekerFilterDTO : FilterDtoBase
    {

        public EducationType HighestEducation { get; set; }

        public string Email { get; set; }

        public Guid Id { get; set; }
    }
}
