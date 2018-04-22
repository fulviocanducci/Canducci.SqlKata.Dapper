using SqlKata;
using SqlKata.Compilers;
using System;
using System.Data;
namespace Canducci.SqlKata.Dapper.Base
{
    public abstract class BaseBuilderQuery : Query
    {
        #region properties
        protected IDbConnection connection;
        protected Compiler compiler;
        #endregion

        #region construct
        public BaseBuilderQuery(IDbConnection connection, Compiler compiler)
        {
            Init(connection, compiler);
        }

        public BaseBuilderQuery(IDbConnection connection, Compiler compiler, string table)
            :base(table)
        {
            Init(connection, compiler);
        }
        #endregion

        #region Compiler
        protected SqlResult Compiler(Query query) 
            => compiler.Compile(query);
        #endregion

        #region Init
        protected void Init(IDbConnection connection, Compiler compiler)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
            this.compiler = compiler ?? throw new ArgumentNullException(nameof(compiler));
        }
        #endregion Init
    }
}
