using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Dapper;
namespace MISA.Web202307.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        /// <summary>
        /// Lấy danh sách tất cả phòng ban
        /// </summary>
        /// <returns>danh sách phòng ban</returns>
        /// Created by: Vũ Tùng Lâm (11/09/2023)
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                string storedProcedureName = "Proc_department_GetAll";
                var connection = new MySqlConnection(DatabaseContext.ConnectionString);
                var result = await connection.QueryAsync<DepartmentEntity>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception e)
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
        }
    }
}
