using System.Data;
using System.Threading.Tasks;
using Canducci.SqlKata.Dapper.Internals;
using Dapper;
using SqlKata;
using SqlKata.Compilers;

namespace Canducci.SqlKata.Dapper.Base
{
    internal abstract class UpdateObjectBase<T> : BaseBuilder
    {
        #region properties
        protected T Model { get; set; }
        #endregion

        #region construct
        internal UpdateObjectBase(IDbConnection connection, Compiler compiler)
            : base(connection, compiler)
        {
            InitUpdateObjectBase(default(T));
        }

        public UpdateObjectBase(IDbConnection connection, Compiler compiler, T model)
            : base(connection, compiler)
        {
            InitUpdateObjectBase(model);
        }
        #endregion

        #region utils
        private void InitUpdateObjectBase(T model)
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
            if (describe.IsHavePrimaryKey && !describe.IsAutoIncrement)
            {
                if (describe.Items.ContainsKey(describe.IdName))
                {
                    describe.Items.Remove(describe.IdName);
                }                
            }
            Query.From(describe.TableFrom.Name).AsUpdate(describe.Items).Where(describe.IdName, describe.Id.GetValue(Model));
            return Compile(Query);            
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
                return (await Connection.ExecuteAsync(compiler.Sql, compiler.Bindings)) > 0;
            }
            return false;
        }
        #endregion
    }
}
