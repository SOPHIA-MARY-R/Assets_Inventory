namespace Fluid.Core.Specifications.Base;

public class BaseSpecification<T> : ISpecification<T> where T : class, IEntity
{
    public BaseSpecification()
    {

    }

    public BaseSpecification(Expression<Func<T, bool>> filterCondition)
    {
        FilterCondition = filterCondition;
    }

    public virtual Expression<Func<T, bool>> FilterCondition { get; protected set; }

    public List<Expression<Func<T, object>>> Includes { get; } = new();

    public List<string> IncludeStrings { get; } = new();

    protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected virtual void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    public bool IsSatisfiedBy(T entity)
    {
        return FilterCondition.Compile()(entity);
    }

    public BaseSpecification<T> And(BaseSpecification<T> specification)
    {
        return new AndSpecification<T>(this, specification);
    }

    public BaseSpecification<T> Or(BaseSpecification<T> specification)
    {
        return new OrSpecification<T>(this, specification);
    }
}
