using System.Configuration;
using System.Data.SqlClient;

namespace TG.Exam.Refactoring
{
    public class BaseRepository
    {
        private const string ConnectionStringName = "OrdersDBConnectionString";
        private const string ConfigurationErrorMessage = "The specified connectionString doesn't exist";

        private readonly string _connectionString;

        public BaseRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName]?.ConnectionString
                ?? throw new ConfigurationErrorsException(ConfigurationErrorMessage);
        }

        protected SqlConnection GetConnection()
        {
            var connection = new SqlConnection(_connectionString);

            connection.Open();

            return connection;
        }
    }
}
