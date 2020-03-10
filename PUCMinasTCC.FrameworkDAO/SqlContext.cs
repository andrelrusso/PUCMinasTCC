using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace PUCMinasTCC.FrameworkDAO
{
    public class SqlContext : IDataContext, IDisposable
    {
        public string ConnectionString { get; set; }
        public int CommandTimeout { get; set; }
        private SqlConnection Connection { get; set; }
        private SqlCommand Command { get; set; }
        public SqlContext(string connectionString)
        {
            ConnectionString = connectionString;
            Command = new SqlCommand();
        }
        public void StartCommand(string query, CommandType cmdTipo)
        {
            Command = new SqlCommand();
            Command.CommandText = query;
            Command.CommandType = cmdTipo;
            Command.CommandTimeout = CommandTimeout;
        }
        public void StartCommand(string query, CommandType cmdTipo, DbTransaction transaction)
        {
            if (Command == null) Command = new SqlCommand();
            Command.CommandText = query;
            Command.CommandType = cmdTipo;
            Command.CommandTimeout = CommandTimeout;
            Command.Transaction = (SqlTransaction)transaction;
        }
        public DbTransaction BeginTransaction()
        {
            var transaction = Connection.BeginTransaction();
            if (Command != null)
            {
                Command.Transaction = transaction;
            }

            return transaction;
        }
        public void CommitTransaction()
        {
            if (Command != null)
                Command.Transaction.Commit();
        }
        public void RollbackTransaction()
        {
            if (Command != null)
                Command.Transaction.Rollback();
        }
        public void AddParameters(string paramName, object value, DbType type)
        {
            var param = new SqlParameter(paramName, type);
            param.Value = value;
            if (Command == null)
            {
                Command = new SqlCommand();
            }
            Command.Parameters.Add(param);
        }
        public void ExecuteNonQuery()
        {
            try
            {
                AbrirConexao();
                Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Parameters.Clear();
                if (Command.Transaction == null)
                    Dispose(true);
            }
        }
        public IDataReader ExecuteReader()
        {
            try
            {
                AbrirConexao();
                var reader = Command.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Parameters.Clear();
            }
        }
        public T ExecuteScalar<T>()
        {
            try
            {
                AbrirConexao();
                var result = (T)Command.ExecuteScalar();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Parameters.Clear();
                if (Command.Transaction == null)
                    Dispose(true);
            }
        }
        public DataSet ExecuteDataSet()
        {
            try
            {
                AbrirConexao();
                using (DataSet ds = new DataSet())
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(Command))
                    {
                        da.Fill(ds);
                    }
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Parameters.Clear();
                if (Command.Transaction == null)
                    Dispose(true);
            }
        }
        public DataSet ExecuteDataSet(string tableName)
        {
            try
            {
                AbrirConexao();
                using (DataSet ds = new DataSet())
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(Command))
                    {
                        da.Fill(ds, tableName);
                    }
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Parameters.Clear();
                if (Command.Transaction == null)
                    Dispose(true);
            }
        }
        public DataSet ExecuteDataSet<T>() where T : new()
        {
            try
            {
                AbrirConexao();
                using (DataSet ds = new DataSet())
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(Command))
                    {
                        string tableName = Manipulation.GetTableName<T>();
                        da.Fill(ds, tableName);
                    }
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Parameters.Clear();
                if (Command.Transaction == null)
                    Dispose(true);
            }
        }

        public void AbrirConexao()
        {
            try
            {
                if (Connection == null || Connection.State == ConnectionState.Closed)
                {
                    Connection = new SqlConnection(ConnectionString);
                    Connection.Open();
                    Command.Connection = Connection;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FecharConexao()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
                Connection.Close();
        }

        ~SqlContext()
        {
            Dispose(true);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            FecharConexao();
            Connection = null;
        }

    }
}
