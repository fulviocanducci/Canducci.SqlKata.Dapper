using Canducci.SqlKata.Dapper.Base;
using SqlKata.Compilers;
using System.Data;
namespace Canducci.SqlKata.Dapper.SqlServer
{
    internal sealed class UpdateObject<T> : UpdateObjectBase<T>
    {
        public UpdateObject(IDbConnection connection, Compiler compiler, T model)
            : base(connection, compiler, model)
        {
        }
    }
}
