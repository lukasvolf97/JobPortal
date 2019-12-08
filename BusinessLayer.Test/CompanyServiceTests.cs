using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Services.Companies;
using DataAccessLayer.Entities;
using BusinessLayer.Tests.FacadesTests.Common;
using NUnit.Framework;

namespace BusinessLayer.Test
{
    [TestFixture]
    public class CompanyServiceTests
    {
        private CompanyService service;
  

        [Test]
        public async Task GetCompanyAccordingToNameTest() 
        {
            var expectedQueryResult = new QueryResultDto<CompanyDTO, CompanyFilterDTO>
            {
                Items = new List<CompanyDTO> {  new CompanyDTO
                    {
                        Id = Guid.Parse("aa05dc64-5c07-40fe-a916-175165b9b91f"),
                    }
        }
            };
            ServiceInit(expectedQueryResult);
            var actual = await service.GetCompanyAccordingToNameAsync("first");

            Assert.AreEqual(expectedQueryResult.Items.ToArray().First().Id, actual.Id);
        }

        [Test]
        public async Task GetCompanyAccordingToLocationTest()
        {
            var expectedQueryResult = new QueryResultDto<CompanyDTO, CompanyFilterDTO>
            {
                Items = new List<CompanyDTO> { new CompanyDTO
                    {
                        Id = Guid.Parse("aa05dc64-5c07-40ce-a916-175165b9b91f")
                    }
                }
            };
            ServiceInit(expectedQueryResult);
            var actual = await service.GetCompanyAccordingToLocationAsync("UK");

            Assert.AreEqual(expectedQueryResult.Items.ToArray().First().Id, actual.Id);
        }

        private void ServiceInit(QueryResultDto<CompanyDTO, CompanyFilterDTO> expectedQueryResult)
        {
            var mockManager = new FacadeMockManager();
            var mapper = FacadeMockManager.ConfigureRealMapper();
            var repositoryMock = mockManager.ConfigureRepositoryMock<Company>();
            var queryMock = mockManager.ConfigureQueryObjectMock<CompanyDTO, Company, CompanyFilterDTO>(expectedQueryResult);
            service = new CompanyService(mapper, repositoryMock.Object, queryMock.Object);
        }
    }
}
