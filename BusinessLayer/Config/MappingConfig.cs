using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using DataAccessLayer.Entities;
using Infrastructure.Query;

namespace BusinessLayer.Config
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Admin, AdminDTO>().ReverseMap();
            config.CreateMap<JobOffer, JobOfferDTO>().ReverseMap();
            config.CreateMap<Jobseeker, JobseekerDTO>().ReverseMap();
            config.CreateMap<JobApplication, JobApplicationDTO>().ReverseMap();
            config.CreateMap<Company, CompanyDTO>().ReverseMap();
            config.CreateMap<JobOffer, JobListDTO>();
            config.CreateMap<Jobseeker, UserProfileDTO>();
            config.CreateMap<Jobseeker, JobseekerRegistrationDTO>().ReverseMap();
            config.CreateMap<User, UserDTO>().ReverseMap();
            config.CreateMap<User, JobseekerRegistrationDTO>().ReverseMap();
            config.CreateMap<User, CompanyRegistrationDTO>().ReverseMap();
            config.CreateMap<Company, JobseekerRegistrationDTO>().ReverseMap();
            config.CreateMap<Company, CompanyRegistrationDTO>().ReverseMap();
            config.CreateMap<QueryResult<JobOffer>, QueryResultDto<JobOfferDTO, JobOfferFilterDTO>>();
            config.CreateMap<QueryResult<Jobseeker>, QueryResultDto<JobseekerDTO, JobseekerFilterDTO>>();
            config.CreateMap<QueryResult<Company>, QueryResultDto<CompanyDTO, CompanyFilterDTO>>();
            config.CreateMap<QueryResult<JobApplication>, QueryResultDto<JobApplicationDTO, JobApplicationFilterDTO>>();
            config.CreateMap<QueryResult<User>, QueryResultDto<UserDTO, UserFilterDTO>>();
        }

    }
}
