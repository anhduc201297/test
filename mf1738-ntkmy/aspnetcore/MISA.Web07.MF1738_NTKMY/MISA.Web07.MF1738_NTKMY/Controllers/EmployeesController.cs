using Microsoft.AspNetCore.Http;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MISA.Web07.MF1738_NTKMY.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IConfiguration _config;

        public EmployeesController(IConfiguration config)
        {
            _config = config;
        }

        // Phương thức tạo kết nối đến cơ sở dữ liệu
        private IDbConnection CreateConnection()
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");
            return new MySqlConnection(connectionString);

        }

        /// <summary>
        /// Lấy danh sách tất cả nhân viên
        /// Author: NTKMY
        /// 10/09/2023
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeAsync()
        {
            try
            {
                using var connection = CreateConnection();

                // Tạo câu truy vấn
                var sql = "CALL Proc_Employee_GetAllEmployees";
                var result = await connection.QueryAsync<EmployeeEntity>(sql);

                // Nếu thành công trả về danh sách nhân viên
                return Ok(result);
            }
            catch (Exception ex)
            {
                var error = new ErrorHandler(ex.Message, "Đã xảy ra lỗi xin vui lòng liên hệ Misa");
                return StatusCode(500, error);
            }
        }

        /// <summary>
        /// Lấy thông tin 1 nhân viên theo id
        /// Author: NTKMY
        /// 10/09/2023
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{employeeId}")]
        public async Task<IActionResult> GetEmployeeAsync(Guid employeeId)
        {
            try
            {
                using var connection = CreateConnection();

                // Tạo câu truy vấn
                var sql = "CALL Proc_Employee_GetEmployee(@employeeId)";
                var param = new { employeeId };
                var result = await connection.QueryFirstOrDefaultAsync<EmployeeEntity>(sql, param);

                if (result == null)
                {
                    var error = new ErrorHandler("Employee not found", "Không tìm thấy được nhân viên");
                    return StatusCode(404, error);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                var error = new ErrorHandler(ex.Message, "Đã xảy ra lỗi xin vui lòng liên hệ Misa");
                return StatusCode(500, error);
            }
        }

        /// <summary>
        /// Xóa một nhân viên theo id
        /// Author: NTKMY
        /// 10/09/2023
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeAsync(Guid employeeId)
        {
            try
            {
                using var connection = CreateConnection();
                var employee = await GetEmployeeAsync(employeeId);

                // Nếu nhân viên không tồn tại trả về NotFound
                if (employee == null)
                {
                    var error = new ErrorHandler("Employee not found", "Không tìm thấy được nhân viên");
                    return StatusCode(404, error);
                }

                // Tạo câu truy vấn
                var sql = "CALL Proc_Employee_DeleteEmployee(@employeeId)";
                var param = new { employeeId };
                var result = await connection.ExecuteAsync(sql, param);

                if (result > 0)
                {
                    return Ok(result);
                }
                else
                {
                    var error = new ErrorHandler("Employee not found", "Không tìm thấy được nhân viên");
                    return StatusCode(404, error);
                }
            }
            catch (Exception ex)
            {
                var error = new ErrorHandler(ex.Message, "Đã xảy ra lỗi xin vui lòng liên hệ Misa");
                return StatusCode(500, error);
            }
        }

        /// <summary>
        /// Thêm một nhân viên 
        /// Author: NTKMY
        /// 11/09/2023
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] EmployeeEntity employee)
        {
            try
            {
                using var connection = CreateConnection();

                // Tạo truy vấn
                var sql = "INSERT INTO Employee (EmployeeId, EmployeeCode, FullName, DateOfBirth, Gender) " +
                          "VALUES (@EmployeeId, @EmployeeCode, @FullName, @DateOfBirth, @Gender)";

                var result = await connection.ExecuteAsync(sql, employee);

                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, "Thêm mới thành công");
                }
                else
                {
                    var error = new ErrorHandler("Can't add employee", "Thêm mới thất bại");
                    return StatusCode(400, error);
                }
            }
            catch (Exception ex)
            {
                var error = new ErrorHandler(ex.Message, "Đã xảy ra lỗi xin vui lòng liên hệ Misa");
                return StatusCode(500, error);
            }
        }

        /// <summary>
        /// Sửa một nhân viên theo id 
        /// Author: NTKMY
        /// 11/09/2023
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{employeeId}")]
        public async Task<IActionResult> UpdateEmployeeAsync([FromBody] EmployeeEntity employee, Guid employeeId)
        {
            try
            {
                using var connection = CreateConnection();

                var checkEmployee = await GetEmployeeAsync(employeeId);

                if (checkEmployee == null)
                {
                    var error = new ErrorHandler("Employee not found", "Không tìm thấy được nhân viên");
                    return StatusCode(404, error);
                }

                // Tạo truy vấn
                var sql = $"UPDATE Employee SET EmployeeCode = @EmployeeCode, FullName = @FullName, DateOfBirth = @DateOfBirth, Gender = @Gender WHERE EmployeeId = '{employeeId}'";

                var result = await connection.ExecuteAsync(sql, checkEmployee);

                if (result > 0)
                {
                    return Ok( "Cập nhật thành công");
                }
                else
                {
                    var error = new ErrorHandler("Can't update employee", "Cập nhật thất bại");
                    return StatusCode(400, error);
                }
            }
            catch (Exception ex)
            {
                var error = new ErrorHandler(ex.Message, "Đã xảy ra lỗi xin vui lòng liên hệ Misa");
                return StatusCode(500, error);

            }
        }
    }
}