using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.JobOffers;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Facades
{
    public class JobOfferFacade : FacadeBase
    {
        private readonly IJobOfferService jobOfferService;
        public JobOfferFacade(IUnitOfWorkProvider unitOfWorkProvider, IJobOfferService jobOfferService) : base(unitOfWorkProvider)
        {
            this.jobOfferService = jobOfferService;
        }

        public async Task<QueryResultDto<JobOfferDTO, JobOfferFilterDTO>> ListAllJobOffers()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobOfferService.ListAllAsync();
            }
        }

        public async Task<IEnumerable<JobOfferDTO>> ListAllJobOffersWithSameLocation(string location)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobOfferService.GetJobOfferAccordingToLocationAsync(location);
            }
        }

        public async Task<JobOfferDTO> ListAllJobOffersWithSameName(string name)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobOfferService.GetJobOfferAccordingToNameAsync(name);
            }
        }

        public async Task<IEnumerable<JobOfferDTO>> ListAllJobOffersOfCompany(Guid company)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobOfferService.GetJobOfferAccordingToCompanyAsync(company);
            }
        }

        public async Task<QueryResultDto<JobOfferDTO, JobOfferFilterDTO>> GetJobOffersAsync(JobOfferFilterDTO filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobOfferService.ListJobOffersAsync(filter);
            }
        }

        public async Task<JobOfferDTO> GetJobOfferById(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobOfferService.GetAsync(id);
            }
        }


        public async Task UpdateJobOffer(JobOfferDTO jobOffer)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                await jobOfferService.Update(jobOffer);
                await uow.Commit();
            }
        }

        public async Task<Guid> CreateJobOffer(JobOfferDTO jobOffer)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var jobOfferId = jobOfferService.Create(jobOffer);
                await uow.Commit();
                return jobOfferId;
            }
        }

        public async Task DeleteJobOffer(Guid jobOfferId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                jobOfferService.Delete(jobOfferId);
                await uow.Commit();
            }
        }

    }
}
