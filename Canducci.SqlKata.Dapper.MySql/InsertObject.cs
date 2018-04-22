using SqlKata.Compilers;
using System.Data;
namespace Canducci.SqlKata.Dapper.MySql
{
    internal class InsertObject<T> : InsertObjectBase<T>
    {
        public InsertObject(IDbConnection connection, Compiler compiler, T model) 
            : base(connection, compiler, model)
        {
        }

        // Mysql Last_Insert_Id()
        protected override string GetCommandSqlGeneratedId()
        {
            return ";SELECT CAST(LAST_INSERT_ID() AS SIGNED);";
        }
    }
}
