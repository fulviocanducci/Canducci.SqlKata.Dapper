﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Canducci.SqlKata.Dapper;
using Canducci.SqlKata.Dapper.MySql;
using MySql.Data.MySqlClient;
using Models;
using System.Collections.Generic;
using System;
using Dapper;

namespace UnitTestProject
{
    [TestClass]
    [TestCategory("MySQL")]
    public class UnitTestMysql
    {
        public MySqlConnection Database { get; set; }
        public string StrConnection
        {
            get
            {
                return "Server=localhost;Database=testunit;Uid=root;Pwd=senha;SslMode=none";
            }
        }

        [TestInitialize]        
        public void UnitTestMysqlInitialize()
        {
            if (Database == null)
            {
                Database = new MySqlConnection(StrConnection);                
            }
        }

        ~UnitTestMysql()
        {
            Database?.Dispose();
        }

        [TestMethod]
        public void TestMethod1InsertBuilder()
        {
            Database.Execute("TRUNCATE TABLE people");

            int ret1 = Database.Insert("people")
                .Set(new
                {
                    name = "name 1",
                    createdat = DateTime.Now.AddDays(-100),
                    active = true
                })
                .Save();

            int ret2 = Database.Insert("people")
                .Set(new Dictionary<string, object>
                {
                    ["name"] = "name 2",
                    ["createdat"] = DateTime.Now.AddDays(-100),
                    ["active"] = true,
                })
                .Save();

            int ret3 = Database.Insert("people")
                .Set(
                        new string[] { "name", "createdat", "active" },
                        new object[] { "name 3", DateTime.Now.AddDays(-100), true }
                    )
                .Save();

            int ret4 = Database.Insert("people")
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
        public void TestMethod2QueryListOfPeople()
        {
            var items = Database
                .Query("people")
                .OrderBy("id")
                .List<People>();

            Assert.IsInstanceOfType(items, typeof(IEnumerable<People>));
            Assert.IsNotNull(items);            
        }

        [TestMethod]
        public void TestMethod3UpdateBuilder()
        {
            int ret1 = Database.Update("people")
                .Set(new
                {
                    name = "name 1",
                    createdat = DateTime.Now.AddDays(-100),
                    active = false
                })
                .Where("id", 1)
                .Save();

            int ret2 = Database.Update("people")
                .Set(new Dictionary<string, object>
                {
                    ["name"] = "name 2",
                    ["createdat"] = DateTime.Now.AddDays(-100),
                    ["active"] = false,
                })
                .Where("id", 2)
                .Save();

            int ret3 = Database.Update("people")
                .Set(
                        new string[] { "name", "createdat", "active" },
                        new object[] { "name 3", DateTime.Now.AddDays(-100), false }
                    )
                    .Where("id", 3)
                .Save();

            int ret4 = Database.Update("people")
                .Set("name", "name 4")
                .Set("createdat", DateTime.Now.AddDays(-100))
                .Set("active", false)
                .Where("id", 4)
                .Save();

            Assert.IsTrue(ret1 == 1);
            Assert.IsTrue(ret2 == 1);
            Assert.IsTrue(ret3 == 1);
            Assert.IsTrue(ret4 == 1);
        }

        [TestMethod]
        public void TestMethod4QueryListOfPeople()
        {
            var item = Database
                .Query("people")
                .Where("id", 1)
                .First<People>();

            Assert.IsNotNull(item);
            Assert.IsInstanceOfType(item, typeof(People));            
        }
        
        [TestMethod]
        public void TestMethod5DeleteBuilder()
        {
            int ret1 = Database.Delete("people")               
               .Where("id", 1)               
               .Save();

            int ret2 = Database.Delete("people")                
                .Where("id", 2)
                .Save();

            int ret3 = Database.Delete("people")
                .Where("id", 3)
                .Save();

            int ret4 = Database.Delete("people")                
                .Where("id", 4)
                .Save();

            Assert.IsTrue(ret1 == 1);
            Assert.IsTrue(ret2 == 1);
            Assert.IsTrue(ret3 == 1);
            Assert.IsTrue(ret4 == 1);
        }
    }
}
