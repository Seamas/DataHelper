using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataHelper;
using Shouldly;

namespace DataHelperTest
{
    /// <summary>
    /// InnerTypeConverterTest 的摘要说明
    /// </summary>
    [TestClass]
    public class InnerTypeConverterTest
    {
        [TestMethod]
        public void TestDouble()
        {
            double result = (double)TypeConverter.ChangeType(10, typeof(double));
            result.ShouldBe(10);
            result = (double)TypeConverter.ChangeType("10", typeof(double));
            result.ShouldBe(10);
        }

        [TestMethod]
        public void TestInt32()
        {
            int result = (int)TypeConverter.ChangeType(10, typeof(int));
            result.ShouldBe(10);
            result = (int)TypeConverter.ChangeType(10.2, typeof(int));
            result.ShouldBe(10);
        }
    }
}
