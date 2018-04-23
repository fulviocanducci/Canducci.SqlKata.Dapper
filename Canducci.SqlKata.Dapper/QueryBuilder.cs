using Canducci.SqlKata.Dapper.Base;
using SqlKata;
using SqlKata.Compilers;
using System.Data;
namespace Canducci.SqlKata.Dapper
{
    public partial class QueryBuilder : BaseBuilderQuery
    {
        #region construct
        public QueryBuilder(IDbConnection connection, Compiler compiler) 
            : base(connection, compiler)
        {
        }

        public QueryBuilder(IDbConnection connection, Compiler compiler, string table) 
            : base(connection, compiler, table)
        {            
        }
        #endregion  

        #region Compiler        
        protected virtual SqlResult Compile()
        {            
            return Compiler.Compile(this);
        }
        #endregion
    }
}
