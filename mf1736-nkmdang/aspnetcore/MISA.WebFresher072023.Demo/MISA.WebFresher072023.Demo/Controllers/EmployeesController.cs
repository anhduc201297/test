using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher072023.Demo.Config;
using MISA.WebFresher072023.Demo.Enum;
using MISA.WebFresher072023.Demo.Entity;
using MISA.WebFresher072023.Demo.ErrorHandler;
using MISA.WebFresher072023.Demo.SQL;
using static Dapper.SqlMapper;
using Microsoft.AspNetCore.Http;

namespace MISA.WebFresher072023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        /// <summary>
        /// Lấy ra toàn bộ dữ liệu về nhân viên (chưa bao gồm các thông tin từ các bảng liên kết như DepartmentName,...)
        /// Author: NKMDANG 10/9/2023
        /// </summary>
        /// <returns>EmployeeEntity nếu thành công, mã lỗi nếu thất bại</returns>
        [HttpGet]
        public async Task<dynamic> GetAllEmployeesAsync()
        {
            try
            {
                // Tạo connection đến cơ sở dữ liệu
                var connection = AccessDatabase.ConnectDatabase();

                // Tạo câu truy vấn
                var sql = EmployeeSQL.GetAllEmployeesSQL();

                // Thực hiện truy vấn
                var result = await connection.QueryAsync<EmployeeEntity>(sql);
                return result.ToList();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);  
                return StatusCode(500, ServerErrorMessage.ServerUnknownError);
            }
        }

        /// <summary>
        /// Lấy ra thông tin nhân viên theo Id (char(36))
        /// Author: NKMDANG 10/9/2023
        /// </summary>
        /// <param name="employeeId">Tham số nhận vào từ route</param>
        /// <returns>Thông tin nhân viên nếu thành công, mã lỗi nếu thất bại</returns>
        [HttpGet]
        [Route("{employeeId}")]
        public async Task<dynamic> GetEmployeeByIdAsync(Guid employeeId)
        {
            // Tạo connection đến cơ sở dữ liệu
            var connection = AccessDatabase.ConnectDatabase();

            // Tạo câu truy vấn
            var sql = EmployeeSQL.GetEmployeeByIdSQL(employeeId);


            try
            {

                var result = await connection.QuerySingleAsync<EmployeeEntity>(sql);

                return result;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                var errorResponse = new ErrorResponse(ex.Message.ToString(), EmployeeUserMessageEnum.EmployeeNotFound);
                return StatusCode(404, errorResponse);
            }


        }

        /// <summary>
        /// Lấy thông tin một nhân viên theo Mã nhân viên
        /// Author: NKMDANG 10/9/2023
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("EmployeeCode")]
        public async Task<dynamic> GetEmployeeByEmployeeCodeAsync(string employeeCode)
        {
            // Tạo connection đến cơ sở dữ liệu
            var connection = AccessDatabase.ConnectDatabase();

            // Tạo câu truy vấn
            var sql = EmployeeSQL.GetEmployeeByEmployeeCodeSQL();

            // Tạo param để tránh SQL Injection
            var param = new DynamicParameters();
            param.Add("EmployeeCode", employeeCode);

            try
            {

                var result = await connection.QuerySingleAsync<EmployeeEntity>(sql, param);

                return result;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                
                return StatusCode(404, EmployeeUserMessageEnum.EmployeeNotFound);
            }


        }

        /// <summary>
        /// Sửa thông tin nhân viên theo Id
        /// Author: NKMDANG 10/9/2023
        /// </summary>
        /// <param name="employeeId">Id của nhân viên trong database: char(36)</param>
        /// <param name="employeeFormData">Object chứa thông tin nhân viên {EmployeeCode (string), FullName (string), DateOfBirth (DateTime), Gender (int [0-2]{1})</param>
        /// <returns>Thông tin nhân viên nếu tìm thấy, mã lỗi 404 nếu không thấy, mã lỗi 500 khi có lỗi khác</returns>
        [HttpPut]
        [Route("{employeeId}")]
        public async Task<dynamic> UpdateEmployeeById(Guid employeeId, [FromBody] EmployeeEntityNoId employeeFormData)
        {
            // Tạo kết nối đến cơ sở dữ liệu
            var connection = AccessDatabase.ConnectDatabase();

            // Tạo câu lệnh sql
            //var sql = EmployeeSQL.UpdateEmployeeByIdSQL(employeeId, employeeFormData);
            string sql = "";
            try
            {
                sql = EmployeeSQL.GetEmployeeByIdSQL(employeeId);
                var existEmployee = await connection.QueryFirstOrDefaultAsync<EmployeeEntity>(sql);

                // Nhân viên đã có trong database
                if(existEmployee != null )
                {
                    sql = EmployeeSQL.UpdateEmployeeByIdSQL(employeeId);

                    var result = await connection.QueryAsync<EmployeeEntity>(sql, employeeFormData);
                    var response = new SuccessResponse(EmployeeUserMessageEnum.UpdateEmployeeSuccess, employeeFormData);
                    
                    return StatusCode(200, response);
                }
                else
                {
                    return StatusCode(404, EmployeeUserMessageEnum.EmployeeNotFound);
                } 
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                var errorResponse = new ErrorResponse(ex.Message, ServerErrorMessage.ServerUnknownError);
                return StatusCode(500, errorResponse);
            }
        }


        /// <summary>
        /// Thêm mới một nhân viên
        /// Author: NKMDANG 10/9/2023
        /// </summary>
        /// <param name="employeeFormData">Object chứa thông tin nhân viên {EmployeeCode (string), FullName (string), DateOfBirth (DateTime), Gender (int [0-2]{1})}</param>
        /// <returns>Thông tin của nhân viên đã thêm mới</returns>
        [HttpPost]
        [Route("")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<dynamic> CreateOneEmployeeAsync([FromBody] EmployeeEntityNoId employeeFormData)
        {

            // Tạo connection đến cơ sở dữ liệu
            var connection = AccessDatabase.ConnectDatabase();

            try
            {
                string sql = "";
                
                // Kiểm tra mã nhân viên đã tồn tại
                sql = EmployeeSQL.GetEmployeeByEmployeeCodeSQL();
                var paramEmployeeCode = new DynamicParameters();
                paramEmployeeCode.Add("EmployeeCode", employeeFormData.EmployeeCode);
                var existEmployee = await connection.QueryFirstOrDefaultAsync<EmployeeEntity>(sql, paramEmployeeCode);
                //Console.WriteLine(existEmployee.ToString());
                if(existEmployee != null ) 
                {
                    var clientRespone = new ErrorResponse("Không thể thêm mới nhân viên", "Mã nhân viên đã tồn tại! Vui lòng nhập mã nhân viên khác.");
                    return StatusCode(400, clientRespone);
                }

                sql = EmployeeSQL.CreateOneEmployeeWithDepartmentIdSQL();

                Console.WriteLine(sql);
                var result = await connection.ExecuteAsync(sql, employeeFormData);

                var clientResponse = new SuccessResponse(EmployeeUserMessageEnum.CreateNewEmployeeSuccess, employeeFormData);

                return StatusCode(201, clientResponse);
                
            }
            catch (Exception ex)
            {
                var errorRespone = new ErrorResponse(ex.Message, ServerErrorMessage.ServerUnknownError);
                return StatusCode(500, errorRespone);
            }
        }


        /// <summary>
        /// Xóa 1 nhân viên theo ID
        /// Author: NKMDANG 10/9/2023
        /// </summary>
        /// <param name="employeeId">ID của nhân viên (Guid)</param>
        /// <returns>Số nguyên > 0 nếu thành công</returns>
        /// <exception cref="Exception"></exception>
        [HttpDelete]
        [Route("{employeeId}")]
        public async Task<dynamic> DeleteEmployeeByIdAsync(Guid employeeId)
        {
            // Tạo connection đến cơ sở dữ liệu
            var connection = AccessDatabase.ConnectDatabase();


            // Mở kết nối
            await connection.OpenAsync();

            // Tạo câu truy vấn
            //var sql = $"DELETE FROM Employee WHERE EmployeeId = '{employeeId}'";
            string sql;

            try
            {
                sql = EmployeeSQL.GetEmployeeByIdSQL(employeeId);
                var notFoundEmployee = await connection.QueryFirstOrDefaultAsync(sql);
                if(notFoundEmployee == null) 
                {
                    return StatusCode(404, EmployeeUserMessageEnum.EmployeeNotFound);
                }
                //var existEmployee = await GetEmployeeByIdAsync(employeeId);
                //Console.WriteLine(existEmployee);
                //if (String.Compare(existEmployee.ToString(), "Microsoft.AspNetCore.Mvc.ObjectResult") == 0)
                //{
                //    return StatusCode(404, "Không tìm thấy nhân viên");
                //}

                //sql = $"DELETE FROM Employee WHERE EmployeeId = '{employeeId}'";
                sql = EmployeeSQL.DeleteOneEmployeeByEmployeeIdSQL(employeeId);
                var result = await connection.ExecuteAsync(sql);

                // Đóng kết nối
                await connection.CloseAsync();
                var clientRespone = new SuccessResponse(EmployeeUserMessageEnum.DeleteEmployeeSuccess);
                return clientRespone;
            }
            catch (Exception ex)
            {
                var errorRespone = new ErrorResponse(ex.Message, ServerErrorMessage.ServerUnknownError);
                return StatusCode(500, errorRespone);
            };

        }

    }
}
