using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataHelper
{
    public static class DataRowExtension
    {
        public static T MapToObject<T>(this DataRow dr)
        {
            var obj = Activator.CreateInstance<T>();
            return (T)MapTo(dr, obj);
        }

        public static object MapToObject(this DataRow dr, object obj)
        {
            return MapTo(dr, obj);
        }

        private static object MapTo(DataRow dr, object obj)
        {
            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                string columnName = dr.Table.Columns[i].ColumnName;
                var property = PropertyHelper.SetProperty(obj.GetType(), columnName);
                PropertyHelper.SetValue(property, obj, dr[i]);
            }
            return obj;
        }
    }
}
