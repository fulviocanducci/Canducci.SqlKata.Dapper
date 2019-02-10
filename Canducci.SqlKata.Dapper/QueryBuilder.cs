using System;
using System.Collections.Generic;
using System.Data;
using SqlKata;
using SqlKata.Compilers;
namespace Canducci.SqlKata.Dapper
{
    public abstract class QueryBuilder: Query
    {
        protected IDbConnection connection;
        protected Compiler compiler;
        public QueryBuilder(IDbConnection connection, Compiler compiler)
        {
            Init(connection, compiler);
        }

        public QueryBuilder(IDbConnection connection, Compiler compiler, string table)
            : base(table)
        {
            Init(connection, compiler);
        }

        #region Compiler
        protected SqlResult Compiler()
        {            
            return compiler.Compile(this);
        }
        protected SqlResult Compiler(IEnumerable<Query> queries)
        {
            return compiler.Compile(queries);
        }
        protected SqlResult Compiler(params Query[] query)
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
