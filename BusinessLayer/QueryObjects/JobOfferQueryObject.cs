using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using DataAccessLayer.Entities;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.QueryObjects.Common
{
    public class JobOfferQueryObject : QueryObjectBase<JobOfferDTO, JobOffer, JobOfferFilterDTO, IQuery<JobOffer>>
    {
        public JobOfferQueryObject(IMapper mapper, IQuery<JobOffer> query) : base(mapper, query) { }

        protected override IQuery<JobOffer> ApplyWhereClause(IQuery<JobOffer> query, JobOfferFilterDTO filter)
        {
            var definedPredicates = new List<IPredicate>();
            AddIfDefined(FilterByName(filter), definedPredicates);
            AddIfDefined(FilterBySalary(filter), definedPredicates);
            if (definedPredicates.Count == 0)
            {
                return query;
            }
            if (definedPredicates.Count == 1)
            {
                return query.Where(definedPredicates.First());
            }
            var wherePredicate = new CompositePredicate(definedPredicates);
            return query.Where(wherePredicate);
        }

        private static void AddIfDefined(IPredicate predicate, ICollection<IPredicate> definedPredicates)
        {
            if (predicate != null)
            {
                definedPredicates.Add(predicate);
            }
        }

        private static SimplePredicate FilterByName(JobOfferFilterDTO filter)
        {
            if (string.IsNullOrWhiteSpace(filter.Name))
            {
                return null;
            }
            return new SimplePredicate(nameof(JobOffer.Name), ValueComparingOperator.StringContains,
                filter.Name);
        }

        private static IPredicate FilterBySalary(JobOfferFilterDTO filter)
        {
            if (filter.MinimalSalary == 0 && filter.MaximalSalary == decimal.MaxValue)
            {
                return null;
            }
            if (filter.MinimalSalary > 0 && filter.MaximalSalary < decimal.MaxValue)
            {
                return new CompositePredicate(new List<IPredicate>
                {
                    new SimplePredicate(nameof(JobOffer.Salary), ValueComparingOperator.GreaterThanOrEqual,
                        filter.MinimalSalary),
                    new SimplePredicate(nameof(JobOffer.Salary), ValueComparingOperator.LessThanOrEqual,
                        filter.MaximalSalary),
                });
            }
            if (filter.MinimalSalary > 0)
            {
                return new SimplePredicate(nameof(JobOffer.Salary), ValueComparingOperator.GreaterThanOrEqual, filter.MinimalSalary);
            }

            return new SimplePredicate(nameof(JobOffer.Salary), ValueComparingOperator.LessThanOrEqual, filter.MaximalSalary);
        }
    }
}
