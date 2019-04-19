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
        //IEnumerable<IResult> IQueryBuilderMultipleExecuteDapper.ExecuteMultiple()
        //{
        //    return ExecuteMultiple();
        //}

        //async Task<IEnumerable<IResult>> IQueryBuilderMultipleExecuteDapper.ExecuteMultipleAsync()
        //{
        //    return await ExecuteMultipleAsync();
        //}


        IEnumerable<IResultItems> IQueryBuilderMultipleExecuteDapper<IResultItems>.ExecuteMultiple()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<IResultItems>> IQueryBuilderMultipleExecuteDapper<IResultItems>.ExecuteMultipleAsync()
        {
            throw new NotImplementedException();
        }

        IEnumerable<IResultAffectedRows> IQueryBuilderMultipleExecuteDapper<IResultAffectedRows>.ExecuteMultiple()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<IResultAffectedRows>> IQueryBuilderMultipleExecuteDapper<IResultAffectedRows>.ExecuteMultipleAsync()
        {
            throw new NotImplementedException();
        }

        protected IEnumerable<TReturn> ExecuteMultiple<TReturn>()
        {
            IEnumerable<TReturn> items = null;
            if (ResultTypeGlobal == ResultType.Insert || ResultTypeGlobal == ResultType.Select)
            {
                GridReader result = GridReaderResults();
                items = GetGridReaderResultsMultiple(result);
            }
            if (ResultTypeGlobal == ResultType.Delete || ResultTypeGlobal == ResultType.Update)
            {
                items = GetAffectedRowsResultsMultiple();
            }
            return items;
        }

        protected async Task<IEnumerable<TReturn>> ExecuteMultipleAsync<TReturn>()
        {
            IEnumerable<TReturn> items = null;
            if (ResultTypeGlobal == ResultType.Insert || ResultTypeGlobal == ResultType.Select)
            {
                GridReader result = await GridReaderResultsAsync();
                items = GetGridReaderResultsMultiple(result);
            }
            if (ResultTypeGlobal == ResultType.Delete || ResultTypeGlobal == ResultType.Update)
            {
                items = await GetAffectedRowsResultsMultipleAsync();
            }
            return items;
        }
        #endregion

        #region GetResultsMultiple
        protected IEnumerable<TReturn> GetGridReaderResultsMultiple<TReturn>(GridReader gridReader)
        {
            foreach (Tuple<Type, Query, ResultType> item in Queries)
            {                
                bool ReturnId = item.Item2
                    .Clauses
                    .OfType<InsertClause>()                    
                    .Select(_ => _.ReturnId)
                    .FirstOrDefault();

                yield return new Result
                {
                    ResultType = item.Item3,
                    Value = item.Item3 == ResultType.Insert 
                        ? !gridReader.IsConsumed && ReturnId ? gridReader.ReadFirstOrDefault(item.Item1) : null
                        : !gridReader.IsConsumed ? gridReader.Read(item.Item1) : null
                };
            }
            Clear();
        }

        protected IEnumerable<TReturn> GetAffectedRowsResultsMultiple<TReturn>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            foreach (Query query in GetQueries())
            {
                SqlResult result = Compiler.Compile(query);
                int affectedRows = Connection.Execute(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType);
                yield return new Result
                {
                    AffectedRows = affectedRows,
                    Value = 0,
                    ResultType = ResultTypeGlobal
                };
            }
        }

        protected async Task<IEnumerable<Result>> GetAffectedRowsResultsMultipleAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IList<Result> results = new List<Result>();
            foreach (Query query in GetQueries())
            {
                SqlResult result = Compiler.Compile(query);
                int affectedRows = await Connection.ExecuteAsync(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType);
                results.Add(new Result
                {
                    AffectedRows = affectedRows,
                    Value = 0,
                    ResultType = ResultTypeGlobal
                });
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
    }

    public interface IResultAffectedRows : IResultBase
    {
        int AffectedRows { get; set; }        
    }

    public interface IResult : IResultItems, IResultAffectedRows { } 

    public class Result: IResult
    {
        public object Value { get; set; }
        public ResultType ResultType { get; set; }
        public int AffectedRows { get; set; } = 0;
    }
}
