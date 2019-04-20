using Dapper;
using System;
using System.Data;
using SqlKata;
using System.Threading.Tasks;
using System.Collections.Generic;
using SqlKata.Compilers;
using static Dapper.SqlMapper;
using System.Linq;

namespace Canducci.SqlKata.Dapper
{
    public interface IQueryBuilderMultipleExecuteDapper<TResult>
    {
        IEnumerable<TResult> ExecuteMultiple();
        Task<IEnumerable<TResult>> ExecuteMultipleAsync();
    }

    public interface IQueryBuilderMultipleSelectDapper: IQueryBuilderMultipleExecuteDapper<IResultItems>
    {
        IQueryBuilderMultipleSelectDapper AddSelect<TReturn>(Query query);
        IQueryBuilderMultipleSelectDapper AddSelect<TReturn>(Func<Query, Query> query);
    }

    public interface IQueryBuilderMultipleInsertDapper : IQueryBuilderMultipleExecuteDapper<IResultItems>
    {
        IQueryBuilderMultipleInsertDapper AddInsert<TReturn>(Query query);
        IQueryBuilderMultipleInsertDapper AddInsert<TReturn>(Func<Query, Query> query);
    }

    public interface IQueryBuilderMultipleUpdateDapper: IQueryBuilderMultipleExecuteDapper<IResultAffectedRows>
    {
        IQueryBuilderMultipleUpdateDapper AddUpdate(Query query);
        IQueryBuilderMultipleUpdateDapper AddUpdate(Func<Query, Query> query);
    }

    public interface IQueryBuilderMultipleDeleteDapper: IQueryBuilderMultipleExecuteDapper<IResultAffectedRows>
    {
        IQueryBuilderMultipleDeleteDapper AddDelete(Query query);
        IQueryBuilderMultipleDeleteDapper AddDelete(Func<Query, Query> query);
    }

    public class QueryBuilderMultipleDapper :
        IQueryBuilderMultipleSelectDapper,
        IQueryBuilderMultipleUpdateDapper,
        IQueryBuilderMultipleDeleteDapper,
        IQueryBuilderMultipleInsertDapper,
        IDisposable
    {
        private IList<Tuple<Type,Query,ResultType>> Queries { get; set; }
        private IDbConnection Connection { get; set; }
        private Compiler Compiler { get; set; }
        private ResultType ResultTypeGlobal { get; set; } = ResultType.None;

        #region Constructor
        public QueryBuilderMultipleDapper(IDbConnection connection, Compiler compiler)            
        {
            Clear();
            Connection = connection;
            Compiler = compiler;
        }
        #endregion Constructor

        #region Add
        protected QueryBuilderMultipleDapper Add<TReturn>(Query query, ResultType resultType)
        {
            if (ResultTypeGlobal == ResultType.None)
            {
                ResultTypeGlobal = resultType;
            }
            Queries.Add(new Tuple<Type, Query, ResultType>(typeof(TReturn), query, resultType));
            return this;
        }
        #endregion Add

        #region AddSelect
        public IQueryBuilderMultipleSelectDapper AddSelect<TReturn>(Query query)
        {
            return Add<TReturn>(query, ResultType.Select);
        }

        public IQueryBuilderMultipleSelectDapper AddSelect<TReturn>(Func<Query, Query> query)
        {
            return AddSelect<TReturn>(query(new Query()));
        }
        #endregion AddSelect

        #region AddUpdate        
        public IQueryBuilderMultipleUpdateDapper AddUpdate(Query query)
        {
            return Add<bool>(query, ResultType.Update);
        }

        public IQueryBuilderMultipleUpdateDapper AddUpdate(Func<Query, Query> query)
        {
            return AddUpdate(query(new Query()));
        }
        #endregion AddUpdate

        #region AddDelete
        public IQueryBuilderMultipleDeleteDapper AddDelete(Query query)
        {
            return Add<bool>(query, ResultType.Delete);
        }

        public IQueryBuilderMultipleDeleteDapper AddDelete(Func<Query, Query> query)
        {
            return AddDelete(query(new Query()));
        }
        #endregion AddDelete

        #region AddInsert
        public IQueryBuilderMultipleInsertDapper AddInsert<TReturn>(Query query)
        {
            return Add<TReturn>(query, ResultType.Insert);
        }

        public IQueryBuilderMultipleInsertDapper AddInsert<TReturn>(Func<Query, Query> query)
        {
            return AddInsert<TReturn>(query(new Query()));
        }
        #endregion AddInsert

        #region Execute
        
        IEnumerable<IResultItems> IQueryBuilderMultipleExecuteDapper<IResultItems>.ExecuteMultiple()
        {
            return ExecuteMultiple<IResultItems>();
        }

        async Task<IEnumerable<IResultItems>> IQueryBuilderMultipleExecuteDapper<IResultItems>.ExecuteMultipleAsync()
        {
            return await ExecuteMultipleAsync<IResultItems>();
        }

        IEnumerable<IResultAffectedRows> IQueryBuilderMultipleExecuteDapper<IResultAffectedRows>.ExecuteMultiple()
        {
            return ExecuteMultiple<IResultAffectedRows>();
        }

        async Task<IEnumerable<IResultAffectedRows>> IQueryBuilderMultipleExecuteDapper<IResultAffectedRows>.ExecuteMultipleAsync()
        {
            return await ExecuteMultipleAsync<IResultAffectedRows>();
        }

        protected IEnumerable<TResult> ExecuteMultiple<TResult>()
        {
            IEnumerable<TResult> items = null;
            if (ResultTypeGlobal == ResultType.Insert || ResultTypeGlobal == ResultType.Select)
            {
                GridReader result = GridReaderResults();
                items = GetGridReaderResultsMultiple<TResult>(result);
            }
            if (ResultTypeGlobal == ResultType.Delete || ResultTypeGlobal == ResultType.Update)
            {
                items = GetAffectedRowsResultsMultiple<TResult>();
            }
            return items;
        }

        protected async Task<IEnumerable<TResult>> ExecuteMultipleAsync<TResult>()
        {
            IEnumerable<TResult> items = null;
            if (ResultTypeGlobal == ResultType.Insert || ResultTypeGlobal == ResultType.Select)
            {
                GridReader result = await GridReaderResultsAsync();
                items = GetGridReaderResultsMultiple<TResult>(result);
            }
            if (ResultTypeGlobal == ResultType.Delete || ResultTypeGlobal == ResultType.Update)
            {
                items = await GetAffectedRowsResultsMultipleAsync<TResult>();
            }
            return items;
        }
        #endregion

        #region GetResultsMultiple
        protected IEnumerable<TResult> GetGridReaderResultsMultiple<TResult>(GridReader gridReader)
        {
            foreach (Tuple<Type, Query, ResultType> item in Queries)
            {                
                bool ReturnId = item.Item2
                    .Clauses
                    .OfType<InsertClause>()                    
                    .Select(_ => _.ReturnId)
                    .FirstOrDefault();

                ResultItems result = new ResultItems
                {
                    ResultType = item.Item3,
                    Value = item.Item3 == ResultType.Insert
                        ? !gridReader.IsConsumed && ReturnId ? gridReader.ReadFirstOrDefault(item.Item1) : null
                        : !gridReader.IsConsumed ? gridReader.Read(item.Item1) : null
                };
                yield return ChangeTypeToType<TResult>(result);
            }
            Clear();
        }

        protected IEnumerable<TResult> GetAffectedRowsResultsMultiple<TResult>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)          
        {
            foreach (Query query in GetQueries())
            {
                SqlResult compiler = Compiler.Compile(query);
                int affectedRows = Connection.Execute(compiler.Sql, compiler.NamedBindings, transaction, commandTimeout, commandType);
                ResultAffectedRows result = new ResultAffectedRows
                {
                    AffectedRows = affectedRows,                    
                    ResultType = ResultTypeGlobal
                };
                yield return ChangeTypeToType<TResult>(result);
            }
        }

        protected async Task<IEnumerable<TResult>> GetAffectedRowsResultsMultipleAsync<TResult>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)            
        {
            IList<TResult> results = new List<TResult>();
            foreach (Query query in GetQueries())
            {
                SqlResult compiler = Compiler.Compile(query);
                int affectedRows = await Connection.ExecuteAsync(compiler.Sql, compiler.NamedBindings, transaction, commandTimeout, commandType);
                ResultAffectedRows result = new ResultAffectedRows
                {
                    AffectedRows = affectedRows,
                    ResultType = ResultTypeGlobal
                };
                results.Add(ChangeTypeToType<TResult>(result));
            }
            return results;
        }
        #endregion GetResultsMultiple

        #region GetReaderResults
        protected GridReader GridReaderResults(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = GetSqlResult();
            return Connection.QueryMultiple(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType);            
        }

        protected async Task<GridReader> GridReaderResultsAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = GetSqlResult();            
            return await Connection.QueryMultipleAsync(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType);
        }
        #endregion GetReaderResults

        protected TType ChangeTypeToType<TType>(object value)
        {
            return (TType)value;
        }

        protected SqlResult GetSqlResult()
        {
            return Compiler.Compile(GetQueries());
        }

        protected IEnumerable<Query> GetQueries()
        {
            return Queries.Select(x => x.Item2);                
        }

        public void Clear()
        {        
            if (Queries == null)
            {
                Queries = new List<Tuple<Type, Query, ResultType>>();
            }
            else
            {
                Queries.Clear();
            }
            ResultTypeGlobal = ResultType.None;
        }

        public void Dispose()
        {
            Connection?.Dispose();            
            GC.SuppressFinalize(this);
        }
        
    }

    public enum ResultType
    {
        Insert,
        Update,
        Select, 
        Delete,
        None
    }

    public interface IResultBase
    {
        ResultType ResultType { get; set; }
    }

    public interface IResultItems: IResultBase
    {
        object Value { get; set; }
        T GetValue<T>();
    }

    public interface IResultAffectedRows : IResultBase
    {
        int AffectedRows { get; set; }        
    }

    public class ResultItems : IResultItems
    {
        public object Value { get; set; }
        public ResultType ResultType { get; set; }

        public T GetValue<T>()
        {
            return (T)Value;
        }
    }

    public class ResultAffectedRows : IResultAffectedRows
    {
        public int AffectedRows { get; set; }
        public ResultType ResultType { get; set; }
    }
}
