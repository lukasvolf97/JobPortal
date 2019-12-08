using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Services.JobOffers
{
    public interface IJobOfferService
    { 
        /// <summary>
        /// Gets job offer with given name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>Job offer with given name</returns>
        Task<JobOfferDTO> GetJobOfferAccordingToNameAsync(string name);

        /// <summary>
        /// Gets job offer with given company
        /// </summary>
        /// <param name="companyId">company</param>
        /// <returns>Job offer with given company</returns>
        Task<IEnumerable<JobOfferDTO>> GetJobOfferAccordingToCompanyAsync(Guid companyId);

        /// <summary>
        /// Gets job offer with given location
        /// </summary>
        /// <param name="location">location</param>
        /// <returns>Job offer with given location</returns>
        Task<IEnumerable<JobOfferDTO>> GetJobOfferAccordingToLocationAsync(string location);

        /// <summary>
        /// Gets job offers according to given filter
        /// </summary>
        /// <param name="filter">The job offers filter</param>
        /// <returns>Filtered results</returns>
        Task<QueryResultDto<JobOfferDTO, JobOfferFilterDTO>> ListJobOffersAsync(JobOfferFilterDTO filter);

        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<JobOfferDTO> GetAsync(Guid entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(JobOfferDTO entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(JobOfferDTO entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(Guid entityId);

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available dtos (for given type)</returns>
        Task<QueryResultDto<JobOfferDTO, JobOfferFilterDTO>> ListAllAsync();
    }
}
