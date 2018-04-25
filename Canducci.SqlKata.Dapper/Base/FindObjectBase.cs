using System.Data;
using System.Threading.Tasks;
using Canducci.SqlKata.Dapper.Internals;
using Dapper;
using SqlKata;
using SqlKata.Compilers;
namespace Canducci.SqlKata.Dapper.Base
{
    internal abstract class FindObjectBase<T> : BaseBuilder
    {
        #region properties
        protected object Id { get; set; }
        #endregion

        #region construct
        internal FindObjectBase(IDbConnection connection, Compiler compiler) 
            : base(connection, compiler)
        {
            InitFindObjectBase(default(T));
        }

        public FindObjectBase(IDbConnection connection, Compiler compiler, object id) 
            : base(connection, compiler)
        {
            InitFindObjectBase(id);
        }
        #endregion

        #region utils
        private void InitFindObjectBase(object id)
        {
            Id = id;
            if (Query == null)
            {
                Query = new Query();
            }
        }
        #endregion

        #region get
        internal SqlResult GetSqlResultFromQuery()
        {
            DescribeObject<T> describe = DescribeObject<T>.Create(System.Activator.CreateInstance<T>());
            if (describe.IsHavePrimaryKey)
            {
                Query.From(describe.TableFrom.Name).Where(describe.IdName, Id);
                return Compile(Query);
            }
            return null;
        }
        public T Get()
        {
            SqlResult compiler = GetSqlResultFromQuery();
            if (compiler != null)
            {
                return Connection.QueryFirstOrDefault<T>(compiler.Sql, compiler.Bindings);
            }
            return default(T);
        }

        public async Task<T> GetAsync()
        {
            SqlResult compiler = GetSqlResultFromQuery();
            if (compiler != null)
            {
                return await Connection.QueryFirstOrDefaultAsync<T>(compiler.Sql, compiler.Bindings);
            }
            return default(T);
        }
        #endregion
    }
}
