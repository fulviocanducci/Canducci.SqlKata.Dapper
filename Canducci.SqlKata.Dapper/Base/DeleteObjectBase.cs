using System.Data;
using System.Threading.Tasks;
using Canducci.SqlKata.Dapper.Internals;
using Dapper;
using SqlKata;
using SqlKata.Compilers;

namespace Canducci.SqlKata.Dapper.Base
{
    internal abstract class DeleteObjectBase<T> : BaseBuilder
    {
        #region properties
        protected T Model { get; set; }
        #endregion

        #region construct
        internal DeleteObjectBase(IDbConnection connection, Compiler compiler)
            : base(connection, compiler)
        {
            InitDeleteObjectBase(default(T));
        }

        public DeleteObjectBase(IDbConnection connection, Compiler compiler, T model)
            : base(connection, compiler)
        {
            InitDeleteObjectBase(model);
        }
        #endregion

        #region utils
        private void InitDeleteObjectBase(T model)
        {
            Model = model;
            if (Query == null)
            {
                Query = new Query();
            }
        }
        #endregion

        #region save
        internal SqlResult GetSqlResultFromQuery()
        {
            DescribeObject<T> describe = DescribeObject<T>.Create(Model);
            if (describe.IsHavePrimaryKey)
            {
                Query.From(describe.TableFrom.Name).AsDelete().Where(describe.IdName, describe.Id.GetValue(Model));
                return Compile(Query);
            }
            return null;
        }
        public bool Save()
        {
            SqlResult compiler = GetSqlResultFromQuery();
            if (compiler != null)
            {
                return (Connection.Execute(compiler.Sql, compiler.Bindings) > 0);
            }
            return false;
        }

        public async Task<bool> SaveAsync()
        {
            SqlResult compiler = GetSqlResultFromQuery();
            if (compiler != null)
            {
                return (await Connection.ExecuteAsync(compiler.Sql, compiler.Bindings) > 0);
            }
            return false;
        }
        #endregion
    }
}
