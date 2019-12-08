using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Services.Companies;
using BusinessLayer.Services.JobApplications;
using BusinessLayer.Services.JobOffers;
using DataAccessLayer.Entities;
using BusinessLayer.Tests.FacadesTests.Common;
using DataAccessLayer.Enums;
using NUnit.Framework;

namespace BusinessLayer.Test
{
    [TestFixture]
    public class JobOfferServiceTests
    {
        private JobOfferService service;


        [Test]
        public async Task ListAllTest()
        {
            var expectedQueryResult = new QueryResultDto<JobOfferDTO, JobOfferFilterDTO>
            {
                Items = new List<JobOfferDTO> {
        }
            };
            ServiceInit(expectedQueryResult);
            var actual = await service.ListAllAsync();

            Assert.AreEqual(expectedQueryResult.Items.Count(), actual.Items.Count());
        }

        private void ServiceInit(QueryResultDto<JobOfferDTO, JobOfferFilterDTO> expectedQueryResult)
        {
            var mockManager = new FacadeMockManager();
            var mapper = FacadeMockManager.ConfigureRealMapper();
            var repositoryMock = mockManager.ConfigureRepositoryMock<JobOffer>();
            var queryMock = mockManager.ConfigureQueryObjectMock<JobOfferDTO, JobOffer, JobOfferFilterDTO>(expectedQueryResult);
            service = new JobOfferService(mapper, repositoryMock.Object, queryMock.Object);
        }
    }
}
