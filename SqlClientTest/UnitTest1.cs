using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLClientLibrary;
using System;

namespace SqlClientTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void isConnectionEstablished()
        {
            Assert.IsNotNull(SqlClient1.GetInstance().GetConnection());        
        }

        [TestMethod] 
        public void isQueryExecuted() {
            Assert.IsNotNull(SqlClient1.GetInstance().GetDataSet(@"SELECT * FROM LEARNING.DBO.PRODUCT"));
        }

        [TestMethod]
        public void validatQueryResult(){
            var resultSet = SqlClient1.GetInstance().GetDataSet(@"SELECT * FROM LEARNING.DBO.PRODUCT");
            Assert.IsTrue(SqlClient1.GetInstance().validateQueryResultDataSet(resultSet, "productName", "IPHONE12"));
        }
    }
}
