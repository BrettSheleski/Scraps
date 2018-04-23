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

            }

            return mapping;
        }
    }
    
}
