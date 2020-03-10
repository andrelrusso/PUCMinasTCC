using PUCMinasTCC.Domain.Enums;
using PUCMinasTCC.FrameworkDAO;
using PUCMinasTCC.FrameworkDAO.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PUCMinasTCC.Domain.Context
{
    public class DataContext : IDbContext
    {
        //private static Dictionary<Type, SqlDbType> TypeMap;
        private static Dictionary<Type, DbType> TypeMap;
        private List<SqlParameter> Parametros = new List<SqlParameter>();
        public IDataContext db { get; set; }
        public string Schema { get; set; } = "dbo";
        private string ConnectionString { get; set; }
        public DataContext(string connectionString)
        {
            ConnectionString = connectionString;
            CreateInstance();
            ResolveMapType();
        }

        private void CreateInstance()
        {
            try
            {
                //if (string.IsNullOrEmpty(ConnectionString))
                //    db = DatabaseFactory.CreateDatabase("CNX", DatabaseType.SqlServer);
                //else
                db = DatabaseFactory.CreateDatabase(DatabaseType.SqlServer, ConnectionString);
                db.CommandTimeout = 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Procedures

        public void InitProcedure(string name)
        {
            if (Schema.Equals("dbo"))
                db.StartCommand(name, CommandType.StoredProcedure);
            else
                db.StartCommand(string.Concat(Schema, ".", name), CommandType.StoredProcedure);
        }

        public void InitProcedure(string name, DbTransaction transaction)
        {
            if (Schema.Equals("dbo"))
                db.StartCommand(name, CommandType.StoredProcedure, transaction);
            else
                db.StartCommand(string.Concat(Schema, ".", name), CommandType.StoredProcedure, transaction);
        }

        public void AddParameter<T>(string name, T value, Func<T, bool> condition)
        {
            if (condition.Invoke(value)) AddParameter(name, value);
        }

        public void AddParameter<T>(string name, T value)
        {
            if (value != null)
            {
                if (TypeMap.ContainsKey(value.GetType()))
                    db.AddParameters(name, value, TypeMap[value.GetType()]);
                else
                    throw new ArgumentOutOfRangeException($"Valor para o parâmetro não contém um tipo conhecido");
            }
        }

        public IList<T> List<T>() where T : class, new() => db.ExecuteDataSet<T>().ToList<T>();
        public Task<IList<T>> ListAsync<T>() where T : class, new() => Task.Run(() => db.ExecuteDataSet<T>().ToList<T>());
        public T Get<T>() where T : class, new() => db.ExecuteDataSet<T>().GetFirstEntity<T>();
        public Task<T> GetAsync<T>() where T : class, new() => Task.Run(() => db.ExecuteDataSet<T>().GetFirstEntity<T>());
        public void Execute() => db.ExecuteNonQuery();
        public T ExecuteScalar<T>() => db.ExecuteScalar<T>();

        //private static void ResolveMapType()
        //{
        //    TypeMap = new Dictionary<Type, SqlDbType>();
        //    TypeMap[typeof(byte)] = SqlDbType.TinyInt;
        //    TypeMap[typeof(sbyte)] = SqlDbType.TinyInt;
        //    TypeMap[typeof(short)] = SqlDbType.SmallInt;
        //    TypeMap[typeof(ushort)] = SqlDbType.SmallInt;
        //    TypeMap[typeof(int)] = SqlDbType.Int;
        //    TypeMap[typeof(uint)] = SqlDbType.Int;
        //    TypeMap[typeof(long)] = SqlDbType.BigInt;
        //    TypeMap[typeof(ulong)] = SqlDbType.BigInt;
        //    TypeMap[typeof(float)] = SqlDbType.Float;
        //    TypeMap[typeof(double)] = SqlDbType.Money;
        //    TypeMap[typeof(decimal)] = SqlDbType.Decimal;
        //    TypeMap[typeof(bool)] = SqlDbType.Bit;
        //    TypeMap[typeof(string)] = SqlDbType.VarChar;
        //    TypeMap[typeof(char)] = SqlDbType.Char;
        //    TypeMap[typeof(Guid)] = SqlDbType.UniqueIdentifier;
        //    TypeMap[typeof(DateTime)] = SqlDbType.DateTime;
        //    TypeMap[typeof(DateTimeOffset)] = SqlDbType.DateTimeOffset;
        //    TypeMap[typeof(byte[])] = SqlDbType.VarBinary;
        //    TypeMap[typeof(byte?)] = SqlDbType.TinyInt;
        //    TypeMap[typeof(sbyte?)] = SqlDbType.TinyInt;
        //    TypeMap[typeof(short?)] = SqlDbType.SmallInt;
        //    TypeMap[typeof(ushort?)] = SqlDbType.SmallInt;
        //    TypeMap[typeof(int?)] = SqlDbType.Int;
        //    TypeMap[typeof(uint?)] = SqlDbType.Int;
        //    TypeMap[typeof(long?)] = SqlDbType.BigInt;
        //    TypeMap[typeof(ulong?)] = SqlDbType.BigInt;
        //    TypeMap[typeof(float?)] = SqlDbType.Float;
        //    TypeMap[typeof(double?)] = SqlDbType.Money;
        //    TypeMap[typeof(decimal?)] = SqlDbType.Decimal;
        //    TypeMap[typeof(bool?)] = SqlDbType.Bit;
        //    TypeMap[typeof(char?)] = SqlDbType.NChar;
        //    TypeMap[typeof(Guid?)] = SqlDbType.UniqueIdentifier;
        //    TypeMap[typeof(DateTime?)] = SqlDbType.DateTime;
        //    TypeMap[typeof(DateTimeOffset?)] = SqlDbType.DateTimeOffset;
        //    TypeMap[typeof(Domain.Types.enumStatus)] = SqlDbType.Bit;
        //}
        private static void ResolveMapType()
        {
            TypeMap = new Dictionary<Type, DbType>();
            TypeMap[typeof(byte)] = DbType.Byte;
            TypeMap[typeof(sbyte)] = DbType.SByte;
            TypeMap[typeof(short)] = DbType.Int16;
            TypeMap[typeof(ushort)] = DbType.UInt16;
            TypeMap[typeof(int)] = DbType.Int32;
            TypeMap[typeof(uint)] = DbType.UInt32;
            TypeMap[typeof(long)] = DbType.Int64;
            TypeMap[typeof(ulong)] = DbType.UInt64;
            TypeMap[typeof(float)] = DbType.Single;
            TypeMap[typeof(double)] = DbType.Double;
            TypeMap[typeof(decimal)] = DbType.Decimal;
            TypeMap[typeof(bool)] = DbType.Boolean;
            TypeMap[typeof(string)] = DbType.String;
            TypeMap[typeof(char)] = DbType.StringFixedLength;
            TypeMap[typeof(Guid)] = DbType.Guid;
            TypeMap[typeof(DateTime)] = DbType.DateTime;
            TypeMap[typeof(DateTimeOffset)] = DbType.DateTimeOffset;
            TypeMap[typeof(byte[])] = DbType.Binary;
            TypeMap[typeof(byte?)] = DbType.Byte;
            TypeMap[typeof(sbyte?)] = DbType.SByte;
            TypeMap[typeof(short?)] = DbType.Int16;
            TypeMap[typeof(ushort?)] = DbType.UInt16;
            TypeMap[typeof(int?)] = DbType.Int32;
            TypeMap[typeof(uint?)] = DbType.UInt32;
            TypeMap[typeof(long?)] = DbType.Int64;
            TypeMap[typeof(ulong?)] = DbType.UInt64;
            TypeMap[typeof(float?)] = DbType.Single;
            TypeMap[typeof(double?)] = DbType.Double;
            TypeMap[typeof(decimal?)] = DbType.Decimal;
            TypeMap[typeof(bool?)] = DbType.Boolean;
            TypeMap[typeof(char?)] = DbType.StringFixedLength;
            TypeMap[typeof(Guid?)] = DbType.Guid;
            TypeMap[typeof(DateTime?)] = DbType.DateTime;
            TypeMap[typeof(DateTimeOffset?)] = DbType.DateTimeOffset;
            TypeMap[typeof(enumStatus)] = DbType.Boolean;
        }
    }
}
