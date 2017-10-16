using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Canducci.SqlKata.Dapper.SoftExtensions;
using Canducci.SqlKata.Dapper;
using SqlKata.Compilers;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {            
            string strConnection = "Server=.\\SqlExpress;Database=QueryBuilderDatabase;User Id=sa;Password=senha;MultipleActiveResultSets=true;";
            using (SqlConnection connection = new SqlConnection(strConnection))
            {

                //var db = new QueryBuilderDapper(connection, new SqlServerCompiler(), "People");
                //var r = db.Insert(new Dictionary<string, object>
                //{
                //    ["Name"] = Guid.NewGuid().ToString(),
                //    ["Created"] = DateTime.Now.AddDays(-1000),
                //    ["Active"] = true
                //})
                //    .SaveInsert<int>();

                var db = new QueryBuilderDapper(connection, new SqlServerCompiler(), "Credit");
                var r = db.Insert(new Dictionary<string, object>
                {
                    ["Description"] = Guid.NewGuid().ToString()
                })
                .SaveInsert();

                Console.WriteLine($"Id inserido: {r}");

            }
            
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Execução finalizada com sucesso !!!");
            Console.WriteLine("");
            Console.ReadKey();
        }
    }
}
