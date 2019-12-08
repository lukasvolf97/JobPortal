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
    public class CompanyQueryObject : QueryObjectBase<CompanyDTO, Company, CompanyFilterDTO, IQuery<Company>>
    {
        public CompanyQueryObject(IMapper mapper, IQuery<Company> query) : base(mapper, query) { }
        protected override IQuery<Company> ApplyWhereClause(IQuery<Company> query, CompanyFilterDTO filter)
        {
            return filter == null || filter.Name == null
            ? query
            : query.Where(new SimplePredicate(nameof(Company.Name), ValueComparingOperator.Equal, filter.Name));
        }
    }
}
