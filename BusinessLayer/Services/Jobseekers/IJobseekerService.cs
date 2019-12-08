using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Jobseekers
{
    public interface IJobseekerService
    { 
        /// <summary>
        /// Gets jobseeker with given email address
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>Customer with given email address</returns>
        Task<JobseekerDTO> GetJobseekerAccordingToEmailAsync(string email);

        /// <summary>
        /// Gets jobseeker with given Id
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>Customer with given email address</returns>
        Task<JobseekerDTO> GetJobseekerAccordingToId(Guid jobseekerId);

        /// <summary>
        /// Gets jobseeker with given highest education type
        /// </summary>
        /// <param name="educationType">education type</param>
        /// <returns>Jobseeker with given highest education type</returns>
        Task<JobseekerDTO> GetJobseekerAccordingToEducationAsync(EducationType educationType);

        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<JobseekerDTO> GetAsync(Guid entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(JobseekerDTO entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(JobseekerDTO entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(Guid entityId);

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available dtos (for given type)</returns>
        Task<QueryResultDto<JobseekerDTO, JobseekerFilterDTO>> ListAllAsync();
    }
}
