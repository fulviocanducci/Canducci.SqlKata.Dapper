using SqlKata;
using SqlKata.Compilers;
using System;
using System.Data;

namespace Canducci.SqlKata.Dapper.Base
{
    public abstract class BaseBuilder
    {
        protected IDbConnection connection;
        protected Compiler compiler;
        protected string table;

        public BaseBuilder(IDbConnection connection, Compiler compiler)
        {
            Init(connection, compiler);
        }

        public BaseBuilder(IDbConnection connection, Compiler compiler, string table)            
        {
            Init(connection, compiler, table);
        }

        #region Compiler
        protected SqlResult Compiler(Query query)
        {
            return compiler.Compile(query);
        }
        #endregion

        #region Init
        protected void Init(IDbConnection connection, Compiler compiler, string table = "")
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
            this.compiler = compiler ?? throw new ArgumentNullException(nameof(compiler));
        }
        #endregion Init
    }

    
}
