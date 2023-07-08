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
            throw new Exception("Sort. MethodCallExpression null");

        var method = methodCallExpression.Method.GetGenericMethodDefinition();
        var genericSortMethod = method.MakeGenericMethod(typeof(T), prop.Type);
        return (IOrderedQueryable<T>)genericSortMethod.Invoke(query, new object[] { query, sortLambda });
    }
}