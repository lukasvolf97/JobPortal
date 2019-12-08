using BusinessLayer.DataTransferObjects.Common;
using DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class JobApplicationFilterDTO : FilterDtoBase
    {
        public Guid JobOfferId { get; set; }
        public Guid JobseekerId { get; set; }

        public Guid CompanyId { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; }
    }
}
