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
            int id = 0;
            string strConnection = "Server=.\\SqlExpress;Database=QueryBuilderDatabase;User Id=sa;Password=senha;MultipleActiveResultSets=true;";
            using (SqlConnection connection = new SqlConnection(strConnection))
            {

                var db = new QueryBuilderDapper(connection, new SqlServerCompiler(), "People");
                db
                    .Insert(new Dictionary<string, object>
                    {
                        ["Name"] = Guid.NewGuid().ToString(),
                        ["Created"] = DateTime.Now.AddDays(-1000),
                        ["Active"] = true
                    })                    
                    .Save(out id);

            }
            Console.WriteLine($"Id inserido: {id}");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Execução finalizada com sucesso !!!");
            Console.WriteLine("");
            Console.ReadKey();
        }
    }
}
