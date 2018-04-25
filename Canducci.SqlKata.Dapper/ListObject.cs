using Canducci.SqlKata.Dapper.Base;
using SqlKata.Compilers;
using System.Data;
namespace Canducci.SqlKata.Dapper
{
    internal class ListObject<T> : ListObjectBase<T>
    {
        public ListObject(IDbConnection connection, Compiler compiler) 
            : base(connection, compiler)
        {
        }
    }
}
