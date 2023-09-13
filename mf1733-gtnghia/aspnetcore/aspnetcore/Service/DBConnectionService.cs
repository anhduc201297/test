using MySqlConnector;

namespace aspnetcore
{
    /// <summary>
    /// Interface để tạo DI cho DBConnectionService
    /// </summary>
    public interface IDBConnectionService
    {
        MySqlConnection CreateConnection();
    }

    public class DBConnectionService : IDBConnectionService
    {
        private readonly string _connectionString;

        /// <summary>
        /// <paramref name="connectionString"/> : "Server=___;port=___;Database=___;Uid=___;pwd=___"
        /// </summary>
        /// <param name="connectionString"></param>
        public DBConnectionService(string connectionString)
        {
            this._connectionString = connectionString;
        }

        /// <summary>
        /// Create Connection Method
        /// Created by: mf1733-gtnghia (10/9/2023)
        /// </summary>
        /// <returns>Connect to MySql by extension MySQLConnector</returns>
        public MySqlConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

    }

}
