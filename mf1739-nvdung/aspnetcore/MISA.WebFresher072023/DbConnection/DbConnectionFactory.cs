using MySqlConnector;
using System.Data;

namespace MISA.WebFresher072023
{
	/// <summary>
	/// Class kết nối đến cơ sở dữ liệu
	/// </summary>
	/// Created by: NVDUNG (09/09/2023)
	public class DbConnectionFactory : IDbConnectionFactory
	{
		#region Property

		private readonly IConfiguration _configuration;

		#endregion


		#region Constructor

		/// <summary>
		/// Hàm tạo
		/// </summary>
		/// <param name="configuration"></param>
		/// Created by: NVDUNG (09/09/2023)
		public DbConnectionFactory(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		#endregion


		#region Method

		/// <summary>
		/// Tạo kết nối đến DataBase
		/// </summary>
		/// <returns>Đối tượng MySqlConnection</returns>
		/// Created by: NVDUNG (09/09/2023)
		public IDbConnection CreateConnection()
		{
			// Lấy chuỗi kết nối từ appsettings.json
			string connectionString = _configuration.GetConnectionString("DefaultConnection");
			// Trả về đối tượng MySqlConnection đã kết nối
			return new MySqlConnection(connectionString);
		}

		#endregion
	}
}
