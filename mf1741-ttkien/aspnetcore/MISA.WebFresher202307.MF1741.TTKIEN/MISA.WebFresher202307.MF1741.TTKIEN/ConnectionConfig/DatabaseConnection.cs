using MySqlConnector;   

namespace MISA.WebFresher202307.MF1741.TTKIEN
{
    public class DatabaseConnection
    {
        private readonly string _connectionString = "Server=18.179.16.166;Port=3306;Database=MISA.WEB202307_MF1741_TTKIEN;Uid=nvmanh;Pwd=12345678;";

        public MySqlConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
