using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Companies
{
    public interface ICompanyService
    { 
        /// <summary>
        /// Gets company with given name
        /// </summary>
        /// <param name="name">email</param>
        /// <returns>Customer with given email address</returns>
        Task<CompanyDTO> GetCompanyAccordingToNameAsync(string name);

        /// <summary>
        /// Gets company with given location
        /// </summary>
        /// <param name="location">location</param>
        /// <returns>Customer with given location</returns>
        Task<CompanyDTO> GetCompanyAccordingToLocationAsync(string location);

        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<CompanyDTO> GetAsync(Guid entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(CompanyDTO entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(CompanyDTO entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(Guid entityId);

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available dtos (for given type)</returns>
        Task<QueryResultDto<CompanyDTO, CompanyFilterDTO>> ListAllAsync();
    }
}
