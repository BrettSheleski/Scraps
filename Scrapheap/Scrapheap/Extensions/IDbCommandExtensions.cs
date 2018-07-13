using System;
using System.Collections;
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



        public static T Execute<T>(this IDbCommand command)
        {
            return (T)command.ExecuteScalar();
        }



        public static List<T> ToList<T>(this IDbConnection connection, string commandText)
        {
            using (var command = connection.CreateCommand(commandText))
            using (ConnectionOpener.Open(connection))
            {
                return command.ToList<T>();
            }
        }

        public static List<T> ToList<T>(this IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                return reader.ToList<T>();
            }
        }

        public static List<T> ToList<T>(this IDataReader dataReader)
        {
            return dataReader.GetEnumerable<T>().ToList();
        }

        public static IEnumerable<T> GetEnumerable<T>(this IDataReader dataReader)
        {
            Dictionary<string, Action<T, object>> assignmentMap = GetMapping<T>();

            T instance;
            string fieldName;
            while (dataReader.Read())
            {
                instance = Activator.CreateInstance<T>();

                for (int i = 0; i < dataReader.FieldCount; ++i)
                {
                    fieldName = dataReader.GetName(i);
                    if (assignmentMap.ContainsKey(fieldName))
                    {
                        assignmentMap[fieldName](instance, dataReader.GetValue(i));
                    }
                }

                yield return instance;
            }
        }

        private static Dictionary<string, Action<T, object>> GetMapping<T>()
        {
            Dictionary<string, Action<T, object>> mapping = new Dictionary<string, Action<T, object>>();

            Type typeofT = typeof(T);
            string fieldName;
            Action<T, object> setterAction;
            foreach (PropertyInfo propertyInfo in typeofT.GetProperties().Where(p => p.CanWrite))
            {
                MethodInfo setter = propertyInfo.GetSetMethod();

                if (setter.IsPublic)
                {
                    fieldName = propertyInfo.GetCustomAttribute<BindFieldToAttribute>()?.FieldName ?? propertyInfo.Name;

                    setterAction = GetDelegate<T>(setter);

                    mapping.Add(fieldName, setterAction);

                    //mapping.Add(fieldName, delegate (T instance, object val)
                    //{
                    //    setter.Invoke(instance, new object[] { DbNullToNull(val) });
                    //});
                }
            }

            return mapping;
        }

        public static Action<T, object> GetDelegate<T>(MethodInfo method)
        {
            Action<T, string> act = GetDelegate<T, string>(method);

            return (instancs, p) => act(instancs, (string)p);
        }



        private static Action<T, TParm> GetDelegate<T, TParm>(MethodInfo method)
        {
            return (Action<T, TParm>)Delegate.CreateDelegate(typeof(Action<T, TParm>), method);
        }

        static object DbNullToNull(object obj)
        {
            if (obj is DBNull)
                return null;

            return obj;
        }
    }


    public static class IDbConnectionExtensions
    {
        public static IDbCommand CreateCommand(this IDbConnection connection, string commandText)
        {
            var command = connection.CreateCommand();

            command.CommandText = commandText;

            return command;
        }
    }
}
