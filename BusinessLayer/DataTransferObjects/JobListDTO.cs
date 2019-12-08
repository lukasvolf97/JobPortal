using BusinessLayer.DataTransferObjects.Common;

namespace BusinessLayer.DataTransferObjects
{
    public class JobListDTO : DtoBase
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public CompanyDTO Company { get; set; }
    }
}
