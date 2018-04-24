using Canducci.SqlKata.Dapper.Base;
using Canducci.SqlKata.Dapper.Internals;
using Dapper;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Data;
using System.Threading.Tasks;
namespace Canducci.SqlKata.Dapper
{
    internal abstract class InsertObjectBase<T>: BaseBuilder
    {
        #region properties
        protected T Model { get; set; }                
        #endregion

        #region abstract_method
        protected abstract string GetCommandSqlGeneratedId(params object[] values);
        #endregion

        #region construct
        internal InsertObjectBase(IDbConnection connection, Compiler compiler)
            : base(connection, compiler)
        {
            InitInsertObjectBase(default(T));
        }

        public InsertObjectBase(IDbConnection connection, Compiler compiler, T model) 
            : base(connection, compiler)
        {
            InitInsertObjectBase(model);
        }
        #endregion

        #region utils
        private void InitInsertObjectBase(T model)
        {
            Model = model;
            if (Query == null)
            {
                Query = new Query();
            }
        }
        #endregion

        #region save
        internal SqlResult GetSqlResultFromQuery(DescribeObject<T> describe)
        {            
            Query.From(describe.TableFrom.Name).AsInsert(describe.Items);            
            SqlResult c0 = Compile(Query);
            string Sql = c0.RawSql + ((describe.IsAutoIncrement) ? GetCommandSqlGeneratedId(describe.IdName) : "");
            return new SqlResult(Sql, c0.RawBindings);            
        }

        public T Save()
        {
            DescribeObject<T> describe = DescribeObject<T>.Create(Model);
            SqlResult compiler = GetSqlResultFromQuery(describe);            
            if (describe.IsAutoIncrement)
            {
                object value = Connection.ExecuteScalar(compiler.Sql, compiler.Bindings);
                describe.Id.SetValue(describe.Model, Convert.ChangeType(value, describe.IdType));
            }
            else
            {
                if (Connection.Execute(compiler.Sql, compiler.Bindings) == 0)
                {
                    throw new Exception("No insert row");
                }
            }            
            return Model;
        }

        public async Task<T> SaveAsync()
        {
            DescribeObject<T> describe = DescribeObject<T>.Create(Model);
            SqlResult compiler = GetSqlResultFromQuery(describe);
            if (describe.IsAutoIncrement)
            {
                object value = await Connection.ExecuteScalarAsync(compiler.Sql, compiler.Bindings);
                describe.Id.SetValue(describe.Model, Convert.ChangeType(value, describe.IdType));
            }
            else
            {
                if (await Connection.ExecuteAsync(compiler.Sql, compiler.Bindings) == 0)
                {
                    throw new Exception("No insert row");
                }
            }
            return Model;
        }
        #endregion
    }
}
