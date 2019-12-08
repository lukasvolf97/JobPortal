using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects.Common;
using DataAccessLayer.Entities;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;
using System.Collections.Generic;

namespace BusinessLayer.QueryObjects
{
    public class JobApplicationQueryObject : QueryObjectBase<JobApplicationDTO, JobApplication, JobApplicationFilterDTO, IQuery<JobApplication>>
    {
        public JobApplicationQueryObject(IMapper mapper, IQuery<JobApplication> query) : base(mapper, query) { }

        protected override IQuery<JobApplication> ApplyWhereClause(IQuery<JobApplication> query, JobApplicationFilterDTO filter)
        {
            return filter.CompanyId.Equals(null) || filter.JobseekerId.Equals(null)
                 ? query
                 : query.Where(new SimplePredicate(nameof(JobApplication.Jobseeker), ValueComparingOperator.Equal, filter.JobseekerId)
                 /*: query.Where(new CompositePredicate(new List<IPredicate>() {
                     new SimplePredicate(nameof(JobApplication.Company), ValueComparingOperator.Equal, filter.CompanyId),
                     new SimplePredicate(nameof(JobApplication.Jobseeker), ValueComparingOperator.Equal, filter.JobseekerId),
                 })*/
                 );
        }
    }
 }
