using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace PUCMinasTCC.FrameworkDAO
{
    public interface IDataContext
    {
        string ConnectionString { get; set; }
        int CommandTimeout { get; set; }
        void StartCommand(string query, CommandType cmdTipo);
        void StartCommand(string query, CommandType cmdTipo, DbTransaction transaction);
        DbTransaction BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void AddParameters(string paramName, object value, DbType type);
        void ExecuteNonQuery();
        IDataReader ExecuteReader();
        T ExecuteScalar<T>();
        DataSet ExecuteDataSet();
        DataSet ExecuteDataSet(string tableName);
        DataSet ExecuteDataSet<T>() where T : new();
        void AbrirConexao();
        void FecharConexao();
    }
}
