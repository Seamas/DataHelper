using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using Seamas.DataHelper;
using Shouldly;

namespace DataHelperTest
{
    [TestClass]
    public class DataRowTest
    {
        [TestMethod]
        public void TestMapToObject()
        {
            var table = CreateDataTable();
            DataRow dr = table.Rows[0];
            var product = dr.MapToObject<Product>();
            product.Name.ShouldBe("apple");
            product.Price.ShouldBe(10.2);
            product.NotMap.ShouldBe(20);

            var tmp = new Product() { Name = "pear", Price = 30.2, NotMap = 15 };
            dr.MapToObject(tmp);
            tmp.Name.ShouldBe("apple");
            tmp.Price.ShouldBe(10.2);
            tmp.NotMap.ShouldBe(15);
        }

        private DataTable CreateDataTable()
        {
            var table = new DataTable();
            table.Columns.Add(new DataColumn("name", typeof(string)));
            table.Columns.Add(new DataColumn("price", typeof(string)));
            var row = table.NewRow();
            row[0] = "apple";
            row[1] = "10.2";
            table.Rows.Add(row);
            return table;
        }
    }
}
