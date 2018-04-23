using Canducci.SqlKata.Dapper.Base;
using SqlKata.Compilers;
using System.Data;
namespace Canducci.SqlKata.Dapper
{    
    internal class DeleteObject<T> : DeleteObjectBase<T>
    {
        public DeleteObject(IDbConnection connection, Compiler compiler, T model)
            : base(connection, compiler, model)
        {
        }
    }
}
