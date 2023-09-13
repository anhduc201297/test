using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.ComponentModel.DataAnnotations;

namespace MISA.Web202307.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// Lấy danh sách tất cả nhân viên
        /// </summary>
        /// <returns>danh sách bản ghi</returns>
        /// Created by: Vũ Tùng Lâm (11/09/2023)
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                string storedProcedureName = "Proc_employee_GetAll";
                var connection = new MySqlConnection(DatabaseContext.ConnectionString);
                var result = await connection.QueryAsync<EmployeeEntity>(storedProcedureName, commandType:System.Data.CommandType.StoredProcedure);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch(Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Lấy thông tin nhân viên theo ID
        /// </summary>
        /// <param name="employeeId"> ID nhân viên</param>
        /// <returns>Thông tin bản ghi</returns>
        /// Created by: Vũ Tùng Lâm (11/09/2023)
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] Guid employeeId)
        {
            try
            {
                string storedProcedureName = "Proc_employee_GetByID";
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeId", employeeId);
                var connection = new MySqlConnection(DatabaseContext.ConnectionString);

                //Gọi vào DB
                var result = await connection.QueryFirstOrDefaultAsync<EmployeeEntity>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                //Xử lí kết quả trả về
                if (result != null)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên</param>
        /// <returns>ID nhân viên thêm mới</returns>
        /// Created by: Vũ Tùng Lâm (11/09/2023)
        [HttpPost]
        public async Task<IActionResult> InsertEmployee([FromBody] EmployeeEntity employee)
        {
            try
            {
                var check = await CheckDuplicateEmployeeCode(employee.EmployeeCode, employee.EmployeeId);
                if (check)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = ErrorCode.DuplicateCode,
                        DevMsg = "Duplicate Data",
                        UserMsg = "Mã nhân viên đã tồn tại trong hệ thống",
                        MoreInfo = "https://openapi.misa.com.vn/errorcode/6",
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
                employee.EmployeeId = Guid.NewGuid();
                string storedProcedureName = "Proc_employee_Insert";
                var connection = new MySqlConnection(DatabaseContext.ConnectionString);

                // Gọi vào DB
                int numberOfRowsAffected = await connection.ExecuteAsync(storedProcedureName, employee, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lí kết quả trả về
                if (numberOfRowsAffected > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, employee.EmployeeId);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.InsertFailed,
                    DevMsg = "Data insert failed",
                    UserMsg = "Thêm nhân viên thất bại",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/2",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Sửa nhân viên
        /// </summary>
        /// <param name="employeeId">Id nhân viên</param>
        /// <param name="employee">Thông tin nhân viên</param>
        /// <returns>ID nhân viên thêm mới</returns>
        /// Created by: Vũ Tùng Lâm (11/09/2023)
        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid employeeId ,[FromBody] EmployeeEntity employee)
        {
            try
            {
                var check = await CheckDuplicateEmployeeCode(employee.EmployeeCode, employeeId);
                if (check)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = ErrorCode.DuplicateCode,
                        DevMsg = "Duplicate Data",
                        UserMsg = "Mã nhân viên đã tồn tại trong hệ thống",
                        MoreInfo = "https://openapi.misa.com.vn/errorcode/6",
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
                employee.ModifiedDate = DateTime.Now;
                string storedProcedureName = "Proc_employee_Update";
                var connection = new MySqlConnection(DatabaseContext.ConnectionString);

                // Gọi vào DB
                int numberOfRowsAffected = await connection.ExecuteAsync(storedProcedureName, employee, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lí kết quả trả về
                if (numberOfRowsAffected > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, employeeId);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.UpdateFailed,
                    DevMsg = "Data update failed",
                    UserMsg = "Sửa nhân viên thất bại",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/3",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        /// <param name="employeeId"> ID nhân viên</param>
        /// <returns>Thông tin bản ghi</returns>
        /// Created by: Vũ Tùng Lâm (11/09/2023)
        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid employeeId)
        {
            try
            {
                string storedProcedureName = "Proc_employee_Delete";
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeId", employeeId);
                var connection = new MySqlConnection(DatabaseContext.ConnectionString);

                // Gọi vào DB
                int numberOfRowsAffected = await connection.ExecuteAsync(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                //Xử lí kết quả trả về
                if (numberOfRowsAffected > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, employeeId);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.DeleteFailed,
                    DevMsg = "Data delete failed",
                    UserMsg = "Xóa nhân viên thất bai",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/4",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }


        /// <summary>
        /// Xử lí Exception
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Status 500 và lỗi</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        protected IActionResult HandleException(Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
            {
                ErrorCode = ErrorCode.Exception,
                DevMsg = e.Message,
                UserMsg = "Có lỗi xảy ra vui lòng liên hệ MISA để được hỗ trợ.",
                MoreInfo = "https://openapi.misa.com.vn/errorcode/1",
                TraceId = HttpContext.TraceIdentifier
            });
        }

        /// <summary>
        /// Kiểm tra trùng mã
        /// </summary>
        /// <param name="employeeCode"> Mã nhân viên</param>
        /// <param name="employeeId">Id nhân viên</param>
        /// <returns>true nếu trùng, false nếu không trùng</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        private async Task<bool> CheckDuplicateEmployeeCode(string employeeCode, Guid employeeId)
        {
            // Chuẩn bị câu lệnh MySQL
            string storedProcedureName = "Proc_employee_GetEmpoyeeCode";
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeCode", employeeCode);
            var connection = new MySqlConnection(DatabaseContext.ConnectionString);

            // Thực hiện gọi vào DB
            var res = await connection.QueryFirstOrDefaultAsync<EmployeeEntity>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            // Xử lí kết quả trả về
            if (res == null)
            {
                return false;
            }
            else if (res.EmployeeId == employeeId)
            {
                return false;
            }
            return true;
        }
    }
}
