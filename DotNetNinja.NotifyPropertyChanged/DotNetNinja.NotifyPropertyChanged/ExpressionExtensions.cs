using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNetNinja.NotifyPropertyChanged
{
    static class ExpressionExtensions
    {
        /// <summary>
        /// Extracts property name from Expression if its body is MemberExpression, otherwise throws ArgumentException.
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static string ExtractPropertyName<TProperty>(this Expression<TProperty> expr)
        {
            var memberExpression = expr.Body as MemberExpression;

            if (memberExpression == null)
            {
                throw new ArgumentException($"Expression's body of {nameof(expr)} must be a {nameof(MemberExpression)}", nameof(expr));
            }

            return memberExpression.Member.Name;
        }
    }
}
