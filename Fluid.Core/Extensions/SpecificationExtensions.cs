using Fluid.Core.Specifications.Base;

namespace Fluid.Core.Extensions;

public static class SpecificationExtensions
{
    public static ISpecification<T> And<T>(this ISpecification<T> left, ISpecification<T> right) where T : class, IEntity
    {
        return new AndSpecification<T>(left, right);
    }

    public static ISpecification<T> Or<T>(this ISpecification<T> left, ISpecification<T> right) where T : class, IEntity
    {
        return new OrSpecification<T>(left, right);
    }

    public static IQueryable<T> Specify<T>(this IQueryable<T> query, ISpecification<T> spec) where T : class, IEntity
    {
        if (spec == null)
            return query;

        if (spec.FilterCondition != null)
            return query.Where(spec.FilterCondition);

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }

    public static IQueryable<T> SpecifyMultipleByAnd<T, TData>(this IQueryable<T> query, Func<TData, ISpecification<T>> specMethod, IEnumerable<TData> data) where T : class, IEntity
    {
        if (data == null || !data.Any() || specMethod == null)
            return query;

        var spec = specMethod(data.First());

        foreach (var i in data.Skip(1))
            spec = spec.And(specMethod(i));

        query = query.Specify(spec);

        return query;
    }

    public static IQueryable<T> SpecifyMultipleByOr<T, TData>(this IQueryable<T> query, Func<TData, ISpecification<T>> specMethod, IEnumerable<TData> data) where T : class, IEntity
    {
        if (data == null || !data.Any() || specMethod == null)
            return query;

        var spec = specMethod(data.First());

        foreach (var i in data.Skip(1))
            spec = spec.Or(specMethod(i));

        query = query.Specify(spec);

        return query;
    }
}
