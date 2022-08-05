namespace Fluid.Core.Specifications.Base;

public interface ISpecification<T> where T : class, IEntity
{
    Expression<Func<T, bool>> FilterCondition { get; }

    List<Expression<Func<T, object>>> Includes { get; }

    List<string> IncludeStrings { get; }

    bool IsSatisfiedBy(T entity);
}