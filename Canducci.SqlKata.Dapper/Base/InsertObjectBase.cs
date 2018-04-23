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
        public T Save()
        {
            DescribeObject<T> describe = DescribeObject<T>.Create(Model);
            Query.From(describe.TableFrom.Name).AsInsert(describe.Items);
            SqlResult compiler = Compile(Query);
            string Sql = compiler.Sql + ((describe.IsAutoIncrement) ? GetCommandSqlGeneratedId(describe.IdName) : "");
            if (describe.IsAutoIncrement)
            {
                object value = Connection.ExecuteScalar(Sql, compiler.Bindings);
                describe.Id.SetValue(describe.Model, Convert.ChangeType(value, describe.IdType));
            }
            else
            {
                if (Connection.Execute(Sql, compiler.Bindings) == 0)
                {
                    throw new Exception("No insert row");
                }
            }            
            return Model;
        }

        //public async Task<T> SaveAsync()
        //{
        //    DescribeObject<T> describe = DescribeObject<T>.Create(Model);            
        //    SqlResult compiler = Compiler(Query.From(describe.TableFrom.Name).AsInsert(describe.Items));
        //    string Sql = compiler.Sql + ((describe.IsAutoIncrement) ? GetCommandSqlGeneratedId() : "");
        //    if (describe.IsAutoIncrement)
        //    {
        //        object value = await connection.ExecuteScalarAsync(Sql, compiler.Bindings);
        //        describe.Id.SetValue(describe.Model, Convert.ChangeType(value, describe.IdType));
        //    }
        //    else
        //    {
        //        await connection.ExecuteAsync(Sql, compiler.Bindings);
        //    }            
        //    return Model;
        //}
        #endregion
    }
}
