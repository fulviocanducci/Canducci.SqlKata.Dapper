using SqlKata;
using SqlKata.Compilers;
using System;
using System.Data;
namespace Canducci.SqlKata.Dapper.Base
{
    public abstract class BaseBuilderQuery : Query
    {
        #region properties
        protected IDbConnection Connection { get; set; }
        protected Compiler Compiler { get; set; }
        #endregion

        #region construct
        public BaseBuilderQuery(IDbConnection connection, Compiler compiler)
        {
            InitBaseBuilderQuery(connection, compiler);
        }

        public BaseBuilderQuery(IDbConnection connection, Compiler compiler, string table)
            :base(table)
        {
            InitBaseBuilderQuery(connection, compiler);
        }
        #endregion

        #region Compiler
        protected virtual SqlResult Compile(Query query)
        {
            return Compiler.Compile(query);
        }
        #endregion

        #region Init
        protected void InitBaseBuilderQuery(IDbConnection connection, Compiler compiler)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(Connection));
            Compiler = compiler ?? throw new ArgumentNullException(nameof(Compiler));
        }
        #endregion Init
    }
}
