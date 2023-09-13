using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace MISA.WebFresher072023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        /// <summary>
        /// Hàm lấy danh sách tất cả nhân viên
        /// </summary>
        /// Danh sách nhân viên trả về list
        /// <author>Lê Trường Lam - 09/11/2023<author>
        [HttpGet]
        public async Task<List<EmployeeEntity>> GetAllEmployeeAsync()
        {
            // Khai báo connection string
            var connectionString = "Server=localhost; Port=3306; Database=misadata; Uid=root; Pwd=962002;";
            // Tao ket noi csdl
            var connection = new MySqlConnection
                (connectionString);
            // Mo ket noi
            // await connnection.OpenAsync();
            var sql = "SELECT * FROM employee";
            var result = await
                connection.QueryAsync<EmployeeEntity>(sql);
            //  await connnection.CloseAsync();
            return result.ToList();
        }

        /// <summary>
        /// Hàm lấy danh sách  nhân viên theo employeeId
        /// </summary>
        /// <author>Lê Trường Lam - 09/11/2023<author>

        [HttpGet]
        [Route("{employeeId}")]
        public async Task<EmployeeEntity> GetEmployeeAsync(Guid employeeId)
        {
            // Khai báo connection string
            var connectionString = "Server=localhost; Port=3306; Database=misadata; Uid=root; Pwd=962002;";
            // Tao ket noi csdl
            var connection = new MySqlConnection
                (connectionString);
            // Mo ket noi
            var sql = $"SELECT * FROM employee WHERE EmployeeId = '{employeeId}'";
            var result = await
                connection.QuerySingleAsync<EmployeeEntity>(sql);
            return result;
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] EmployeeEntity employee)
        {
            // Khai báo connection string
            var connectionString = "Server=localhost; Port=3306; Database=misadata; Uid=root; Pwd=962002;";
            // Tao ket noi csdl
            using (var connection = new MySqlConnection(connectionString))
            {
                // Mo ket noi
                await connection.OpenAsync();
                // Thực hiện câu truy vấn để chèn dữ liệu vào CSDL
                var sql = "INSERT INTO employee (EmployeeId, EmployeeCode, FullName, DateOfBirth, Gender) " +
                          "VALUES (@EmployeeId, @EmployeeCode, @FullName, @DateOfBirth, @Gender)";
                // Tạo đối tượng DynamicParameters để truyền các tham số cho câu truy vấn
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeId", employee.EmployeeId);
                parameters.Add("@EmployeeCode", employee.EmployeeCode);
                parameters.Add("@FullName", employee.FullName);
                parameters.Add("@DateOfBirth", employee.DateOfBirth);
                parameters.Add("@Gender", employee.Gender);
                // Thực thi câu truy vấn
                await connection.ExecuteAsync(sql, parameters);
                // Trả về kết quả thành công
                return Ok();
            }
        }

        /// <summary>
        /// Hàm update  nhân viên theo employeeId
        /// </summary>
        /// <author>Lê Trường Lam - 09/11/2023<author>
        [HttpPut]
        [Route("{employeeId}")]
        public async Task<IActionResult> UpdateEmployeeAsync(Guid employeeId, [FromBody] EmployeeEntity updatedEmployee)
        {
            // Khai báo connection string
            var connectionString = "Server=localhost; Port=3306; Database=misadata; Uid=root; Pwd=962002;";
            // Tạo kết nối CSDL
            using (var connection = new MySqlConnection(connectionString))
            {
                // Mở kết nối
                await connection.OpenAsync();
                // Kiểm tra xem nhân viên có tồn tại hay không
                var existingEmployee = await connection.QuerySingleOrDefaultAsync<EmployeeEntity>(
                    "SELECT * FROM employee WHERE EmployeeId = @EmployeeId",
                    new { EmployeeId = employeeId });
                if (existingEmployee == null)
                {
                    return NotFound(); // Trả về lỗi 404 nếu không tìm thấy nhân viên
                }
                // Cập nhật thông tin nhân viên
                existingEmployee.FullName = updatedEmployee.FullName;
                existingEmployee.DateOfBirth = updatedEmployee.DateOfBirth;
                existingEmployee.Gender = updatedEmployee.Gender;
                // Cập nhật thông tin nhân viên vào CSDL
                await connection.ExecuteAsync(
                    "UPDATE employee SET FullName = @FullName, DateOfBirth = @DateOfBirth, Gender = @Gender WHERE EmployeeId = @EmployeeId",
                    new
                    {
                        FullName = existingEmployee.FullName,
                        DateOfBirth = existingEmployee.DateOfBirth,
                        Gender = existingEmployee.Gender,
                        EmployeeId = employeeId
                    });
                return Ok(); // Trả về thành công nếu cập nhật thành công
            }
        }


        /// <summary>
        /// Hàm Xóa  nhân viên theo employeeId
        /// </summary>
        /// <author>Lê Trường Lam - 09/11/2023<author>

        [HttpDelete]
        [Route("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeAsync(Guid employeeId)
        {
            // Khai báo connection string
            var connectionString = "Server=localhost; Port=3306; Database=misadata; Uid=root; Pwd=962002;";

            // Tạo kết nối CSDL
            using (var connection = new MySqlConnection(connectionString))
            {
                // Mở kết nối
                await connection.OpenAsync();
                // Kiểm tra xem nhân viên có tồn tại hay không
                var sqlCheckExistence = $"SELECT COUNT(*) FROM employee WHERE EmployeeId = '{employeeId}'";
                var employeeExists = await connection.ExecuteScalarAsync<int>(sqlCheckExistence);
                if (employeeExists == 0)
                {
                    // Nếu nhân viên không tồn tại, trả về lỗi 404 Not Found
                    return NotFound();
                }
                // Xây dựng câu lệnh DELETE
                var sqlDelete = $"DELETE FROM employee WHERE EmployeeId = '{employeeId}'";
                // Thực thi câu lệnh DELETE và trả về số lượng hàng bị ảnh hưởng
                var rowsAffected = await connection.ExecuteAsync(sqlDelete);
                // Đóng kết nối
                connection.Close();
                // Kiểm tra số lượng hàng bị ảnh hưởng
                if (rowsAffected == 0)
                {
                    // Nếu không có hàng nào bị xóa, trả về lỗi 500 Internal Server Error
                    return StatusCode(500);
                }
                // Nếu có ít nhất một hàng bị xóa, trả về thành công
                return Ok();
            }
        }
    }
}
