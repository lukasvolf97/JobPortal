using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using DataAccessLayer.Entities;
using Infrastructure;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Users
{
    public class UserService : ServiceBase, IUserService
    {
        private const int PBKDF2IterCount = 100000;
        private const int PBKDF2SubkeyLength = 160 / 8;
        private const int saltSize = 128 / 8;

        private readonly IRepository<Jobseeker> jobseekerRepository;
        private readonly IRepository<Company> companyRepository;
        private readonly QueryObjectBase<UserDTO, User, UserFilterDTO, IQuery<User>> userQueryObject;

        public UserService(IMapper mapper, IRepository<Jobseeker> jobseekerRepository, IRepository<Company> companyRepository, QueryObjectBase<UserDTO, User, UserFilterDTO, IQuery<User>> userQueryObject)
            : base(mapper)
        {
            this.jobseekerRepository = jobseekerRepository;
            this.companyRepository = companyRepository;
            this.userQueryObject = userQueryObject;
        }

        public async Task<UserDTO> GetUserAccordingToUsernameAsync(string username)
        {
            var queryResult = await userQueryObject.ExecuteQuery(new UserFilterDTO() { Username = username });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<Guid> RegisterUserAsync(JobseekerRegistrationDTO jobseekerRegistrationDTO)
        {
            var jobseeker = Mapper.Map<Jobseeker>(jobseekerRegistrationDTO);

            if (await GetIfUserExistsAsync(jobseeker.UserName))
            {
                throw new ArgumentException();
            }

            var password = CreateHash(jobseekerRegistrationDTO.Password);
            jobseeker.PasswordHash = password.Item1;
            jobseeker.PasswordSalt = password.Item2;

            jobseekerRepository.Create(jobseeker);

            return jobseeker.Id;
        }

        public async Task<Guid> RegisterUserAsync(CompanyRegistrationDTO companyRegistrationDTO)
        {
            var company = Mapper.Map<Company>(companyRegistrationDTO);

            if (await GetIfUserExistsAsync(company.UserName))
            {
                throw new ArgumentException();
            }

            var password = CreateHash(companyRegistrationDTO.Password);
            company.PasswordHash = password.Item1;
            company.PasswordSalt = password.Item2;

            companyRepository.Create(company);

            return company.Id;
        }

        public async Task<(bool success, string roles)> AuthorizeUserAsync(string username, string password)
        {
            var userResult = await userQueryObject.ExecuteQuery(new UserFilterDTO { Username = username });
            var user = userResult.Items.SingleOrDefault();

            var succ = user != null && VerifyHashedPassword(user.PasswordHash, user.PasswordSalt, password);
            var roles = user?.Roles ?? "";
            return (succ, roles);
        }

        private async Task<bool> GetIfUserExistsAsync(string username)
        {
            var queryResult = await userQueryObject.ExecuteQuery(new UserFilterDTO { Username = username });
            return (queryResult.Items.Count() == 1);
        }

        private bool VerifyHashedPassword(string hashedPassword, string salt, string password)
        {
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            var saltBytes = Convert.FromBase64String(salt);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, PBKDF2IterCount))
            {
                var generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                return hashedPasswordBytes.SequenceEqual(generatedSubkey);
            }
        }

        private Tuple<string, string> CreateHash(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                return Tuple.Create(Convert.ToBase64String(subkey), Convert.ToBase64String(salt));
            }
        }
    }
}
