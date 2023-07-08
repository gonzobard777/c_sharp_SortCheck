using System.Linq.Expressions;

namespace ConsoleApp.Database;

public static class Sort2Ext
{
    public static IQueryable<T> AddSorting2<T>(this IQueryable<T> query, string propOrFieldName, bool desc)
    {
        var param = Expression.Parameter(typeof(T));
        var prop = Expression.PropertyOrField(param, propOrFieldName);
        var sortLambda = Expression.Lambda(prop, param);

        Expression<Func<IOrderedQueryable<T>>> sortMethod;
        bool isFirstSortingCondition = query.Expression.Type != typeof(IOrderedQueryable<T>);

        if (!desc)
        {
            sortMethod = isFirstSortingCondition
                ? () => query.OrderBy<T, object>(k => null)
                : () => ((IOrderedQueryable<T>)query).ThenBy<T, object>(k => null);
        }
        else
        {
            sortMethod = isFirstSortingCondition
                ? () => query.OrderByDescending<T, object>(k => null)
                : () => ((IOrderedQueryable<T>)query).ThenByDescending<T, object>(k => null);
        }

        var methodCallExpression = sortMethod.Body as MethodCallExpression;
        if (methodCallExpression is null)
            throw new Exception("MethodCallExpression null");

        var method = methodCallExpression.Method.GetGenericMethodDefinition();
        var genericSortMethod = method.MakeGenericMethod(typeof(T), prop.Type);
        return (IOrderedQueryable<T>)genericSortMethod.Invoke(query, new object[] { query, sortLambda });
    }

public static IQueryable<T> OrderingHelper<T>(IQueryable<T> source, string propertyName, bool descending, bool anotherLevel)
{
    if (!string.IsNullOrEmpty(propertyName))
        try
        {
            ParameterExpression param = Expression.Parameter(typeof(T), string.Empty);
            MemberExpression property = Expression.PropertyOrField(param, propertyName);
            LambdaExpression sort = Expression.Lambda(property, param);

            MethodCallExpression call = Expression.Call(
                typeof(Queryable),
                (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
                new[] { typeof(T), property.Type },
                source.Expression,
                Expression.Quote(sort));
            return (IQueryable<T>)source.Provider.CreateQuery<T>(call);
        }
        catch
        {
            return null;
        }

    return null;
}
}