using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using DataAccessLayer.Entities;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;
using System;

namespace BusinessLayer.QueryObjects.Common
{
    public class JobOfferQueryObject : QueryObjectBase<JobOfferDTO, JobOffer, JobOfferFilterDTO, IQuery<JobOffer>>
    {
        public JobOfferQueryObject(IMapper mapper, IQuery<JobOffer> query) : base(mapper, query) { }

        protected override IQuery<JobOffer> ApplyWhereClause(IQuery<JobOffer> query, JobOfferFilterDTO filter)
        {
            return filter.CompanyId.Equals(Guid.Empty)
                 ? query
                 : query.Where(new SimplePredicate(nameof(JobOffer.CompanyId), ValueComparingOperator.Equal, filter.CompanyId));
        }
    }
}
