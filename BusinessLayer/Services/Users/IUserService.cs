using BusinessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Users
{
    public interface IUserService
    {
        Task<Guid> RegisterUserAsync(JobseekerRegistrationDTO jobseekerRegistrationDTO);
        Task<Guid> RegisterUserAsync(CompanyRegistrationDTO companyRegistrationDTO);
        Task<(bool success, string roles)> AuthorizeUserAsync(string username, string password);
        Task<UserDTO> GetUserAccordingToUsernameAsync(string username);
    }
}
