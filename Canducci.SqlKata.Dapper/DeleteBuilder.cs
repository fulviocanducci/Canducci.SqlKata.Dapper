using Canducci.SqlKata.Dapper.Base;
using Dapper;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
namespace Canducci.SqlKata.Dapper
{
    public sealed class DeleteBuilder : BaseBuilder
    {
        #region properties
        //private Dictionary<string, object> Items;
        private Query Query;
        #endregion

        #region construct
        public DeleteBuilder(IDbConnection connection, Compiler compiler) 
            : base(connection, compiler)
        {
        }
        public DeleteBuilder(IDbConnection connection, Compiler compiler, string table)
            : base(connection, compiler)
        {
            InitDeleteBuilder(table);
        }

        #endregion

        #region utils
        private void InitDeleteBuilder(string table)
        {
            //if (Items == null)
            //{
            //    Items = new Dictionary<string, object>();
            //}
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
        public DeleteBuilder From(string table)
        {
            Query.From(table);
            return this;
        }
        #endregion

        #region where
        public DeleteBuilder WhereRaw(string sql, params object[] bindings)
        {
            Query.WhereRaw(sql, bindings);
            return this;
        }
        public DeleteBuilder Where<T>(string column, string op, T value)
        {
            Query.Where(column, op, value);
            return this;
        }

        public DeleteBuilder Where<T>(string column, T value)
        {
            Query.Where(column, value);
            return this;
        }

        public DeleteBuilder WhereNot<T>(string column, T value)
        {
            Query.WhereNot(column, value);
            return this;
        }

        public DeleteBuilder WhereNot<T>(string column, string op, T value)
        {
            Query.WhereNot(column, op, value);
            return this;
        }

        public DeleteBuilder Where(Func<Query, Query> callback)
        {
            Query.Where(callback);
            return this;
        }

        public DeleteBuilder WhereNull(string column)
        {
            Query.WhereNull(column);
            return this;
        }

        public DeleteBuilder WhereNotNull(string column)
        {
            Query.WhereNotNull(column);
            return this;
        }

        #endregion

        #region save
        public int Save()
        {            
            SqlResult compiler = Compiler(Query.AsDelete());
            return connection.Execute(compiler.Sql, compiler.Bindings);
        }

        public Task<int> SaveAsync()
        {           
            SqlResult compiler = Compiler(Query.AsDelete());
            return connection.ExecuteAsync(compiler.Sql, compiler.Bindings);
        }
        #endregion

    }
}
