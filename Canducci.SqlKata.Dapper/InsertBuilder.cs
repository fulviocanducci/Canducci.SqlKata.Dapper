using System;
using System.Collections.Generic;
using System.Data;
using SqlKata.Compilers;
using Dapper;
using SqlKata;
using System.Threading.Tasks;
using Canducci.SqlKata.Dapper.Base;

namespace Canducci.SqlKata.Dapper
{
    public class InsertBuilder : BaseBuilder
    {
        private Dictionary<string, object> Items;        
        private Query Query;

        public InsertBuilder(IDbConnection connection, Compiler compiler) 
            : base(connection, compiler)
        {
            InitInsertBuilder();            
        }

        public InsertBuilder(IDbConnection connection, Compiler compiler, string table) 
            : base(connection, compiler, table)
        {            
            InitInsertBuilder(table);
        }

        private void InitInsertBuilder(string table = "")
        {
            if (Items == null)
            {
                Items = new Dictionary<string, object>();
            }
            if (Query == null)
            {
                Query = new Query();
            }
            if (!string.IsNullOrEmpty(table))
            {
                From(table);
            }
        }

        public InsertBuilder Set<T>(string name, T value)
        {
            Items.Add(name, value);
            return this;
        }

        public InsertBuilder SetNull(string name)
        {
            Items.Add(name, null);
            return this;
        }   
        
        public InsertBuilder From(string table)
        {
            Query.From(table);
            return this;
        }

        public int Save()
        {            
            if (string.IsNullOrEmpty(table))
            {

            }
            if (Items?.Count == 0)
            {

            }
            SqlResult compiler = Compiler(Query.AsInsert(Items));
            return connection.Execute(compiler.Sql, compiler.Bindings);
        }

        public Task<int> SaveAsync()
        {
            if (string.IsNullOrEmpty(table))
            {

            }
            if (Items?.Count == 0)
            {

            }
            SqlResult compiler = Compiler(Query.AsInsert(Items));
            return connection.ExecuteAsync(compiler.Sql, compiler.Bindings);
        }
    }
}
