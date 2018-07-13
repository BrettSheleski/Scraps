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

            List<College> collegeList;

            using (var connection = new System.Data.SqlClient.SqlConnection(csBuilder.ConnectionString))
            using (ConnectionOpener.Open(connection))
            using (var command = connection.CreateCommand("SELECT * FROM college"))
            {
                // Act
                collegeList = command.ToList<College>();
            }

            // Verify
            Assert.IsTrue(collegeList.Count > 0);
        }

        [TestMethod]
        public void MaybeDoesSomething_WithGoodNames()
        {
            // Setup
            var csBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder()
            {
                IntegratedSecurity = true,
                DataSource = "AppDvSqlLi1",
                InitialCatalog = "brett_data"
            };
            
            List<CollegeWithGoodNames> collegeList;

            using (var connection = new System.Data.SqlClient.SqlConnection(csBuilder.ConnectionString))
            using (ConnectionOpener.Open(connection))
            using (var command = connection.CreateCommand("SELECT * FROM college"))
            {
                // Act
                collegeList = command.ToList<CollegeWithGoodNames>();
            }

            // Verify
            Assert.IsTrue(collegeList.Count > 0);
        }

        [TestMethod]
        public void UseConnectionShorthand()
        {
            // Setup
            var csBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder()
            {
                IntegratedSecurity = true,
                DataSource = "AppDvSqlLi1",
                InitialCatalog = "brett_data"
            };

            List<CollegeWithGoodNames> collegeList;

            using (var connection = new System.Data.SqlClient.SqlConnection(csBuilder.ConnectionString))
            {
                // Act
                collegeList = connection.ToList<CollegeWithGoodNames>("SELECT * FROM college");
            }

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

        class CollegeWithGoodNames
        {
            [BindFieldTo("UnitID")]
            public int Id { get; set; }

            [BindFieldTo("name")]
            public string Name { get; set; }

            [BindFieldTo("city")]
            public string City { get; set; }

            [BindFieldTo("state_code")]
            public string StateCode { get; set; }

            public string ThisPropertyDoesNotExistInDb { get; set; }

            [BindFieldTo("null_column")]
            public string NullColumn { get; set; }
        }
    }
}
