namespace Canducci.SqlKata.Dapper
{
    public class ResultInsert<T>
    {
        public T LastInsertId { get; set; } = default(T);
        public bool Status { get; set; } = false;
    }
}
