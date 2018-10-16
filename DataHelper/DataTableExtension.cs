using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using System.Linq;

namespace Seamas.DataHelper
{
    public static class DataTableExtension
    {
        public static List<T> MapToTypeWithCache<T>(this DataTable table)
        {
            var propertyNames = GetColumnNames(table);
            var type = typeof(T);
            var dictionary = PropertyHelper.SetMapping(type, propertyNames);
            
            var list = new List<T>();
            foreach(DataRow dr in table.Rows)
            {
                list.Add(dr.MapWithDictionary<T>(dictionary));
            }
            return list;
        }

        public static List<T> MapToType<T>(this DataTable table)
        {
            var list = new List<T>();
            foreach(DataRow dr in table.Rows)
            {
                list.Add(dr.MapTo<T>());
            }
            return list;
        }

        private static IEnumerable<string> GetColumnNames(DataTable table)
        {
            foreach (DataColumn column in table.Columns)
            {
                yield return column.ColumnName;
            }
        }
    }
}
