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
        
        protected override string GetCommandSqlGeneratedId(params object[] values)
        {            
            object id = values.Length > 0 ? values[0] : "id";
            return $" RETURNING {id}";
        }
    }
}
