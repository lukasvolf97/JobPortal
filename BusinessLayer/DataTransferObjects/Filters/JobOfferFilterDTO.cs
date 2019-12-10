using BusinessLayer.DataTransferObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class JobOfferFilterDTO : FilterDtoBase
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public Guid? CompanyId { get; set; }

        public decimal MinimalSalary { get; set; } = 0;

        public decimal MaximalSalary { get; set; } = decimal.MaxValue;
    }
}
