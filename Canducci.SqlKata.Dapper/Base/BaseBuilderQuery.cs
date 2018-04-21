using SqlKata;
using SqlKata.Compilers;
using System;
using System.Data;
namespace Canducci.SqlKata.Dapper.Base
{
    public abstract class BaseBuilderQuery : Query
    {
        protected IDbConnection connection;
        protected Compiler compiler;        

        public BaseBuilderQuery(IDbConnection connection, Compiler compiler)
            :base()
        {
            Init(connection, compiler);
        }

        public BaseBuilderQuery(IDbConnection connection, Compiler compiler, string table)
            :base(table)
        {
            Init(connection, compiler);
        }

        #region Compiler
        protected SqlResult Compiler(Query query)
        {
            return compiler.Compile(query);
        }
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
