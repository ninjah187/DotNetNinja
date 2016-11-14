using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;

namespace DotNetNinja.Wpf.Mvvm
{
    static class InjectExtensions
    {
        // caches to make use of reflection less efficiency-painful:
        // tuples contain Type instance and target member name
        static Dictionary<Tuple<Type, string>, FieldInfo> _fieldsCache = new Dictionary<Tuple<Type, string>, FieldInfo>();
        static Dictionary<Tuple<Type, string>, PropertyInfo> _propertiesCache = new Dictionary<Tuple<Type, string>, PropertyInfo>();

        public static object InjectPrivateField(this object o, string name, object value)
        {
            var type = o.GetType();

            var field = GetPrivateField(type, name);

            field.SetValue(o, value);

            return o;
        }

        static FieldInfo GetPrivateField(Type type, string fieldName)
        {
            var tuple = new Tuple<Type, string>(type, fieldName);

            if (_fieldsCache.ContainsKey(tuple))
            {
                return _fieldsCache[tuple];
            }

            while (type != typeof(object))
            {
                var field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);

                if (field != null)
                {
                    _fieldsCache.Add(tuple, field);
                    return field;
                }

                type = type.BaseType;
            }

            return null;
        }

        public static object InjectProperty(this object o, string name, object value)
        {
            var type = o.GetType();

            var property = GetProperty(type, name);

            property.SetValue(o, value);

            return o;
        }

        //public static T InjectProperty<T>(this T o, string name, object value)
        //{
        //    o.InjectProperty(name, value);
        //    return o;
        //}

        public static T Inject<T, TProperty>(this T o, Expression<Func<T, TProperty>> propertyExpr, TProperty value)
        {
            o.InjectProperty(propertyExpr.ExtractPropertyName(), value);
            return o;
        }

        //public static T Inject<T>(this T o, Expression<Func<T, object>> propertyExpr, object value)
        //{
        //    o.InjectProperty(propertyExpr.ExtractPropertyName(), value);
        //    return o;
        //}

        static PropertyInfo GetProperty(Type type, string propertyName)
        {
            var tuple = new Tuple<Type, string>(type, propertyName);

            if (_propertiesCache.ContainsKey(tuple))
            {
                return _propertiesCache[tuple];
            }

            while (type != typeof(object))
            {
                var property = type.GetProperty(propertyName);

                if (property != null)
                {
                    _propertiesCache.Add(tuple, property);
                    return property;
                }

                type = type.BaseType;
            }

            return null;
        }
    }
}
