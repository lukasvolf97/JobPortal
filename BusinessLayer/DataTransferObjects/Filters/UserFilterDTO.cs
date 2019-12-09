using BusinessLayer.DataTransferObjects.Common;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class UserFilterDTO : FilterDtoBase
    {
        public string Username { get; set; }
    }
}
