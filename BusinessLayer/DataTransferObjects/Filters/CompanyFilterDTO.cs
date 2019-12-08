﻿using BusinessLayer.DataTransferObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class CompanyFilterDTO : FilterDtoBase
    {
        public string Name { get; set; }

        public string Location { get; set; }
    }
}
