using SqlKata;
using SqlKata.Compilers;
using System;
using System.Data;
namespace Canducci.SqlKata.Dapper.Base
{
    public abstract class BaseBuilder: IDisposable
    {
        #region properties
        protected IDbConnection connection;
        protected Compiler compiler;
        #endregion

        #region construct
        public BaseBuilder(IDbConnection connection, Compiler compiler)
            => Init(connection, compiler);
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

        #region dispose
        public void Dispose()
        {
            connection?.Dispose();
            compiler = null;
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
