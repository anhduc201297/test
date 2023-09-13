using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MISA.WebFresher202307.MF1741.TTKIEN.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        [HttpGet]
        //Lấy tất cả nhân viên
        public async Task<List<EmployeeEntity>> GetAllEmployeeAsync()
        {
            // Tạo kết nối tới cơ sở dữ liệu
            var dbConnection = new DatabaseConnection();
            var connection = dbConnection.CreateConnection();

            // Mở kết nối
            //await connection.openasync();

            // Tạo câu truy vấn gọi procedure
            var sql = "CALL Proc_Employee_GetAll";

            var result = await connection.QueryAsync<EmployeeEntity>(sql);

            // Đóng kết nối
            //await connection.CloseAsync();

            return result.ToList();
        }

        [HttpGet]
        [Route("{employeeId}")]
        // Lấy nhân viên theo Id
        public async Task<IActionResult> GetEmployeeAsync(Guid employeeId)
        {
            // Tạo kết nối tới cơ sở dữ liệu
            var dbConnection = new DatabaseConnection();
            var connection = dbConnection.CreateConnection();

            // Tạo câu truy vấn
            var sql = $"SELECT * FROM Employee WHERE EmployeeId = '{employeeId}'";

            var result = await connection.QueryFirstOrDefaultAsync<EmployeeEntity>(sql, employeeId);

            if (result == null)
            {
                return NotFound("Không tìm thấy nhân viên.");
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("{employeeId}")]
        // Xóa nhân viên theo Id
        public async Task<int> DeleteEmployeeAsync(Guid employeeId)
        {
            // Tạo kết nối tới cơ sở dữ liệu
            var dbConnection = new DatabaseConnection();
            var connection = dbConnection.CreateConnection();

            var employee = await GetEmployeeAsync(employeeId);

            if (employee == null)
            {
                throw new Exception("Nhân viên không tồn tại.");
            }
            // Tạo câu truy vấn
            var sql = $"CALL Proc_Employee_DeleteById (@employeeId)";

            // Tạo param chống sql injection
            var param = new DynamicParameters();
            param.Add("employeeId", employeeId);

            var result = await connection.ExecuteAsync(sql, param);

            return result;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        // Thêm nhân viên mới
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] NewEmployeeEntity newEmployee)
        {
            // Tạo kết nối tới cơ sở dữ liệu
            var dbConnection = new DatabaseConnection();
            var connection = dbConnection.CreateConnection();

            // Tạo id cho nhân viên mới
            var newEmployeeId = Guid.NewGuid();

            // Tạo câu truy vấn
            var sql = $"INSERT INTO Employee (EmployeeId, EmployeeCode, FullName, DateOfBirth, Gender) VALUES ('{newEmployeeId}', @EmployeeCode, @FullName, @DateOfBirth, @Gender)";

            var result = await connection.ExecuteAsync(sql, newEmployee);

            if (result == 1)
            {
                return Created(sql, "Thêm mới nhân viên thành công");
            }
            else
            {
                return StatusCode(500, "Không thể thêm nhân viên.");
            }
        }

        [HttpPut]
        [Route("{employeeId}")]
        // Sửa thông tin nhân viên
        public Task<int> EditEmplloyeeAsync(Guid employeeId, [FromBody] NewEmployeeEntity newEmployee)
        {
            // Tạo kết nối tới cơ sở dữ liệu
            var dbConnection = new DatabaseConnection();
            var connection = dbConnection.CreateConnection();

            // Tạo câu truy vấn
            var sql = $"UPDATE Employee SET EmployeeCode = @EmployeeCode, FullName = @FullName, DateOfBirth = @DateOfBirth, Gender = @Gender WHERE EmployeeId = '{employeeId}'";

            var result = connection.ExecuteAsync(sql, newEmployee);

            return result;
        }
    }
}
