using System.Linq.Expressions;

namespace ConsoleApp.Database;

public static class SortExt
{
    public static IQueryable<T> AddSorting<T>(this IQueryable<T> query, string propOrFieldName, SortDirection sortDirection)
    {
        var param = Expression.Parameter(typeof(T));
        var prop = Expression.PropertyOrField(param, propOrFieldName);
        var sortLambda = Expression.Lambda(prop, param);

        Expression<Func<IOrderedQueryable<T>>> sortMethod = null;

        switch (sortDirection)
        {
            case SortDirection.Ascending when query.Expression.Type == typeof(IOrderedQueryable<T>):
                sortMethod = () => ((IOrderedQueryable<T>)query).ThenBy<T, object>(k => null);
                break;
            case SortDirection.Ascending:
                sortMethod = () => query.OrderBy<T, object>(k => null);
                break;
            case SortDirection.Descending when query.Expression.Type == typeof(IOrderedQueryable<T>):
                sortMethod = () => ((IOrderedQueryable<T>)query).ThenByDescending<T, object>(k => null);
                break;
            case SortDirection.Descending:
                sortMethod = () => query.OrderByDescending<T, object>(k => null);
                break;
        }

        var methodCallExpression = (sortMethod.Body as MethodCallExpression);
        if (methodCallExpression == null)
            throw new Exception("Sort. MethodCallExpression null");

        var method = methodCallExpression.Method.GetGenericMethodDefinition();
        var genericSortMethod = method.MakeGenericMethod(typeof(T), prop.Type);
        return (IOrderedQueryable<T>)genericSortMethod.Invoke(query, new object[] { query, sortLambda });
    }
}