using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects.Common;
using DataAccessLayer.Entities;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;

namespace BusinessLayer.QueryObjects
{
    public class JobseekerQueryObject : QueryObjectBase<JobseekerDTO, Jobseeker, JobseekerFilterDTO, IQuery<Jobseeker>>
    {
        public JobseekerQueryObject(IMapper mapper, IQuery<Jobseeker> query) : base(mapper, query) { }
        protected override IQuery<Jobseeker> ApplyWhereClause(IQuery<Jobseeker> query, JobseekerFilterDTO filter)
        {
            if (filter.Email.Equals(null) && filter.HighestEducation.Equals(null))
                return query;
            else if (!filter.Email.Equals(null))
                return query.Where(new SimplePredicate(nameof(Jobseeker.Email), ValueComparingOperator.Equal, filter.Email));
            else if (!filter.HighestEducation.Equals(null))
                return query.Where(new SimplePredicate(nameof(Jobseeker.HighestEducation), ValueComparingOperator.Equal, filter.HighestEducation));
            return query;
        }
    }
}
