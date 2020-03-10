using System;

namespace PUCMinasTCC.FrameworkDAO
{
    public class DatabaseFactory
    {
        public static IDataContext CreateDatabase(DatabaseType databaseType, string connectionString)
        {
            switch (databaseType)
            {
                case DatabaseType.SqlServer:
                case DatabaseType.Oracle:
                case DatabaseType.MySql:
                case DatabaseType.PostgresSql:
                case DatabaseType.DB2:
                case DatabaseType.Firebird:
                default:
                    return new SqlContext(connectionString);
            }
        }

        //public static IDataContext CreateDatabase(string nameConnectionString, DatabaseType databaseType)
        //{
        //    if (ConfigurationManager.ConnectionStrings.Count == 0) throw new Exception("Nenhuma connection string cadastrada");

        //    string connectionString = ConfigurationManager.ConnectionStrings[nameConnectionString].ConnectionString;

        //    switch (databaseType)
        //    {
        //        case DatabaseType.SqlServer:
        //        case DatabaseType.Oracle:
        //        case DatabaseType.MySql:
        //        case DatabaseType.PostgresSql:
        //        case DatabaseType.DB2:
        //        case DatabaseType.Firebird:
        //        default:
        //            return new SqlContext(connectionString);
        //    }
        //}
    }

    public enum DatabaseType
    {
        SqlServer,
        Oracle,
        MySql,
        PostgresSql,
        DB2,
        Firebird
    }
}
