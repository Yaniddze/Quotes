using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace Quotes.MoexProvider.Moex.Mapper.Extensions;

public static class ExpressionExtensions
{
    public static IEnumerable<PropertyPath> GetSimplePropertyAccessList(this LambdaExpression propertyAccessExpression)
    {
        var propertyPaths
            = MatchPropertyAccessList(propertyAccessExpression, (p, e) => e.MatchSimplePropertyAccess(p));

        return propertyPaths;
    }
    
    private static IEnumerable<PropertyPath> MatchPropertyAccessList(
        this LambdaExpression lambdaExpression, Func<Expression, Expression, PropertyPath> propertyMatcher)
    {
        Debug.Assert(lambdaExpression.Body != null);

        var newExpression
            = RemoveConvert(lambdaExpression.Body) as NewExpression;

        if (newExpression != null)
        {
            var parameterExpression
                = lambdaExpression.Parameters.Single();

            var propertyPaths
                = newExpression.Arguments
                    .Select(a => propertyMatcher(a, parameterExpression))
                    .Where(p => p != null);

            if (propertyPaths.Count()
                == newExpression.Arguments.Count())
            {
                return newExpression.HasDefaultMembersOnly(propertyPaths) ? propertyPaths : null;
            }
        }

        var propertyPath = propertyMatcher(lambdaExpression.Body, lambdaExpression.Parameters.Single());

        return (propertyPath != null) ? new[] { propertyPath } : null;
    }
    
    public static Expression RemoveConvert(this Expression expression)
    {
        while (expression.NodeType == ExpressionType.Convert
               || expression.NodeType == ExpressionType.ConvertChecked)
        {
            expression = ((UnaryExpression)expression).Operand;
        }

        return expression;
    }
    
    private static bool HasDefaultMembersOnly(
        this NewExpression newExpression, IEnumerable<PropertyPath> propertyPaths)
    {
        return newExpression.Members == null
               || !newExpression.Members
                   .Where(
                       (t, i) =>
                           !string.Equals(t.Name, propertyPaths.ElementAt(i).Last().Name, StringComparison.Ordinal))
                   .Any();
    }
    
    private static PropertyPath MatchSimplePropertyAccess(
        this Expression parameterExpression, Expression propertyAccessExpression)
    {
        var propertyPath = MatchPropertyAccess(parameterExpression, propertyAccessExpression);

        return propertyPath != null && propertyPath.Count == 1 ? propertyPath : null;
    }
    
    private static PropertyPath MatchPropertyAccess(
        this Expression parameterExpression, Expression propertyAccessExpression)
    {
        var propertyInfos = new List<PropertyInfo>();

        MemberExpression memberExpression;

        do
        {
            memberExpression = RemoveConvert(propertyAccessExpression) as MemberExpression;

            if (memberExpression == null)
            {
                return null;
            }

            var propertyInfo = memberExpression.Member as PropertyInfo;

            if (propertyInfo == null)
            {
                return null;
            }

            propertyInfos.Insert(0, propertyInfo);

            propertyAccessExpression = memberExpression.Expression;
        }
        while (memberExpression.Expression != parameterExpression);

        return new PropertyPath(propertyInfos);
    }
    
    public static void Each<T>(this IEnumerable<T> ts, Action<T, int> action)
    {
        var i = 0;
        foreach (var t in ts)
        {
            action(t, i++);
        }
    }
    
    public static void Each<T>(this IEnumerable<T> ts, Action<T> action)
    {
        foreach (var t in ts)
        {
            action(t);
        }
    }

    public static void Each<T, S>(this IEnumerable<T> ts, Func<T, S> action)
    {
        foreach (var t in ts)
        {
            action(t);
        }
    }
    
    public static bool SequenceEqual<TSource>(
        this IEnumerable<TSource> source, IEnumerable<TSource> other, Func<TSource, TSource, bool> func)
        where TSource : class
    {
        return source.SequenceEqual(other, new DynamicEqualityComparer<TSource>(func));
    }
    
    public static bool IsSameAs(this PropertyInfo propertyInfo, PropertyInfo otherPropertyInfo)
    {
        return (propertyInfo == otherPropertyInfo) ||
               (propertyInfo.Name == otherPropertyInfo.Name
                && (propertyInfo.DeclaringType == otherPropertyInfo.DeclaringType
                    || propertyInfo.DeclaringType.IsSubclassOf(otherPropertyInfo.DeclaringType)
                    || otherPropertyInfo.DeclaringType.IsSubclassOf(propertyInfo.DeclaringType)
                    || propertyInfo.DeclaringType.GetInterfaces().Contains(otherPropertyInfo.DeclaringType)
                    || otherPropertyInfo.DeclaringType.GetInterfaces().Contains(propertyInfo.DeclaringType)));
    }
}