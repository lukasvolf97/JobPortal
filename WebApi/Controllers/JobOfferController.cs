using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class JobOfferController : ApiController
    {
        public JobOfferFacade JobOfferFacade { get; set; }

        [HttpGet]
        public async Task<IEnumerable<JobOfferDTO>> ListAllJobOffers()
        {
            var jobOffers = (await JobOfferFacade.ListAllJobOffers()).Items;
            foreach (var jobOffer in jobOffers)
            {
                jobOffer.Id = Guid.Empty;
            }
            return jobOffers;
        }

        // GET api/values/5
        public async Task<JobOfferDTO> Get(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var jobOffer = await JobOfferFacade.ListAllJobOffersWithSameName(name);
            if (jobOffer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            jobOffer.Id = Guid.Empty;
            return jobOffer;
        }
    }
}
