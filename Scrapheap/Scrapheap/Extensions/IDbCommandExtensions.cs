using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scrapheap.Extensions
{
    public static class IDbCommandExtensions
    {

        public static IEnumerable<T> GetEnumerable<T>(this IDbCommand command)
        {
            return GetEnumerable<T>(command.ExecuteReader());
        }

        public static IEnumerable<T> GetEnumerable<T>(IDataReader dataReader)
        {
            Dictionary<string, Action<T, object>> assignmentMap = GetMapping<T>();

            T instance;
            while (dataReader.Read())
            {
                instance = Activator.CreateInstance<T>();

                for (int i = 0; i < dataReader.FieldCount; ++i)
                {
                    assignmentMap[dataReader.GetName(i)](instance, dataReader.GetValue(i));
                }

                yield return instance;
            }
        }

        private static Dictionary<string, Action<T, object>> GetMapping<T>()
        {
            Dictionary<string, Action<T, object>> mapping = new Dictionary<string, Action<T, object>>();

            Type typeofT = typeof(T);

            foreach (PropertyInfo propertyInfo in typeofT.GetProperties().Where(p => p.CanWrite))
            {
                var setter = propertyInfo.GetSetMethod();


                mapping.Add(propertyInfo.Name, delegate (T instance, object val)
                {
                    setter.Invoke(instance, new object[] { val });
                });


                //ParameterExpression instance = Expression.Parameter(typeofT, "instance");
                //ParameterExpression parameter = Expression.Parameter(propertyInfo.PropertyType, "param");

                //MethodCallExpression body = Expression.Call(instance, propertyInfo.GetSetMethod(), parameter);
                //ParameterExpression[] parameters = new ParameterExpression[] { instance, parameter };

                //var lambda = Expression.Lambda<Action<T, string>>(body, parameters).Compile();




            }

            return mapping;
        }
    }

    public static class ExpressionUtils
    {
        public static PropertyInfo GetProperty<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            var member = GetMemberExpression(expression).Member;
            var property = member as PropertyInfo;
            if (property == null)
            {
                throw new InvalidOperationException(string.Format("Member with Name '{0}' is not a property.", member.Name));
            }
            return property;
        }

        private static MemberExpression GetMemberExpression<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            MemberExpression memberExpression = null;
            if (expression.Body.NodeType == ExpressionType.Convert)
            {
                var body = (UnaryExpression)expression.Body;
                memberExpression = body.Operand as MemberExpression;
            }
            else if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = expression.Body as MemberExpression;
            }

            if (memberExpression == null)
            {
                throw new ArgumentException("Not a member access", "expression");
            }

            return memberExpression;
        }

        public static Action<TEntity, TProperty> CreateSetter<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            PropertyInfo propertyInfo = ExpressionUtils.GetProperty(property);

            ParameterExpression instance = Expression.Parameter(typeof(TEntity), "instance");
            ParameterExpression parameter = Expression.Parameter(typeof(TProperty), "param");

            var body = Expression.Call(instance, propertyInfo.GetSetMethod(), parameter);
            var parameters = new ParameterExpression[] { instance, parameter };

            return Expression.Lambda<Action<TEntity, TProperty>>(body, parameters).Compile();
        }

        public static Func<TEntity, TProperty> CreateGetter<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            PropertyInfo propertyInfo = ExpressionUtils.GetProperty(property);

            ParameterExpression instance = Expression.Parameter(typeof(TEntity), "instance");

            var body = Expression.Call(instance, propertyInfo.GetGetMethod());
            var parameters = new ParameterExpression[] { instance };

            return Expression.Lambda<Func<TEntity, TProperty>>(body, parameters).Compile();
        }

        public static Func<TEntity> CreateDefaultConstructor<TEntity>()
        {
            var body = Expression.New(typeof(TEntity));
            var lambda = Expression.Lambda<Func<TEntity>>(body);

            return lambda.Compile();
        }
    }
}
