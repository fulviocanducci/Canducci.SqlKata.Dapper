using Canducci.SqlKata.Dapper.Base;
using Dapper;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
namespace Canducci.SqlKata.Dapper
{
    public sealed class UpdateBuilder : BaseBuilder
    {
        #region properties
        private Dictionary<string, object> Items;
        private Query Query;
        #endregion

        #region construct
        public UpdateBuilder(IDbConnection connection, Compiler compiler)
            : base(connection, compiler)
        {
        }
        public UpdateBuilder(IDbConnection connection, Compiler compiler, string table)
            : base(connection, compiler)
        {
            InitUpdateBuilder(table);
        }
        #endregion

        #region utils
        private void InitUpdateBuilder(string table)
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
        public UpdateBuilder From(string table)
        {
            Query.From(table);
            return this;
        }
        #endregion

        #region set

        public UpdateBuilder Set<T>(string name, T value)
        {
            Items.Add(name, value);
            return this;
        }

        public UpdateBuilder SetNull(string name)
        {
            Items.Add(name, null);
            return this;
        }

        public UpdateBuilder Set(IReadOnlyDictionary<string, object> data)
        {
            data.AsList()
                .ForEach(x =>
                {
                    Items.Add(x.Key, x.Value);
                });
            return this;
        }

        public UpdateBuilder Set(IEnumerable<string> columns, IEnumerable<object> values)
        {
            if (columns.Count() != values.Count())
            {
            }
            var c = columns.ToList();
            var v = values.ToList();
            for (int i = 0; i < columns.Count(); i++)
            {
                Items.Add(c[i], v[i]);
            }
            return this;
        }

        public UpdateBuilder Set(object anonymousType)
        {
            PropertyInfo[] properties = anonymousType.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Items.Add(property.Name, property.GetValue(anonymousType));
            }
            return this;
        }

        #endregion

        #region where
        public UpdateBuilder WhereRaw(string sql, params object[] bindings)
        {
            Query.WhereRaw(sql, bindings);
            return this;
        }
        public UpdateBuilder Where<T>(string column, string op, T value)
        {
            Query.Where(column, op, value);            
            return this;
        }

        public UpdateBuilder Where<T>(string column, T value)
        {
            Query.Where(column, value);            
            return this;
        }

        public UpdateBuilder WhereNot<T>(string column, T value)
        {
            Query.WhereNot(column, value);
            return this;
        }

        public UpdateBuilder WhereNot<T>(string column, string op, T value)
        {
            Query.WhereNot(column, op, value);            
            return this;
        }

        public UpdateBuilder Where(Func<Query, Query> callback)
        {
            Query.Where(callback);
            return this;
        }

        public UpdateBuilder WhereNull(string column)
        {
            Query.WhereNull(column);
            return this;
        }

        public UpdateBuilder WhereNotNull(string column)
        {
            Query.WhereNotNull(column);
            return this;
        }
        #endregion

        #region save
        public int Save()
        {
            if (Items?.Count == 0)
            {

            }
            SqlResult compiler = Compiler(Query.AsUpdate(Items));
            return connection.Execute(compiler.Sql, compiler.Bindings);
        }

        public Task<int> SaveAsync()
        {
            if (Items?.Count == 0)
            {

            }
            SqlResult compiler = Compiler(Query.AsUpdate(Items));
            return connection.ExecuteAsync(compiler.Sql, compiler.Bindings);
        }
        #endregion
    }
}
