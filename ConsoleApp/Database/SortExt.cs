using System.Linq.Expressions;

namespace ConsoleApp.Database;

public static class SortExt
{
    // public virtual IQueryable<T> SortDefault(IQueryable<T> query) => query.OrderBy(x => x.Id);
    
    /// <summary>
    /// Универсальная сортировка.
    /// Сортировку по умолчанию надо реализовывать, переопределяя метод BaseService.SortDefault
    /// </summary>
    /// <param name="query">Объект запроса.</param>
    /// <param name="propOrFieldName">Регистронезависимое название поля, по которому надо произвести сортировку.</param>
    /// <param name="desc">Направление сортировки.</param>
    /// <returns>Запрос с сортировкой.</returns>
    public static IQueryable<T> AddSorting<T>(this IQueryable<T> query, string propOrFieldName, SortDirection sortDirection)
    {
        Type propType = null;
        LambdaExpression sortLambda = null;
        try
        {
            var param = Expression.Parameter(typeof(T));
            var prop = Expression.PropertyOrField(param, propOrFieldName);
            propType = prop.Type;
            sortLambda = Expression.Lambda(prop, param);
        }
        catch (Exception e)
        {
            // при попытке сортировать по несуществующему свойству
            // return SortDefault(query);
        }

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
        var genericSortMethod = method.MakeGenericMethod(typeof(T), propType);
        return (IOrderedQueryable<T>)genericSortMethod.Invoke(query, new object[] { query, sortLambda });
    }
}