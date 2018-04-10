using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DataHelper
{
    internal class PropertyHelper
    {
        public static PropertyInfo[] GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
        }

        public static PropertyInfo[] SetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);
        }

        public static PropertyInfo GetProperty(Type type, string propertyName)
        {
            return type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.IgnoreCase);
        }

        public static PropertyInfo SetProperty(Type type, string propertyName)
        {
            return type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.IgnoreCase);
        }

        public static void SetValue(PropertyInfo property, object obj, object value)
        {
            try
            {
                var valueForType = TypeConverter.ChangeType(value, property?.PropertyType);
                property?.SetValue(obj, valueForType);
            }
            catch (Exception ex)
            {
            }
            
        }
    }
}
