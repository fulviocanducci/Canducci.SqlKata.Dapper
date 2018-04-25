using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Canducci.SqlKata.Dapper.Internals;
using Dapper;
using SqlKata;
using SqlKata.Compilers;
namespace Canducci.SqlKata.Dapper.Base
{
    internal abstract class ListObjectBase<T> : BaseBuilder
    {
        #region properties
        protected T Model { get; set; }
        #endregion

        #region construct
        public ListObjectBase(IDbConnection connection, Compiler compiler) 
            : base(connection, compiler)
        {
            InitListObjectBase(default(T));
        }
        #endregion

        #region utils
        private void InitListObjectBase(T model)
        {
            Model = (model == null) ? Activator.CreateInstance<T>() : model;
            if (Query == null)
            {
                Query = new Query();
            }
        }
        #endregion

        #region List
        internal SqlResult GetSqlResultFromQuery()
        {
            DescribeObject<T> describe = DescribeObject<T>.Create(Model);
            Query.From(describe.TableFrom.Name);
            return Compile(Query);
        }
        public IEnumerable<T> List()
        {
            SqlResult compiler = GetSqlResultFromQuery();
            return Connection.Query<T>(compiler.Sql, compiler.Bindings);
        }
        public async Task<IEnumerable<T>> ListAsync()
        {
            SqlResult compiler = GetSqlResultFromQuery();
            return await Connection.QueryAsync<T>(compiler.Sql, compiler.Bindings);
        }
        #endregion
    }
}
