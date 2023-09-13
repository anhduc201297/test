using MySqlConnector;


namespace MISA.Web07.MF1732_DKTUAN
{
    /// <summary>
    /// Quản lý kết nối đến database
    /// Created by: mf1732-dktuan (11/9/2023)
    /// </summary>
    public class MySqlConnectionFactory
    {
        private readonly string _connectionString;

        public MySqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevConnectionString");
        }

        public MySqlConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
