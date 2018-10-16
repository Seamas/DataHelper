using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Seamas.DataHelper
{
    public static class DataRowExtension
    {
        /// <summary>
        /// 映射成T类型的对象
        /// </summary>
        /// <param name="dr"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T MapTo<T>(this DataRow dr)
        {
            var obj = Activator.CreateInstance<T>();
            return (T)MapTo(dr, obj);
        }
        
        /// <summary>
        /// 映射到obj对象
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object MapTo(this DataRow dr, object obj)
        {
            var propertyNames = GetColumnNames(dr);
            var dictionary = PropertyHelper.SetMapping(obj.GetType(), propertyNames);
            Mapping(dr, dictionary, obj);
            return obj;
        }

        /// <summary>
        /// 根据字典进行映射
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="dictionary"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T MapWithDictionary<T>(this DataRow dr, Dictionary<string, PropertyInfo> dictionary)
        {
            var obj = Activator.CreateInstance<T>();
            Mapping(dr, dictionary, obj);
            return obj;
        }

        /// <summary>
        /// 根据字典映射到obj
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="dictionary"></param>
        /// <param name="obj"></param>
        private static void Mapping(DataRow dr, Dictionary<string, PropertyInfo> dictionary, object obj)
        {
            foreach (var kvp in dictionary)
            {
                PropertyHelper.SetValue(kvp.Value, obj, dr[kvp.Key]);
            }
        }


        /// <summary>
        /// 获取所有列名
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static IEnumerable<string> GetColumnNames(DataRow dr)
        {
            foreach (DataColumn dataColumn in dr.Table.Columns)
            {
                yield return dataColumn.ColumnName;
            }
        }
    }
}
