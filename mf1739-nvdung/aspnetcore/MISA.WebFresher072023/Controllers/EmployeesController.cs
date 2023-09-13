using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher072023.Entity;
using MySqlConnector;
using MISA.WebFresher072023;
using System.Net;
using Microsoft.AspNetCore.Authorization;


namespace MISA.WebFresher072023.Controllers
{
	/// <summary>
	/// Các API thực hiện các thao tác dữ liệu về Employee trong Database, gồm:
	/// <list type="bullet">
	/// <item>Các hàm truy vấn, thêm/sửa/xóa nhân viên</item>
	/// </list>
	/// </summary>
	/// Created by NVDUNG (09/09/2023)
	[Route("api/v1/[controller]")]
	[ApiController]
	public class EmployeesController : ControllerBase
	{
		#region Property

		// Thuộc tính để kết nối tới database
		private readonly IDbConnectionFactory _dbConnectionFactory;

		#endregion

		#region Constructor

		// Hàm tạo
		public EmployeesController(IDbConnectionFactory dbConnectionFactory)
		{
			_dbConnectionFactory = dbConnectionFactory;
		}

		#endregion


		#region Method

		/// <summary>
		/// Hàm lấy danh sách tất cả nhân viên
		/// </summary>
		/// <param name=""></param>
		/// <returns>Danh sách tất cả nhân viên</returns>
		/// Created by: NVDUNG (09/09/2023)
		[HttpGet]
		//[Authorize] // Kiểm tra Authentication - Yêu cầu người dùng phải đăng nhập -- 401 UNAUTHORIZED 
		public async Task<IActionResult> GetAllEmployeeAsync()
		{
			#region Kiểm tra Authorization

			// Lỗi từ client - Không có quyền truy cập vào tài nguyên. (Đã đăng nhập thành công)
			//if (!User.IsInRole("Admin"))
			//{
			//	// Trả về mã trạng thái 403 Forbidden
			//	return StatusCode(403);
			//}

			#endregion


			#region Validate input
			#endregion


			#region Validate business logic
			#endregion


			#region Thực hiện truy vấn

			// Tạo kết nối cơ sở dữ liệu
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				// Tạo câu truy vấn
				//var sql = "SELECT * FROM Employee";
				var sql = "CALL GetEmployees()";
				// Truy vấn Dapper
				var result = await connection.QueryAsync(sql);

				// Trả về httpStatusCode 200 và data
				return Ok(result);
			}

			#endregion
		}


		/// <summary>
		/// Hàm lấy thông tin nhân viên theo EmployeeId
		/// </summary>
		/// <param name="employeeId">Id nhân viên cần lấy thông tin</param>
		/// <returns>Thông tin nhân viên phù hợp, hoặc báo lỗi không tìm thấy.</returns>
		/// Created by: NVDUNG (09/09/2023)
		[HttpGet]
		[Route("{employeeId}")]
		//[Authorize(Roles = "")] // Kiểm tra Authentication - Yêu cầu người dùng phải đăng nhập -- 401 UNAUTHORIZED 
		public async Task<IActionResult> GetEmployeeAsync(string employeeId)
		{

			#region Kiểm tra Authorization
			// Lỗi từ client - Không có quyền truy cập vào tài nguyên. (Đã đăng nhập thành công)
			//if (!User.IsInRole("Admin"))
			//{
			//	// Trả về mã trạng thái 403 Forbidden
			//	return Forbid();
			//}

			#endregion


			#region Validate input
			#endregion


			#region Validate business logic
			#endregion


			#region Thực hiện truy xuất
			// Tạo kết nối đến csdl
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				// Tạo câu truy vấn
				//var sql = $"SELECT * FROM Employee WHERE EmployeeId = @employeeId";
				var sql = $"CALL GetEmployeeById(@employeeId)";

				// Tạo Param chống sql injection
				var param = new DynamicParameters();
				param.Add("employeeId", employeeId);

				try
				{
					var result = await connection.QuerySingleOrDefaultAsync(sql, param);
					if (result != null)
					{
						// Trả về thông tin nhân viên nếu tìm thấy
						return Ok(result);
					}
					else
					{
						// Không tìm thấy nhân viên

						// Tạo đối tượng lỗi chung trả về
						var errorHandle = new ErrorResponseEntity(1, "Không thấy tài nguyên", "Nhân viên không tồn tại", "", "");

						return NotFound(errorHandle);
					}

				}
				catch (Exception ex)
				{
					// Ghi log lỗi
					Console.WriteLine(ex);

					// Tạo đối tượng chung trả về
					var errorHandle = new ErrorResponseEntity();
					// Trả về lỗi do server
					return StatusCode(500);
				}
			}
			#endregion
		}


		/// <summary>
		/// Hàm thêm mới một nhân viên
		/// </summary>
		/// <param name="newEmployee">Đối tượng nhân viên mới</param>
		/// <returns></returns>
		/// /// Created by: NVDUNG (11/09/2023)
		[HttpPost]
		//[Authorize] // Kiểm tra Authentication - Yêu cầu người dùng phải đăng nhập -- 401 UNAUTHORIZED 
		public async Task<IActionResult> CreateEmployeeAsync(EmployeeEntity newEmployee)
		{
			#region Kiểm tra Authorization

			// Lỗi từ client - Không có quyền truy cập vào tài nguyên. (Đã đăng nhập thành công)
			//if (!User.IsInRole("Admin"))
			//{
			//	// Trả về mã trạng thái 403 Forbidden
			//	return StatusCode(403);
			//}

			#endregion


			#region Validate input
			if (!ModelState.IsValid) // Lỗi validate input
			{
				// Lấy ra danh sách lỗi
				var errors = ModelState.SelectMany(x => x.Value.Errors.Select(e => e.ErrorMessage)).ToList();

				// Tạo đối tượng lỗi
				var errorHandle = new ErrorResponseEntity(1, "Lỗi validate input: " + errors.ToString(), "Đã nhập sai, hãy nhập lại", "", "");

				return StatusCode(400, errorHandle);
			}
			#endregion


			#region Validate business logic
			#endregion


			#region Thực hiện truy vấn

			// Tạo kết nối cơ sở dữ liệu
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				// Tạo câu truy vấn
				var sql = $"CALL CreateEmployee(@employeeCode, @firstName, @lastName, @fullName, @dateOfBirth, @gender, @identityNumber, @identityDate, @identityPlace, @address, @phoneNumber, @email, @bankAccount, @bankName, @bankBranch, @createdDate, @createdBy, @modifiedDate, @modifiedBy, @departmentId, @positionId)";

				// Tạo Param chống sql injection
				var param = new DynamicParameters();
				#region Thêm param
				param.Add("employeeCode", newEmployee.EmployeeCode);
				param.Add("firstName", newEmployee.FullName);
				param.Add("lastName", newEmployee.LastName);
				param.Add("fullName", newEmployee.FullName);
				param.Add("dateOfBirth", newEmployee.DateOfBirth);
				param.Add("gender", newEmployee.Gender);
				param.Add("identityNumber", newEmployee.IdentityNumber);
				param.Add("identityDate", newEmployee.IdentityDate);
				param.Add("identityPlace", newEmployee.IdentityPlace);
				param.Add("address", newEmployee.Address);
				param.Add("phoneNumber", newEmployee.PhoneNumber);
				param.Add("email", newEmployee.Email);
				param.Add("bankAccount", newEmployee.BankAccount);
				param.Add("bankName", newEmployee.BankName);
				param.Add("bankBranch", newEmployee.BankBranch);
				param.Add("createdDate", newEmployee.CreatedDate);
				param.Add("createdBy", newEmployee.CreatedBy);
				param.Add("modifiedDate", newEmployee.ModifiedDate);
				param.Add("modifiedBy", newEmployee.ModifiedBy);
				param.Add("departmentId", newEmployee.DepartmentId);
				param.Add("positionId", newEmployee.PositionId);
				#endregion
				try
				{
					// Thêm dữ liệu
					var result = await connection.ExecuteAsync(sql, param);
					if (result != 0)
					{
						// Trả về thông tin nhân viên nếu tìm thấy

						return Ok(new ErrorResponseEntity(1, "Thêm mới thành công", "Thêm mới thành công", "", ""));
					}
					else { return StatusCode(500); }
				}
				catch (Exception ex)
				{
					// Ghi log lỗi
					Console.WriteLine(ex);

					// Tạo đối tượng chung trả về
					var errorHandle = new ErrorResponseEntity(1, "Lỗi", "Liên hệ tổng tài để được trợ giúp", "", "");
					// Trả về lỗi do server
					return StatusCode(500, errorHandle);
				}
			}

			#endregion
		}



		/// <summary>
		/// Hàm sửa thông tin một nhân viên
		/// </summary>
		/// <param name="newEmployee">Id nhân viên cần sửa</param>
		/// <param name="newEmployee">Giá trị nhân viên mới</param>
		/// <returns></returns>
		/// /// Created by: NVDUNG (11/09/2023)
		[HttpPut]
		[Route("{employeeId}")]
		//[Authorize] // Kiểm tra Authentication - Yêu cầu người dùng phải đăng nhập -- 401 UNAUTHORIZED 
		public async Task<IActionResult> UpdateEmployeeAsync(Guid employeeId, EmployeeEntity newEmployee)
		{
			#region Kiểm tra Authorization

			// Lỗi từ client - Không có quyền truy cập vào tài nguyên. (Đã đăng nhập thành công)
			//if (!User.IsInRole("Admin"))
			//{
			//	// Trả về mã trạng thái 403 Forbidden
			//	return StatusCode(403);
			//}

			#endregion


			#region Validate input
			if (!ModelState.IsValid) // Lỗi validate input
			{
				// Lấy ra danh sách lỗi
				var errors = ModelState.SelectMany(x => x.Value.Errors.Select(e => e.ErrorMessage)).ToList();

				// Tạo đối tượng lỗi
				var errorHandle = new ErrorResponseEntity(1, "Lỗi validate input: " + errors.ToString(), "Đã nhập sai, hãy nhập lại", "", "");

				return StatusCode(400, errorHandle);
			}
			#endregion


			#region Validate business logic
			#endregion


			#region Thực hiện truy vấn

			// Tạo kết nối cơ sở dữ liệu
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				// Tạo câu truy vấn
				var sql = $"CALL UpdateEmployee(@employeeId, @employeeCode, @firstName, @lastName, @fullName, @dateOfBirth, @gender, @identityNumber, @identityDate, @identityPlace, @address, @phoneNumber, @email, @bankAccount, @bankName, @bankBranch, @createdDate, @createdBy, @modifiedDate, @modifiedBy, @departmentId, @positionId)";

				// Tạo Param chống sql injection
				var param = new DynamicParameters();
				#region Thêm param
				param.Add("employeeId", employeeId);

				param.Add("employeeCode", newEmployee.EmployeeCode);
				param.Add("firstName", newEmployee.FullName);
				param.Add("lastName", newEmployee.LastName);
				param.Add("fullName", newEmployee.FullName);
				param.Add("dateOfBirth", newEmployee.DateOfBirth);
				param.Add("gender", newEmployee.Gender);
				param.Add("identityNumber", newEmployee.IdentityNumber);
				param.Add("identityDate", newEmployee.IdentityDate);
				param.Add("identityPlace", newEmployee.IdentityPlace);
				param.Add("address", newEmployee.Address);
				param.Add("phoneNumber", newEmployee.PhoneNumber);
				param.Add("email", newEmployee.Email);
				param.Add("bankAccount", newEmployee.BankAccount);
				param.Add("bankName", newEmployee.BankName);
				param.Add("bankBranch", newEmployee.BankBranch);
				param.Add("createdDate", newEmployee.CreatedDate);
				param.Add("createdBy", newEmployee.CreatedBy);
				param.Add("modifiedDate", newEmployee.ModifiedDate);
				param.Add("modifiedBy", newEmployee.ModifiedBy);
				param.Add("departmentId", newEmployee.DepartmentId);
				param.Add("positionId", newEmployee.PositionId);
				#endregion
				try
				{
					// Thêm dữ liệu
					var result = await connection.ExecuteAsync(sql, param);
					if (result != 0)
					{
						// Trả về thông tin nhân viên nếu tìm thấy

						return Ok(new ErrorResponseEntity(1, "Cập nhật thành công", "Cập nhật thành công", "", ""));
					}
					else { return StatusCode(500); }
				}
				catch (Exception ex)
				{
					// Ghi log lỗi
					Console.WriteLine(ex);

					// Tạo đối tượng chung trả về
					var errorHandle = new ErrorResponseEntity(1, "Lỗi", "Liên hệ tổng tài để được trợ giúp", "", "");
					// Trả về lỗi do server
					return StatusCode(500, errorHandle);
				}
			}

			#endregion
		}
		#endregion


		/// <summary>
		/// Hàm xóa thông tin nhân viên theo EmployeeId
		/// </summary>
		/// <param name="employeeId">Id nhân viên cần xóa</param>
		/// <returns></returns>
		/// Created by: NVDUNG (11/09/2023)
		[HttpDelete]
		[Route("{employeeId}")]
		//[Authorize(Roles = "")] // Kiểm tra Authentication - Yêu cầu người dùng phải đăng nhập -- 401 UNAUTHORIZED 
		public async Task<IActionResult> DeleteEmployeeAsync(string employeeId)
		{

			#region Kiểm tra Authorization
			// Lỗi từ client - Không có quyền truy cập vào tài nguyên. (Đã đăng nhập thành công)
			//if (!User.IsInRole("Admin"))
			//{
			//	// Trả về mã trạng thái 403 Forbidden
			//	return Forbid();
			//}

			#endregion


			#region Validate input

			#endregion


			#region Validate business logic
			#endregion


			#region Thực hiện truy xuất
			// Tạo kết nối đến csdl
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				// Tạo câu truy vấn
				//var sql = $"SELECT * FROM Employee WHERE EmployeeId = @employeeId";
				var sql = $"CALL DeleteEmployee(@employeeId)";

				// Tạo Param chống sql injection
				var param = new DynamicParameters();
				param.Add("employeeId", employeeId);

				try
				{
					var result = await connection.ExecuteAsync(sql, param);
					if (result != null)
					{
						// Trả về thông tin nhân viên nếu tìm thấy
						return Ok(result);
					}
					else
					{
						// Không tìm thấy nhân viên

						// Tạo đối tượng lỗi chung trả về
						var errorHandle = new ErrorResponseEntity(1, "Không thấy tài nguyên", "Nhân viên không tồn tại", "", "");

						return NotFound(errorHandle);
					}

				}
				catch (Exception ex)
				{
					// Ghi log lỗi
					Console.WriteLine(ex);

					// Tạo đối tượng chung trả về
					var errorHandle = new ErrorResponseEntity(1, "Lỗi", "Liên hệ tổng đài để được giúp đỡ", "", "");
					// Trả về lỗi do server
					return StatusCode(500, errorHandle);
				}
			}
			#endregion
		}
	}
}
