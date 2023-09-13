using System.Data;

namespace MISA.WebFresher072023
{
	/// <summary>
	/// Tạo interface để đại diện cho dịch vụ kết nối cơ sở dữ liệu
	/// </summary>
	/// Created by: NVDUNG (09/09/2023)
	public interface IDbConnectionFactory
	{
		#region Method

		/// <summary>
		/// Phương thức tạo kết nối đến cơ sở dữ liệu
		/// </summary>
		/// <returns></returns>
		/// Created by: NVDUNG (09/09/2023)
		IDbConnection CreateConnection();

		#endregion
	}
}
