using System.Data;
using Oracle.ManagedDataAccess.Client;
using SsoOAuth.BaseClasses;

namespace SsoOAuth.DB
{
    public static class DbConnectionHelper
    {
        public static DataTable GetData(string environmentName, string dbName, string query)
        {
            var connectionString = EnvironmentManager.GetConnectionString(environmentName, dbName);
            var dataTable = new DataTable();

            using var connection = new OracleConnection(connectionString);
            connection.Open();

            using var command = new OracleCommand(query, connection);
            using var adapter = new OracleDataAdapter(command);
            adapter.Fill(dataTable);

            return dataTable;
        }
    }
}