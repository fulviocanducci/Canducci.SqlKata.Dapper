using SqlKata.Compilers;
using System.Data;
namespace Canducci.SqlKata.Dapper.SqlServer
{
    internal class InsertObject<T> : InsertObjectBase<T>
    {
        public InsertObject(IDbConnection connection, Compiler compiler, T model)
            : base(connection, compiler, model)
        {
        }

        protected override string GetCommandSqlGeneratedId(params object[] values)
        {
            //;SELECT CAST(SCOPE_IDENTITY() AS INT);
            //;SELECT CAST(SCOPE_IDENTITY() AS BIGINT);
            return "SELECT SCOPE_IDENTITY()";
        }
    }
}
