using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Seamas.DataHelper
{
    internal class PropertyHelper
    {
        /// <summary>
        /// 获取类型的所有Get属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>Get属性数组</returns>
        public static PropertyInfo[] GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
        }

        /// <summary>
        /// 获取类型所有的Set属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>Set属性数组</returns>
        public static PropertyInfo[] SetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);
        }

        /// <summary>
        /// 根据属性名称，获取类型的Get属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>Get属性</returns>
        public static PropertyInfo GetProperty(Type type, string propertyName)
        {
//            return type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.IgnoreCase);
            var properties = GetProperties(type);
            return FindPropertyByName(properties, propertyName);
        }

        /// <summary>
        /// 根据属性名称，获取类型的Set属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>Set属性</returns>
        public static PropertyInfo SetProperty(Type type, string propertyName)
        {
//            var property = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.IgnoreCase);
            var properties = SetProperties(type);
            return FindPropertyByName(properties, propertyName);
        }

        /// <summary>
        /// 设置目标对象的属性值
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="obj">目标对象</param>
        /// <param name="value">需要赋予属性的值</param>
        public static void SetValue(PropertyInfo property, object obj, object value)
        {
            if (property == null || !property.CanWrite ) return;
            var valueForType = TypeConverter.ChangeType(value, property.PropertyType);
            property.SetValue(obj, valueForType);
        }

        /// <summary>
        /// 读取源对象的属性值
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="obj">源对象</param>
        /// <returns></returns>
        public static object GetValue(PropertyInfo property, object obj)
        {
            if (property == null || !property.CanRead) return null;
            return property.GetValue(obj);
        }

        /// <summary>
        /// 从属性数组中找出名字匹配的属性
        /// 先根据名称（忽略大小写）查询，如果没有找到
        /// 再根据 DisplayNameAttribute的 DisplayName查询
        /// </summary>
        /// <param name="propertyInfos">属性数组</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        private static PropertyInfo FindPropertyByName(PropertyInfo[] propertyInfos, string propertyName)
        {
            return propertyInfos.FirstOrDefault(item =>
                item.Name.Equals(propertyName, StringComparison.CurrentCultureIgnoreCase)
                || (item.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName
                    .Equals(propertyName, StringComparison.CurrentCultureIgnoreCase) ?? false));
        }

        /// <summary>
        ///  根据属性数组和 显示名称，生成映射关系
        /// </summary>
        /// <param name="propertyInfos">属性数组</param>
        /// <param name="propertyNames">显示名称</param>
        /// <returns></returns>
        private static Dictionary<string, PropertyInfo> Mapping(PropertyInfo[] propertyInfos,
            IEnumerable<string> propertyNames)
        {
            var dictionary = new Dictionary<string, PropertyInfo>();
            foreach (var propertyName in propertyNames.Distinct())
            {
                var propertyInfo = FindPropertyByName(propertyInfos, propertyName);
                if (propertyInfo != null)
                {
                    dictionary.Add(propertyName, propertyInfo);
                }
            }

            return dictionary;
        }

        /// <summary>
        /// 获取Set属性的映射关系
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="propertyNames">属性显示名称</param>
        /// <returns></returns>
        public static Dictionary<string, PropertyInfo> SetMapping(Type type, IEnumerable<string> propertyNames)
        {
            var propertyInfos = SetProperties(type);
            return Mapping(propertyInfos, propertyNames);
        }
        
        /// <summary>
        /// 获取Get属性的映射关系
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="propertyNames">属性显示名称</param>
        /// <returns></returns>
        public static Dictionary<string, PropertyInfo> GetMapping(Type type, IEnumerable<string> propertyNames)
        {
            var propertyInfos = GetProperties(type);
            return Mapping(propertyInfos, propertyNames);
        }

    }
}
