using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    static class ExpressionExtensions
    {
        public static string ExtractPropertyName<T>(this Expression<T> expr)
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
