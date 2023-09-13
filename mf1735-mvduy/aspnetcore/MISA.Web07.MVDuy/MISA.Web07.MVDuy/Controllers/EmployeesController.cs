using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Net.Mail;

namespace MISA.Web07.MVDuy.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<EmployeeEntity>>> GetAllEmployeeAsync()
        {
            try
            {
                // Khai báo connection string
                var connectionString = "User Id=root;Host=localhost;Character Set=utf8;Database=misa.web07_mf1735_mvduy";
                //"Server=18.179.16.166;Port=3306;Database=NVMANH.DEMO;Uid=nvmanh;Pwd=12345678;"

                //Tạo kết nối tới cơ sở dữ liệu
                var connection = new MySqlConnector.MySqlConnection(connectionString);


                //Tạo câu truy vấnz
                var sql = "SELECT * FROM Employee";

                var result = await connection.QueryAsync<EmployeeEntity>(sql);


                return result.ToList();
            }
            catch (Exception ex)
            {

                var result = HandleException(ex);
                return result;
            }

        }

        private ActionResult HandleException(Exception ex)
        {
            var error = new ErrorService();
            error.DevMsg = ex.Message;
            error.UserMsg = Resources.ResourceVN.Error_Exception;
            error.Data = ex.Data;

            return StatusCode(500, error);



        }

        [HttpGet]
        [Route("{employeeId}")]
        public async Task<ActionResult<EmployeeEntity>> GetEmployeeAsync(string employeeId)
        {
            try
            {
                // Khai báo connection string
                var connectionString = "User Id=root;Host=localhost;Character Set=utf8;Database=misa.web07_mf1735_mvduy";
                //"Server=18.179.16.166;Port=3306;Database=NVMANH.DEMO;Uid=nvmanh;Pwd=12345678;"

                //Tạo kết nối tới cơ sở dữ liệu
                var connection = new MySqlConnector.MySqlConnection(connectionString);

                //Mở kết nối
                await connection.OpenAsync();

                //Tạo câu truy vấnz
                var sql = $"SELECT * FROM Employee WHERE EmployeeId = @employeeId";

                //Tạo Param chống sql injection
                var param = new DynamicParameters();
                param.Add("employeeId", employeeId);

                var result = await connection.QueryFirstOrDefaultAsync<EmployeeEntity>(sql, param);

                // Đóng kết nối
                await connection.CloseAsync();

                return result;
            }

            catch (Exception ex)
            {
                var result = HandleException(ex);
                return result;

            }

        }

        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên</param>
        /// <returns>
        /// 201 - Thêm mới thành công
        /// 400 - Dữ liệu đầu vào không hợp lệ
        /// 500 - Có exception
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> PostEmployeeAsync(EmployeeEntity employee)
        {

            try
            {
                var error = new ErrorService();

                // Validate dữ liệu

                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    error.UserMsg = "Mã nhân viên không được phép để trống";
                    return BadRequest(error);
                }

                if (CheckEmployeeCode(employee.EmployeeCode))
                {
                    error.UserMsg = "Mã nhân viên bị trùng";
                    return BadRequest(error);
                }

                if (string.IsNullOrEmpty(employee.FullName))
                {
                    error.UserMsg = "Họ và tên không được phép để trống";
                    return BadRequest(error);
                }

                bool ValidateEmailAddress(string emailAddress)
                {
                    try
                    {
                        var email = new MailAddress(emailAddress);
                        return email.Address == emailAddress.Trim();
                    }
                    catch
                    {
                        return false;
                    }
                }
                if (!ValidateEmailAddress(employee.Email))
                {
                    error.UserMsg = "Email không đúng định dạng";
                    return BadRequest(error);

                }

                // Khai báo connection string
                var connectionString = "User Id=root;Host=localhost;Character Set=utf8;Database=misa.web07_mf1735_mvduy";
                //Tạo kết nối tới cơ sở dữ liệu
                var connection = new MySqlConnector.MySqlConnection(connectionString);


                //Tạo câu truy vấnz
                var sql = "INSERT INTO employee (EmployeeId, EmployeeCode, FullName, Gender, DateOfBirth, IdentityNumber, IdentityDate, IdentityPlace, PositionName, Address, Email, PhoneNumber, BankName, BankNumber, CreatedBy, CreatedDate, ModifiedDate, ModifiedBy, DepartmentId)\r\n  VALUES (@EmployeeId, @EmployeeCode, @FullName, @Gender, @DateOfBirth, @IdentityNumber, @IdentityDate, @IdentityPlace, @PositionName, @Address, @Email, @PhoneNumber, @BankName, @BankNumber, @CreatedBy, @CreatedDate, @ModifiedDate, @ModifiedBy, @DepartmentId);";

                // Thực hiện post dữ liệu
                //Tạo mới EmployeeID
                employee.EmployeeId = Guid.NewGuid();
                var result = await connection.ExecuteAsync(sql: sql, param: employee);

                if (result > 0)
                {
                    return StatusCode(201, result);
                }
                else return Ok(result);


            }
            catch (Exception ex)
            {
                var result = HandleException(ex);
                return result;
            }


        }
        /// <summary>
        /// Kiểm tra mã nhân viên có bị trùng hay không
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>
        /// true - đã bị trùng;
        /// false - không trùng;
        /// </returns>
        private bool CheckEmployeeCode(string employeeCode)
        {
            var connectionString = "User Id=root;Host=localhost;Character Set=utf8;Database=misa.web07_mf1735_mvduy";
            //Tạo kết nối tới cơ sở dữ liệu
            var connection = new MySqlConnector.MySqlConnection(connectionString);
            var sqlCheck = "SELECT EmployeeCode FROM Employee WHERE EmployeeCode = @EmployeeCode";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("EmployeeCode", employeeCode);
            var res = connection.QueryFirstOrDefault<string>(sqlCheck, dynamicParams);
            if (res == null)
            {
                return false;
            }
            return true;
        }


        [HttpPut]
        [Route("{employeeId}")]
        public async Task<IActionResult> EditEmployee(string employeeId, EmployeeEntity employee)
        {
            try
            {
                var error = new ErrorService();

                var connectionString = "User Id=root;Host=localhost;Character Set=utf8;Database=misa.web07_mf1735_mvduy";
                //"Server=18.179.16.166;Port=3306;Database=NVMANH.DEMO;Uid=nvmanh;Pwd=12345678;"

                //Tạo kết nối tới cơ sở dữ liệu
                var connection = new MySqlConnector.MySqlConnection(connectionString);



                // Check xem nhân viên có tồn tại không
                var Employee = await GetEmployeeAsync(employeeId);
                if (Employee == null)
                {
                    error.UserMsg = "Nhân viên không tồn tại";
                    return BadRequest(error);
                }

                //Tạo câu truy vấnz
                var sql = @"UPDATE employee e
            SET
                e.EmployeeCode = @EmployeeCode,
                e.FullName = @FullName,
                e.Gender = @Gender,
                e.DateOfBirth = @DateOfBirth,
                e.IdentityNumber = @IdentityNumber,
                e.IdentityDate = @IdentityDate,
                e.IdentityPlace = @IdentityPlace,
                e.PositionName = @PositionName,
                e.Address = @Address,
                e.Email = @Email,
                e.PhoneNumber = @PhoneNumber,
                e.BankName = @BankName,
                e.BankNumber = @BankNumber,
                e.CreatedBy = @CreatedBy,
                e.CreatedDate = @CreatedDate,
                e.ModifiedDate = @ModifiedDate,
                e.ModifiedBy = @ModifiedBy,
                e.DepartmentId = @DepartmentId
            WHERE e.EmployeeId = @EmployeeId;";



                var param = new DynamicParameters();
                param.Add("EmployeeCode", employee.EmployeeCode);
                param.Add("FullName", employee.FullName);
                param.Add("Gender", employee.Gender);
                param.Add("DateOfBirth", employee.DateOfBirth);
                param.Add("IdentityNumber", employee.IdentityNumber);
                param.Add("IdentityDate", employee.IdentityDate);
                param.Add("IdentityPlace", employee.IdentityPlace);
                param.Add("PositionName", employee.PositionName);
                param.Add("Address", employee.Address);
                param.Add("Email", employee.Email);
                param.Add("PhoneNumber", employee.PhoneNumber);
                param.Add("BankName", employee.BankName);
                param.Add("BankNumber", employee.BankNumber);
                param.Add("CreatedBy", employee.CreatedBy);
                param.Add("CreatedDate", employee.CreatedDate);
                param.Add("ModifiedDate", employee.ModifiedDate);
                param.Add("ModifiedBy", employee.ModifiedBy);
                param.Add("DepartmentId", employee.DepartmentId);
                param.Add("EmployeeId", employeeId);


                // Thực hiện sửa dữ liệu

                var result = await connection.ExecuteAsync(sql: sql, param: employee);

                if (result > 0)
                {
                    return StatusCode(201, result);
                }
                else return Ok(result);

            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }


        }


        [HttpDelete]
        [Route("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeAsync(string employeeId)
        {

            try
            {
                var error = new ErrorService();

                // Khai báo connection string
                var connectionString = "User Id=root;Host=localhost;Character Set=utf8;Database=misa.web07_mf1735_mvduy";
                //"Server=18.179.16.166;Port=3306;Database=NVMANH.DEMO;Uid=nvmanh;Pwd=12345678;"

                //Tạo kết nối tới cơ sở dữ liệu
                var connection = new MySqlConnector.MySqlConnection(connectionString);


                var employee = await GetEmployeeAsync(employeeId);

                if (employee == null)
                {
                    error.UserMsg = "Nhân viên không tồn tại";
                    return BadRequest(error);
                }

                //Tạo câu truy vấnz
                var sql = $"DELETE FROM employee WHERE EmployeeId = @employeeId";

                //Tạo Param chống sql injection
                var param = new DynamicParameters();
                param.Add("employeeId", employeeId);

                var result = await connection.ExecuteAsync(sql, param);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
