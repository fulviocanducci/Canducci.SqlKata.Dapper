using Canducci.SqlKata.Dapper.Base;
using SqlKata.Compilers;
using System.Data;
namespace Canducci.SqlKata.Dapper
{
    internal class FindObject<T> : FindObjectBase<T>
    {
        public FindObject(IDbConnection connection, Compiler compiler, object id)
            : base(connection, compiler, id)
        {
        }
    }
}
