using SqlKata;
using SqlKata.Compilers;
using System;
using System.Data;
namespace Canducci.SqlKata.Dapper.Base
{
    public abstract class BaseBuilder: IDisposable
    {
        #region properties
        protected IDbConnection Connection { get; set; }
        protected Compiler Compiler { get; set; }
        protected Query Query { get; set; }
        #endregion

        #region construct
        public BaseBuilder(IDbConnection connection, Compiler compiler)
        {
            InitBaseBuilder(connection, compiler);
        }
        #endregion

        #region Compiler
        protected virtual SqlResult Compile(Query query)
        {
            return Compiler.Compile(query);
        }
        #endregion

        #region Init
        protected void InitBaseBuilder(IDbConnection connection, Compiler compiler)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(Connection));
            Compiler = compiler ?? throw new ArgumentNullException(nameof(Compiler));            
        }
        #endregion Init

        #region dispose
        public void Dispose()
        {
            Connection?.Dispose();
            Compiler = null;
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
