using System;
using System.Data.SqlClient;
using Canducci.SqlKata.Dapper;
using System.Data;
using Models;
using System.Collections.Generic;
using System.Linq;
using SqlKata.Compilers;    
using MySql.Data.MySqlClient;
using Canducci.SqlKata.Dapper.Extensions.SoftBuilder;
using  Canducci.SqlKata.Dapper.MySql;
namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //POSTGRESQL TEST
                string strConnection = "Server=localhost;Database=testdb;Uid=root;Pwd=senha;SslMode=none";                        
                Compiler compiler = new MySqlCompiler();

                MySqlConnection connection = new MySqlConnection(strConnection);

                var multiple = connection.MultipleBuild();
                multiple.Clear();

                var resultInsert = multiple
                    .AddInsert<int>(x => x.From("test").AsInsert(new { name = Guid.NewGuid().ToString() }, false))
                    .AddInsert<int>(x => x.From("test").AsInsert(new { name = Guid.NewGuid().ToString() }, false))
                    .AddInsert<int>(x => x.From("test").AsInsert(new { name = Guid.NewGuid().ToString() }, false))
                    .AddInsert<int>(x => x.From("test").AsInsert(new { name = Guid.NewGuid().ToString() }, true))
                    .ExecuteMultiple()
                    .ToList();

                var a = resultInsert;
                
                multiple.Clear();
                var resultSelect = multiple
                    .AddSelect<Test>(x => x.From("test").Where("id", 1))
                    .AddSelect<Test>(x => x.From("test").Where("id", 2))
                    .AddSelect<Test>(x => x.From("test").Where("id", 3))
                    .AddSelect<Test>(x => x.From("test").Where("id", 4))
                    .ExecuteMultiple()
                    .ToList();

                var b = resultSelect;

                foreach(IResultItems item in resultSelect)
                {
                    var ab = item.Value;
                    var c = ab.GetType();
                    foreach(Test t in item.GetValue<IEnumerable<object>>())
                    {                        
                        System.Console.WriteLine("{0:000} {1}", t.Id, t.Name);
                    }
                    System.Console.WriteLine("-----------------------------------------------------");
                }

                multiple.Clear();
                var resultUpdate = multiple
                    .AddUpdate(x => x.From("test").Where("id", 1).AsUpdate(new { name = Guid.NewGuid().ToString() }))
                    .AddUpdate(x => x.From("test").Where("id", 2).AsUpdate(new { name = Guid.NewGuid().ToString() }))
                    .AddUpdate(x => x.From("test").Where("id", 3).AsUpdate(new { name = Guid.NewGuid().ToString() }))
                    .AddUpdate(x => x.From("test").Where("id", 4).AsUpdate(new { name = Guid.NewGuid().ToString() }))
                    .ExecuteMultiple()
                    .ToList();

                multiple.Clear();
                var resultDelete = multiple
                    .AddDelete(x => x.From("test").Where("id", 1).AsDelete())                    
                    .ExecuteMultiple()
                    .ToList();

                //var resultsI = connection.MultipleBuild().AddInsert<int>(x => x.From("")).AddInsert<int>(x => x.From("")).ExecuteMultiple();
                //var resultsD = connection.MultipleBuild().AddDelete(x => x.From("")).ExecuteMultiple();
                //var resultsS = connection.MultipleBuild().AddSelect<int>(x => x.From("")).ExecuteMultiple();
                //var resultsU = connection.MultipleBuild().AddUpdate(x => x.From("")).ExecuteMultiple();


                //.AddInsert<int>(x => x.From("test").AsInsert(new { name = Guid.NewGuid().ToString() }, true))
                //.AddInsert<int>(x => x.From("test").AsInsert(new { name = Guid.NewGuid().ToString() }, true))
                //.AddSelect<Test>(x => x.From("test").OrderBy("id"))

                ///*.AddQuery<int>(x => x.From("test").AsInsert(new { name = Guid.NewGuid().ToString() }, true))
                //.AddQuery<int>(x => x.From("test").AsInsert(new { name = Guid.NewGuid().ToString() }, true))
                //.AddQuery<int>(x => x.From("test").AsInsert(new { name = Guid.NewGuid().ToString() }, true))*/
                ////.AddUpdate(x => x.From("test").Where("id", 1).AsUpdate(new { name = Guid.NewGuid().ToString() }))
                //.ExecuteMultiple()
                //.ToList();


                var aba = connection.SoftBuild();
                    //    .From("credit")                        
                    //    .AsInsert(new Dictionary<string, object>
                    //    {
                    //        ["description"] = "desc1",
                    //        ["created"] = DateTime.Now.AddDays(-3)
                    //    }, true);
                    //    var result = a.SaveInsert<int>();
                    //var b = result;

                    //var c = new QueryBuilderSoftDapper(connection, compiler);

                    //var reader = c.QueryBuilderMultipleCollection()
                    //    .AddQuery(x => x.From("credit").OrderBy("id"))
                    //    .AddQuery(x => x.From("credit").OrderBy("description"))
                    //    .AddQuery(x => x.From("credit").Where("id", 1))
                    //    .AddQuery(x => x.From("credit").AsCount())
                    //    .Results();

                    //IList<Credit> credit0 = reader.Read<Credit>().ToList();
                    //IList<Credit> credit1 = reader.Read<Credit>().ToList();
                    //Credit credit3 = reader.ReadFirst<Credit>();
                    //int count = reader.ReadFirst<int>();


                    //var b = 10;

                    //var peoples = r.Read<People>();
                    //var credits = r.Read<Credit>();
                    //int countPeople = r.ReadFirst<int>();

                    //var result = c.AddQuery<Credit>(x =>
                    //    x.From("Credit")
                    //        .AsInsert(new Dictionary<string, object>
                    //        {
                    //            ["Description"] = "Testando 123",
                    //            ["Created"] = DateTime.Now.AddDays(-1)
                    //        })
                    //    )
                    //    .AddQuery<Credit>(x =>
                    //    x.From("Credit")
                    //        .AsInsert(new Dictionary<string, object>
                    //        {
                    //            ["Description"] = "Testando 345",
                    //            ["Created"] = DateTime.Now.AddDays(-2)
                    //        }))
                    //        .Results();

                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Execução finalizada com sucesso !!!");
            Console.WriteLine("");
            Console.ReadKey();
        }

        public static void Clean()
        {
            //MYSQLSERVER TEST
            //string strConnection = "Server=localhost;Database=testdb;Uid=root;Pwd=senha;SslMode=none";
            //Compiler compiler = new MySqlCompiler();

            //SQLSERVER TEST
            string strConnection = "Server=.\\SqlExpress;Database=QueryBuilderDatabase;User Id=sa;Password=senha;MultipleActiveResultSets=true;";
            Compiler compiler = new SqlServerCompiler();

            //POSTGRESQL TEST
            //string strConnection = "Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;Password=senha;";                        
            //Compiler compiler = new PostgresCompiler();

            //using (MySqlConnection connection = new MySqlConnection(strConnection))
            using (IDbConnection connection = new SqlConnection(strConnection))
            //using (NpgsqlConnection connection = new NpgsqlConnection(strConnection))
            {

                //var db = new QueryBuilderDapper(connection, compiler, "People");
                /*
                var result0 = db.Select("*")
                    .Where("Id", "IN", x => x.From("Bank").Select("PeopleId"))
                    .Query();
               */

                //var result1 = db.Join("Bank", "Bank.PeopleId", "People.Id")
                //    .Query();

                //var result2 = db
                //    .GroupBy("PeopleId")                    
                //    .From()
                //    .SelectRaw("PeopleId, Count(Id) as Quant")
                //    .Query();


                //var result3 = connection
                //    .SoftBuild("Bank")
                //    .List<dynamic>();
                //.From(x => x.From("Bank").Where("PeopleId", ">", 0), "b")                                        


                //var r = db.Insert(new Dictionary<string, object>
                //{
                //    ["Name"] = Guid.NewGuid().ToString(),
                //    ["Created"] = DateTime.Now.AddDays(-6),
                //    ["Active"] = false
                //})
                //.SaveInsertGetByIdInserted<long>();

                //var db = new QueryBuilderDapper(connection, compiler, "Credit");
                //var r = db.Insert(new Dictionary<string, object>
                //{
                //    ["Description"] = "Credit - teste" + Guid.NewGuid().ToString(),                    
                //})




                //Console.WriteLine($"Resultado: {result.ToString()}");


            }
        }
    }
}
