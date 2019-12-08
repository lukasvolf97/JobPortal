using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using DataAccessLayer.Enums;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Services.JobApplications
{
    public interface IJobApplicationService
    {
        /// <summary>
        /// Gets job application with given application status
        /// </summary>
        /// <param name="status">status</param>
        /// <returns>Job application with given application status</returns>
        Task<JobApplicationDTO> GetJobApplicationAccordingToStatusAsync(ApplicationStatus status);

        /// <summary>
        /// Gets job application with given company
        /// </summary>
        /// <param name="companyId">company</param>
        /// <returns>Job application with given company</returns>
        Task<JobApplicationDTO> GetJobApplicationAccordingToCompanyAsync(Guid companyId);

        /// <summary>
        /// Gets job application with given jobseeker
        /// </summary>
        /// <param name="jobseekerId">jobseeker</param>
        /// <returns>Job application with given jobseeker</returns>
        Task<JobApplicationDTO> GetJobApplicationAccordingToJobseekerAsync(Guid jobseekerId);

        /// <summary>
        /// Gets job application with given jobOffer
        /// </summary>
        /// <param name="jobOfferId">jobOffer</param>
        /// <returns>Job application with given jobOffer</returns>
        Task<JobApplicationDTO> GetJobApplicationAccordingToJobOfferAsync(Guid jobOfferId);

        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<JobApplicationDTO> GetAsync(Guid entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(JobApplicationDTO entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(JobApplicationDTO entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(Guid entityId);

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available dtos (for given type)</returns>
        Task<QueryResultDto<JobApplicationDTO, JobApplicationFilterDTO>> ListAllAsync();
    }
}
