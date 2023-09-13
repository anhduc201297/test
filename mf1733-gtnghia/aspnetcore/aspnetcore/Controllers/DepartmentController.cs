using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace aspnetcore
{
    [Route("api/v1/Departments")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly IDBConnectionService _dbConnectionService;

        public DepartmentController(IDBConnectionService dbConnectionService)
        {
            this._dbConnectionService = dbConnectionService;
        }

        /// <summary>
        /// [GET] https://localhost:7250/api/v1/Departments
        /// Get all departments from database
        /// Created by: mf1733-gtnghia 7/9/2023
        /// </summary>
        /// <returns>List(DepartmentEntity)</returns>
        [HttpGet]
        public async Task<dynamic> GetAllDepartmentsAsync()
        {
            var connection = _dbConnectionService.CreateConnection();

            // create sql
            var sql = "CALL Proc_Department_GetAll()";
            try
            {

                // get data
                var result = await connection.QueryAsync<DepartmentEntity>(sql);

                return result.ToList();
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse("MISAE-006", Guid.NewGuid());

                return StatusCode(500, errorResponse);
            }

        }

        /// <summary>
        /// [GET] https://localhost:7250/api/v1/Departments
        /// <param name="departmentId"></param>
        /// Get specified department by departmentId (Guid) from database
        /// Created by: mf1733-gtnghia 7/9/2023
        /// <returns>DepartmentEntity</returns>
        [HttpGet]
        [Route("{departmentId}")]
        public async Task<dynamic> GetDepartmentAsync(Guid departmentId)
        {
            var connection = _dbConnectionService.CreateConnection();

            var sql = "CALL Proc_Department_GetById(@departmentId)";
            var param = new DynamicParameters();
            param.Add("departmentId", departmentId);
            try
            {

                var result = await connection.QueryFirstOrDefaultAsync<DepartmentEntity>(sql, param);

                return result;
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse("MISAE-006", Guid.NewGuid());

                return StatusCode(500, errorResponse);
            }
        }

        /// <summary>
        /// [DELETE] https://localhost:7250/api/v1/Departments
        /// <param name="departmentId" type="Guid">Department Id</param>
        /// Delele specified department by departmentId (Guid) from database
        /// Created by: mf1733-gtnghia 7/9/2023
        /// <returns>DepartmentEntity</returns>
        [HttpDelete]
        [Route("{departmentId}")]
        public async Task<dynamic> DeleteDepartmentAsync(Guid departmentId)
        {
            var connection = _dbConnectionService.CreateConnection();

            var department = await GetDepartmentAsync(departmentId);

            if (department == null)
            {
                throw new NotFoundException("Không tồn tại Mã phòng ban");
            }

            var sql = "CALL Proc_Department_DeleteById('@departmentId')";
            var param = new DynamicParameters();
            param.Add("departmentId", departmentId);
            try
            {

            var result = await connection.QueryFirstOrDefaultAsync<DepartmentEntity>(sql, param);

            return result;
            } catch (NotFoundException ex)
            {
                var errorResponse = new ErrorResponse("MISAE-005", Guid.NewGuid(), ex.Message, "Không tìm thấy mã phòng ban", "Mã phòng ban muốn xóa không tồn tại");

                return StatusCode(404, errorResponse);
            } catch (Exception ex) 
            {
                var errorResponse = new ErrorResponse("MISAE-006", Guid.NewGuid());

                return StatusCode(500, errorResponse);
            }
        }

        /// <summary>
        /// [POST] https://localhost:7250/api/v1/Departments
        /// <param name="department" type="DepartmentEntity">Department object from body</param>
        /// Create new department
        /// Created by: mf1733-gtnghia 7/9/2023
        /// </summary>
        /// <returns>1</returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<dynamic> CreateDepartmentAsync([FromBody] DepartmentEntity department)
        {
            var connection = _dbConnectionService.CreateConnection();
            try
            {
                #region handle request body
                if (department.DepartmentCode == null)
                {
                    throw new Exception("Không được để trống Mã phòng ban");
                }
                else if (department.DepartmentName == null)
                {
                    throw new Exception("Không được để trống Tên phòng ban");
                }
                department.DepartmentId = Guid.NewGuid();

                // need to helper to reuse formated function
                string formatedCreatedDate = "";
                string formatedModifiedDate = "";

                if (DateTime.TryParse(department.CreatedDate.ToString(), out DateTime createdDate) &&
                    DateTime.TryParse(department.ModifiedDate.ToString(), out DateTime modifiedDate))
                {
                    formatedCreatedDate = createdDate.ToString("yyyy-MM-dd HH:mm:ss");
                    formatedModifiedDate = modifiedDate.ToString("yyyy-MM-dd HH:mm:ss");

                    Console.WriteLine(department.CreatedDate + " " + formatedCreatedDate);
                    Console.WriteLine(department.ModifiedDate + " " + formatedModifiedDate);
                }
                else
                {
                    throw new Exception("Lỗi thời gian");
                }
                #endregion

                var sql = $"CALL Proc_Department_Create(" +
                    $"'{department.DepartmentId}'," +
                    $"'{department.DepartmentCode}'," +
                    $"'{department.DepartmentName}'," +
                    $"'{department.CreatedBy}'," +
                    $"'{formatedCreatedDate}'," +
                    $"'{department.ModifiedBy}'," +
                    $"'{formatedModifiedDate}');";

                var result = await connection.QueryFirstOrDefaultAsync(sql);

                return 1;
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse("MISAE-011", Guid.NewGuid());

                return StatusCode(500, errorResponse);
            }
        }
    }
}
