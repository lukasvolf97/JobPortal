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
using BusinessLayer.Services.Jobseekers;
using DataAccessLayer.Entities;
using BusinessLayer.Tests.FacadesTests.Common;
using DataAccessLayer.Enums;
using NUnit.Framework;

namespace BusinessLayer.Test
{
    [TestFixture]
    public class JobseekerServiceTest
    {
        private JobseekerService service;


        [Test]
        public async Task GetJobseekerAccordingToEmailTest()
        {
            var expectedQueryResult = new QueryResultDto<JobseekerDTO, JobseekerFilterDTO>
            {
                Items = new List<JobseekerDTO> {  new JobseekerDTO
                    {
                        Id = Guid.Parse("aa01dc64-5c07-40fe-a916-175165b9b90f")
                    }
        }
            };
            ServiceInit(expectedQueryResult);
            var actual = await service.GetJobseekerAccordingToEmailAsync("mail@.com");

            Assert.AreEqual(expectedQueryResult.Items.ToArray().First().Id, actual.Id);
        }

        [Test]
        public async Task GetJobseekerAccordingToEducationTest()
        {
            var expectedQueryResult = new QueryResultDto<JobseekerDTO, JobseekerFilterDTO>
            {
                Items = new List<JobseekerDTO> { new JobseekerDTO
                    {
                        Id = Guid.Parse("aa07dc64-5c07-40fe-a916-175165b9b90f")
                    }
                }
            };
            ServiceInit(expectedQueryResult);
            var actual = await service.GetJobseekerAccordingToEducationAsync(EducationType.Graduation);

            Assert.AreEqual(expectedQueryResult.Items.ToArray().First().Id, actual.Id);
        }

        private void ServiceInit(QueryResultDto<JobseekerDTO, JobseekerFilterDTO> expectedQueryResult)
        {
            var mockManager = new FacadeMockManager();
            var mapper = FacadeMockManager.ConfigureRealMapper();
            var repositoryMock = mockManager.ConfigureRepositoryMock<Jobseeker>();
            var queryMock = mockManager.ConfigureQueryObjectMock<JobseekerDTO, Jobseeker, JobseekerFilterDTO>(expectedQueryResult);
            service = new JobseekerService(mapper, repositoryMock.Object, queryMock.Object);
        }
    }
}
