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
    public class JobApplicationServiceTests
    {
        private JobApplicationService service;


        [Test]
        public async Task GetJobApplicationAccordingToStatusTest()
        {
            var expectedQueryResult = new QueryResultDto<JobApplicationDTO, JobApplicationFilterDTO>
            {
                Items = new List<JobApplicationDTO> {  new JobApplicationDTO
                    {
                        Id = Guid.Parse("aa05dc64-5c27-40ce-a916-175165b9b91f"),
                    }
        }
            };
            ServiceInit(expectedQueryResult);
            var actual = await service.GetJobApplicationAccordingToStatusAsync(ApplicationStatus.Undecided);

            Assert.AreEqual(expectedQueryResult.Items.ToArray().First().Id, actual.Id);
        }

        [Test]
        public async Task GetJobApplicationAccordingToJobseekerTest()
        {
            var expectedQueryResult = new QueryResultDto<JobApplicationDTO, JobApplicationFilterDTO>
            {
                Items = new List<JobApplicationDTO> { new JobApplicationDTO
                    {
                        Id = Guid.Parse("aa05dc64-5c27-40ce-a916-175165b9b91f"),
                    }
                }
            };
            ServiceInit(expectedQueryResult);
            var actual = await service.GetJobApplicationAccordingToJobseekerAsync(Guid.Parse("aa07dc64-5c07-40fe-a916-175165b9b90f"));

            Assert.AreEqual(expectedQueryResult.Items.ToArray().First().Id, actual.Id);
        }

        private void ServiceInit(QueryResultDto<JobApplicationDTO, JobApplicationFilterDTO> expectedQueryResult)
        {
            var mockManager = new FacadeMockManager();
            var mapper = FacadeMockManager.ConfigureRealMapper();
            var repositoryMock = mockManager.ConfigureRepositoryMock<JobApplication>();
            var queryMock = mockManager.ConfigureQueryObjectMock<JobApplicationDTO, JobApplication, JobApplicationFilterDTO>(expectedQueryResult);
            service = new JobApplicationService(mapper, repositoryMock.Object, queryMock.Object);
        }
    }
}
