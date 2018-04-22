using Canducci.SqlKata.Dapper.Base;
using Dapper;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;

namespace Canducci.SqlKata.Dapper
{
    public abstract class InsertObjectBase<T>: BaseBuilder
    {
        #region properties
        private T Model { get; }
        private Dictionary<string, object> Items;
        private Query Query;
        #endregion

        #region abstract_method
        protected abstract string GetCommandSqlGeneratedId();
        #endregion

        #region construct
        internal InsertObjectBase(IDbConnection connection, Compiler compiler)
            : base(connection, compiler)
        {
        }

        public InsertObjectBase(IDbConnection connection, Compiler compiler, T model) 
            : base(connection, compiler)
        {
            Model = model;
            if (Query == null)
            {
                Query = new Query();
            }
            if (Items == null)
            {
                Items = new Dictionary<string, object>();
            }
        }
        #endregion

        #region utils_methods
        public SqlResult Generated(ref PropertyInfo Id)
        {
            Type type = Model.GetType();
            TableFromAttribute tableFrom = type.GetTypeInfo().GetCustomAttribute<TableFromAttribute>();            
            if (tableFrom != null)
            {
                Query.From(tableFrom.Name);
            }
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetCustomAttribute(typeof(AutoIncrementAttribute)) == null)
                {
                    Items.Add(property.Name, property.GetValue(Model));
                }
                else
                {
                    Id = property;
                }
            }
            return Compiler(Query.AsInsert(Items));
        }

        public void SetId(ref PropertyInfo Id, object value)
        {
            if (Id != null)
            {
                Id.SetValue(Model, Convert.ToInt32(value));
            }
        }
        #endregion

        #region save
        public T Save()
        {
            PropertyInfo Id = null;
            var compiler = Generated(ref Id);
            object value = connection.ExecuteScalar(compiler.Sql + GetCommandSqlGeneratedId(), compiler.Bindings);
            SetId(ref Id, value);
            return Model;
        }

        public async Task<T> SaveAsync()
        {
            PropertyInfo Id = null;
            var compiler = Generated(ref Id);
            object value = await connection.ExecuteScalarAsync(compiler.Sql + GetCommandSqlGeneratedId(), compiler.Bindings);
            SetId(ref Id, value);
            return Model;
        }
        #endregion
    }
}
