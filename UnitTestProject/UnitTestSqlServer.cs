using Canducci.SqlKata.Dapper;
using Canducci.SqlKata.Dapper.SqlServer;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestSqlServer
    {
        public SqlConnection Database { get; set; }
        public string StrConnection
        {
            get
            {
                return "Server=.\\SqlExpress;Database=TestUnit;User Id=sa;Password=senha;MultipleActiveResultSets=true;";
            }
        }

        [TestInitialize]
        public void UnitTestSqlServerInitialize()
        {
            if (Database == null)
            {
                Database = new SqlConnection(StrConnection);
            }
        }

        ~UnitTestSqlServer()
        {
            Database?.Dispose();
        }

        public void InitTable() 
            => Database.Execute("TRUNCATE TABLE People");

        [TestMethod]
        public void TestMethodSqlServer1InsertBuilder()
        {
            InitTable();

            int ret1 = Database.Insert("People")
                .Set(new
                {
                    name = "name 1",
                    createdat = DateTime.Now.AddDays(-100),
                    active = true
                })
                .Save();

            int ret2 = Database.Insert("People")
                .Set(new Dictionary<string, object>
                {
                    ["name"] = "name 2",
                    ["createdat"] = DateTime.Now.AddDays(-100),
                    ["active"] = true,
                })
                .Save();
            
            int ret3 = Database.Insert("People")
                .Set(
                        new string[] { "name", "createdat", "active" },
                        new object[] { "name 3", DateTime.Now.AddDays(-100), true }
                    )
                .Save();

            int ret4 = Database.Insert("People")
                .Set("name", "name 4")
                .Set("createdat", DateTime.Now.AddDays(-100))
                .Set("active", true)
                .Save();

            Assert.IsTrue(ret1 == 1);
            Assert.IsTrue(ret2 == 1);
            Assert.IsTrue(ret3 == 1);
            Assert.IsTrue(ret4 == 1);
        }

        [TestMethod]
        public void TestMethodSqlServer2QueryListOfPeople()
        {
            var items = Database
                .Query("People")
                .OrderBy("Id")
                .List<People>();

            Assert.IsInstanceOfType(items, typeof(IEnumerable<People>));
            Assert.IsNotNull(items);
        }

        [TestMethod]
        public void TestMethodSqlServer3UpdateBuilder()
        {
            int ret1 = Database.Update("People")
                .Set(new
                {
                    name = "name 1",
                    createdat = DateTime.Now.AddDays(-100),
                    active = false
                })
                .Where("Id", 1)
                .Save();

            int ret2 = Database.Update("People")
                .Set(new Dictionary<string, object>
                {
                    ["name"] = "name 2",
                    ["createdat"] = DateTime.Now.AddDays(-100),
                    ["active"] = false,
                })
                .Where("Id", 2)
                .Save();

            int ret3 = Database.Update("People")
                .Set(
                        new string[] { "name", "createdat", "active" },
                        new object[] { "name 3", DateTime.Now.AddDays(-100), false }
                    )
                    .Where("Id", 3)
                .Save();

            int ret4 = Database.Update("People")
                .Set("name", "name 4")
                .Set("createdat", DateTime.Now.AddDays(-100))
                .Set("active", false)
                .Where("Id", 4)
                .Save();

            Assert.IsTrue(ret1 == 1);
            Assert.IsTrue(ret2 == 1);
            Assert.IsTrue(ret3 == 1);
            Assert.IsTrue(ret4 == 1);
        }

        [TestMethod]
        public void TestMethodSqlServer4QueryListOfPeople()
        {
            var item = Database
                .Query("People")
                .Where("Id", 1)
                .First<People>();

            Assert.IsNotNull(item);
            Assert.IsInstanceOfType(item, typeof(People));
        }

        [TestMethod]
        public void TestMethodSqlServer5DeleteBuilder()
        {
            int ret1 = Database.Delete("People")
               .Where("Id", 1)
               .Save();

            int ret2 = Database.Delete("People")
                .Where("Id", 2)
                .Save();

            int ret3 = Database.Delete("People")
                .Where("Id", 3)
                .Save();

            int ret4 = Database.Delete("People")
                .Where("Id", 4)
                .Save();

            Assert.IsTrue(ret1 == 1);
            Assert.IsTrue(ret2 == 1);
            Assert.IsTrue(ret3 == 1);
            Assert.IsTrue(ret4 == 1);
        }

        [TestMethod]
        public void TestMethodSqlServer6InsertObject()
        {
            InitTable();

            People p = new People
            {
                Active = true,
                CreatedAt = DateTime.Now.AddDays(-5),
                Name = "Insert 1"
            };

            var p0 = Database.Insert(p);
            Assert.AreEqual(p0, p);
            Assert.IsTrue(p.Id > 0);
            Assert.IsTrue(p0.Id > 0);
            Assert.IsTrue(p0.Id == 1);
            Assert.AreEqual(p0.Id, p.Id);
            Assert.IsInstanceOfType(p, typeof(People));
            Assert.IsInstanceOfType(p0, typeof(People));
            Assert.IsNotNull(p);
            Assert.IsNotNull(p0);
        }

        [TestMethod]
        public void TestMethodSqlServer7UpdateObject()
        {
            People p = Database.Find<People>(1);
            p.Name = "Insert 2";
            var ret1 = Database.Update(p);

            Assert.IsTrue(ret1);
            Assert.IsTrue(p.Id == 1);            
            Assert.IsInstanceOfType(p, typeof(People));            
            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void TestMethodSqlServer8DeleteObject()
        {
            People p = Database.Find<People>(1);
            var ret1 = Database.Delete(p);
            Assert.IsTrue(ret1);            
        }
    }
}
