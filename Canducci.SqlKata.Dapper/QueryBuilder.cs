using Canducci.SqlKata.Dapper.Extensions.SoftBuilder;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Data;

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
        protected SqlResult Compiler<T>(string name)
            where T : struct
        {
            if (compiler is MySqlCompiler c)
            {
                return c.CompileWithLastId(this);
            }
            else if (compiler is SqlServerCompiler s)
            {
                return s.CompileWithLastId<T>(this, name: name);
            }
            else if (compiler is PostgresCompiler p)
            {
                return p.CompileWithLastId(this);
            }
            throw new Exception("Compiler");
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
