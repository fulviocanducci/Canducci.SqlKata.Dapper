using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ConsoleAppTest.Models
{
    public class TestExpressions
    {
        public PropertyInfo GetName<T>(Expression<Func<T, object>> expression)
        {

            //ExpressionEqualityComparer c = new ExpressionEqualityComparer();

            MemberExpression body = expression.Body as MemberExpression;
            if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)expression.Body;
                body = ubody.Operand as MemberExpression;
            }
            var result = expression.Compile()(Activator.CreateInstance<T>());
            return body?.Member as PropertyInfo;
        }
    }
}
