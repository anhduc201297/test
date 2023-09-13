using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher072023.Demo.Entity;
using MySqlConnector;



namespace MISA.WebFresher072023.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        // //Khởi tạo kết nối tời DB MySQL

        readonly string _connectionString = "Server=localhost;Port=3306;Database=misa_amis_linhlt;Uid=root;Pwd=1111;Allow User Variables=True";

        /// <summary>
        /// API lấy danh sách chưa phân trang
        /// </summary>
        /// <param name="employee"></param>

        /// Created by: Linhlt(11/09/2023)
        [HttpGet]
        public async Task<List<EmployeeEntity>> GetAllEmployeeAsync()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            var sql = "SELECT * FROM Employee";
            var result = await connection.QueryAsync<EmployeeEntity>(sql);

            //đóng kết nối
            await connection.CloseAsync();

            return result.ToList();
        }


        /// <summary>
        /// API lấy mã nhân viên
        /// </summary>
        /// <param name="employee"></param>

        /// Created by: Linhlt(11/09/2023)

        [HttpGet]
        [Route("{EmployeeID}")]
        public async Task<IActionResult> GetOneEmployeeAsync([FromRoute] string EmployeeID)
        {
            try
            {
                var mySQLConnection = new MySqlConnection(_connectionString);

                //mở kết nối
                await mySQLConnection.OpenAsync();


                //Thực hiện truy vấn
                var sqlQuery = $"SELECT * FROM Employee WHERE EmployeeID= @employeeID";


                //Tạo param chống injection
                var param = new DynamicParameters();
                param.Add("employeeID", EmployeeID);
                var result = await mySQLConnection.QueryFirstOrDefaultAsync<EmployeeEntity>(sqlQuery, param);

                //đóng kết nối
                await mySQLConnection.CloseAsync();
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest);
            }
          
        }



        /// <summary>
        /// API xóa 1 nhân viên
        /// </summary>
        /// <param name="EmployeeID"></param>

        /// Created by: Linhlt(11/09/2023)
        [HttpDelete("{EmployeeID}")]
        public async Task<IActionResult> DeleteOneEmployeeAsync([FromRoute] Guid EmployeeID)
        {
            try {
                var mySQLConnection = new MySqlConnection(_connectionString);

                //mở kết nối
                await mySQLConnection.OpenAsync();

                //Thực hiện truy vấn
                var sqlQuery = $"DELETE FROM Employee WHERE EmployeeID = @employeeID";

                var param = new DynamicParameters();
                param.Add("employeeID", EmployeeID);

                var result = await mySQLConnection.ExecuteAsync(sqlQuery, param);


                if(result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, EmployeeID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }



            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
      

        }

        /// <summary>
        /// API thêm mới nhân viên
        /// </summary>
        /// <param name="employee"></param>

        /// Created by: Linhlt(11/09/2023)

        //thêm mới nhân viên
        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] EmployeeEntity employee)
        {
            try
            {
                var mySQLConnection = new MySqlConnection(_connectionString);
                var guid = Guid.NewGuid();
                await mySQLConnection.OpenAsync();
                var sqlQuery = $"INSERT INTO Employee (EmployeeID, EmployeeCode, FullName, Gender, DateOfBirth, DepartmentID, DepartmentName, PositionName) " +
                "VALUES (@employeeID, @employeeCode, @fullName, @gender, @dateOfBirth, @departmentID, @departmentName, @positionName)";
                // chuẩn bị dữ liệu
                var parameters = new DynamicParameters();
                parameters.Add("employeeID", guid);
                parameters.Add("employeeCode", employee.EmployeeCode);
                parameters.Add("fullName", employee.FullName);
                parameters.Add("gender", employee.Gender);
                parameters.Add("dateOfBirth", employee.DateOfBirth);
                parameters.Add("departmentID", employee.DepartmentID);
                parameters.Add("departmentName", employee.DepartmentName);
                parameters.Add("positionName", employee.PositionName);

                // cách code dùng for khi add nhiều nhưng vẫn đang bị lỗi phần Query.Replace
      /*          var properties = typeof(EmployeeEntity).GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    var propertyName = properties[i].Name;
                    var propertyValue = properties[i].GetValue(employee, null);
                    Console.WriteLine(propertyName, propertyValue);
                    parameters.Add(propertyName, propertyValue);
                    sqlQuery = sqlQuery.Replace($"@{propertyName}", $"'{propertyValue}'");

                }*/


                var result = await mySQLConnection.ExecuteAsync(sqlQuery, parameters);

                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, guid);
                }
                else  //Thất bại
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// API sửa nhân viên
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <param name="employee"></param>
        /// Created by: Linhlt(11/09/2023)
        [HttpPut("{EmployeeID}")]
        public async Task<IActionResult> UpdateEmployeeAsync([FromRoute]Guid EmployeeID, [FromBody] EmployeeEntity employee)
        {

            try
            {
                var mySQLConnection = new MySqlConnection(_connectionString);
                await mySQLConnection.OpenAsync();
                var sqlQuery = "UPDATE Employee SET EmployeeCode = @employeeCode, FullName = @fullName, Gender = @gender, DateOfBirth = @dateOfBirth, DepartmentID = @departmentID, " +
                    "DepartmentName = @departmentName, PositionName = @positionName WHERE EmployeeID = @employeeID";

                var parameters = new DynamicParameters();
                parameters.Add("employeeID", employee.EmployeeID);
                parameters.Add("employeeCode", employee.EmployeeCode);
                parameters.Add("fullName", employee.FullName);
                parameters.Add("gender", employee.Gender);
                parameters.Add("dateOfBirth", employee.DateOfBirth);
                parameters.Add("departmentID", employee.DepartmentID);
                parameters.Add("departmentName", employee.DepartmentName);
                parameters.Add("positionName", employee.PositionName);

                var employeeUpdate = await mySQLConnection.ExecuteAsync(sqlQuery, parameters);

                if (employeeUpdate > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, EmployeeID);
                }
                else  //Thát bại
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
      

    }
}
