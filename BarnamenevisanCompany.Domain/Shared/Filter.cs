using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace BarnamenevisanCompany.Domain.Shared;

public class FilterConditions<TEntity> : List<Expression<Func<TEntity, bool>>>;

public static class Filter
{
    public static OrderCondition<TEntity> GenerateOrder<TEntity>(Expression<Func<TEntity, object>> expression,
        FilterOrderBy orderBy = FilterOrderBy.Ascending) => new(expression, orderBy);


    public static FilterConditions<TEntity> GenerateConditions<TEntity>() => [];

    public static OrderConditions<TEntity> GenerateOrders<TEntity>() => [];
}


public class OrderCondition<TEntity>(
    Expression<Func<TEntity, object>> expression,
    FilterOrderBy orderBy = FilterOrderBy.Ascending)
{
    public FilterOrderBy Order { get; set; } = orderBy;

    public Expression<Func<TEntity, object>> Expression { get; private set; } = expression;
}

public class OrderConditions<TEntity> : List<OrderCondition<TEntity>>
{
    public void Add(Expression<Func<TEntity, object>> expression,
        FilterOrderBy orderBy = FilterOrderBy.Ascending)
        => this.Add(new OrderCondition<TEntity>(expression, orderBy));
}



public enum FilterOrderBy : byte
{
    [Display(Name = "قدیمی ترین")]
    Ascending = 1,
    
    [Display(Name = "جدیدترین")]
    Descending = 0
}