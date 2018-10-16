using System;
using System.Collections.Generic;
using System.Text;

namespace Seamas.DataHelper
{
    public static class TypeConverter
    {
        /// <summary>
        /// 把 value 转换成T类型
        /// </summary>
        /// <param name="value">原始对象的值</param>
        /// <typeparam name="T">目标类型</typeparam>
        /// <returns></returns>
        public static T ChangeType<T>(object value)
        {
            return (T)ChangeType(value, typeof(T));
        }

        public static object ChangeType(object value, Type type)
        {
            if (type == null) return value;
            if (type != typeof(string) && (value == null || string.IsNullOrEmpty(value.ToString())))
            {
                value = null;
            }
            
            switch (value)
            {
                case null when type.IsGenericType:
                    return Activator.CreateInstance(type);
                case null:
                    return null;
            }

            if (type == value.GetType()) return value;
            if (type.IsEnum)
            {
                if (value is string s)
                    return Enum.Parse(type, s);
                
                return Enum.ToObject(type, value);
            }
            if (!type.IsInterface && type.IsGenericType)
            {
                var innerType = type.GetGenericArguments()[0];
                var innerValue = ChangeType(value, innerType);
                return Activator.CreateInstance(type, innerValue);
            }
            
            switch (value)
            {
                case string s when type == typeof(Guid):
                    return new Guid(s);
                case string s1 when type == typeof(Version):
                    return new Version(s1);
            }

            return !(value is IConvertible) ? value : Convert.ChangeType(value, type);
        }
    }
}
