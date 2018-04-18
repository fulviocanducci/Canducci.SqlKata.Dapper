using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

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
        protected SqlResult Compiler(List<Query> items)
        {            
            StringBuilder sqls = new StringBuilder();
            List<object> bindings = new List<object>();
            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    var result = compiler.Compile(items[i]);
                    if (sqls.Length > 0) sqls.Append(";");
                    sqls.Append(result.RawSql);                    
                    bindings.AddRange(result.RawBindings);
                }
            }
            return new SqlResult(sqls.ToString(), bindings);
        }
        protected SqlResult Compiler(IDictionary<int, KeyValuePair<Type, Query>> items)
        {
            return Compiler(items.Values.Select(x => x.Value).ToList());
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
