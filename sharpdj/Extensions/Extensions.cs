using System;
using System.Linq.Expressions;

namespace SharpDj.Extensions
{
    public static class Extensions
    {
        public static string GetPropertyName(this Expression<Func<object>> extension)
        {
            var unaryExpression = extension.Body as UnaryExpression;
            var memberExpression = unaryExpression != null ? (MemberExpression)unaryExpression.Operand : (MemberExpression)extension.Body;
            return memberExpression.Member.Name;
        }
    }
}
