using System;
using System.Collections.Generic;
using System.Data;
using SqlKata.Compilers;
using Dapper;
using SqlKata;
using System.Threading.Tasks;
using Canducci.SqlKata.Dapper.Base;
using System.Linq;
using System.Reflection;
namespace Canducci.SqlKata.Dapper
{
    public sealed class InsertBuilder : BaseBuilder
    {
        #region properties
        private Dictionary<string, object> Items;        
        private Query Query;
        #endregion

        #region construct
        public InsertBuilder(IDbConnection connection, Compiler compiler) 
            : base(connection, compiler)
        {
            InitInsertBuilder();            
        }

        public InsertBuilder(IDbConnection connection, Compiler compiler, string table) 
            : base(connection, compiler)
        {            
            InitInsertBuilder(table);
        }
        #endregion

        #region utils
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
        #endregion

        #region from
        public InsertBuilder From(string table)
        {
            Query.From(table);
            return this;
        }
        #endregion

        #region set

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

        public InsertBuilder Set(IReadOnlyDictionary<string, object> data)
        {
            data.AsList()
                .ForEach(x =>
                {
                    Items.Add(x.Key, x.Value);
                });            
            return this;
        }

        public InsertBuilder Set(IEnumerable<string> columns, IEnumerable<object> values)
        {
            if (columns.Count() != values.Count())
            {
            }
            var c = columns.ToList();
            var v = values.ToList();
            for (int i = 0; i< columns.Count(); i++)
            {
                Items.Add(c[i], v[i]);
            }
            return this;
        }

        public InsertBuilder Set(object anonymousType)
        {
            PropertyInfo[] properties = anonymousType.GetType().GetProperties();
            foreach(PropertyInfo property in properties)
            {
                Items.Add(property.Name, property.GetValue(anonymousType));
            }
            return this;
        }

        #endregion

        #region save
        public int Save()
        {   
            if (Items?.Count == 0)
            {
                
            }
            SqlResult compiler = Compiler(Query.AsInsert(Items));
            return connection.Execute(compiler.Sql, compiler.Bindings);
        }

        public Task<int> SaveAsync()
        {            
            if (Items?.Count == 0)
            {

            }
            SqlResult compiler = Compiler(Query.AsInsert(Items));
            return connection.ExecuteAsync(compiler.Sql, compiler.Bindings);
        }
        #endregion
    }
}
