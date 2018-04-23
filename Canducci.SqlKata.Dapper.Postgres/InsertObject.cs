using SqlKata.Compilers;
using System.Data;
namespace Canducci.SqlKata.Dapper.Postgres
{
    internal class InsertObject<T> : InsertObjectBase<T>
    {
        public InsertObject(IDbConnection connection, Compiler compiler, T model)
            : base(connection, compiler, model)
        {
        }

        // Postgres RETURNING id
        protected override string GetCommandSqlGeneratedId(params object[] values)
        {            
            var strNameId = values.Length > 0 ? values[0] : "id";
            return @";RETURNING {values[0]};";
        }
    }
}
