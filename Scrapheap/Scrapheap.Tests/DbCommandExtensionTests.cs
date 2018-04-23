using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrapheap.Extensions;

namespace Scrapheap.Tests
{
    [TestClass]
    public class DbCommandExtensionTests
    {
        [TestMethod]
        public void MaybeDoesSomething()
        {
            // Setup

            var csBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder()
            {
                IntegratedSecurity = true,
                DataSource = "AppDvSqlLi1",
                InitialCatalog = "brett_data"
            };
            IEnumerable<College> colleges;
            List<College> collegeList;

            using (var connection = new System.Data.SqlClient.SqlConnection(csBuilder.ConnectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM college";

                colleges = command.GetEnumerable<College>();

                collegeList = colleges.ToList();
            }


            // Act

            // Verify

            Assert.IsTrue(collegeList.Count > 0);
        }

        class College
        {
            public int UnitID { get; set; }
            public string name { get; set; }
            public string city { get; set; }
            public string state_code { get; set; }
        }
    }
}
