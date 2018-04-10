using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;

namespace DataHelper
{
    public static class DataTableExtension
    {
        public static List<T> MapToObject<T>(this DataTable table)
        {
            List<T> list = new List<T>();
            foreach(DataRow dr in table.Rows)
            {
                list.Add(dr.MapToObject<T>());
            }
            return list;
        }
    }
}
