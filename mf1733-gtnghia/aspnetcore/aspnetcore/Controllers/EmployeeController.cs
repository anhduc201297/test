using Dapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Text.RegularExpressions;

namespace aspnetcore
{
    [Route("api/v1/Employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IDBConnectionService _dbConnectionService;

        public EmployeeController(IDBConnectionService dbConnectionService)
        {
            this._dbConnectionService = dbConnectionService;
        }

        /// <summary>
        /// [GET] https://localhost:7250/api/v1/Employees
        /// Get all employees from database
        /// Created by: mf1733-gtnghia (10/9/2023)
        /// </summary>
        /// <returns>List<EmployeeEntity></returns>
        [HttpGet]
        public async Task<dynamic> GetAllEmployeeAsync()
        {
            // declare connect string
            var connection = _dbConnectionService.CreateConnection();

            // create sql
            var sql = "CALL Proc_Employee_GetAll()";

            try
            {

                // get data
                var result = await connection.QueryAsync<EmployeeEntity>(sql);

                return result.ToList();

            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse("MISAE-006", Guid.NewGuid());

                return StatusCode(500, errorResponse);
            }
        }

        /// <summary>
        /// [GET] https://localhost:7250/api/v1/Employees/Paging
        /// <param name="pageNumber" type="int">number of page</param>
        /// <param name="pageSize" type="int">number of records in each page</param>
        /// Get all employees pagination
        /// Created by: mf1733-gtnghia (10/9/2023)
        /// </summary>
        /// <returns>List<EmployeeEntity></returns>
        [HttpGet]
        [Route("Paging")]
        public async Task<dynamic> GetAllEmployeePagingAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var connection = _dbConnectionService.CreateConnection();

            var sql = $"CALL Proc_Employee_Paging({pageNumber},{pageSize});";

            try
            {

                var result = await connection.QueryAsync<EmployeeEntity>(sql);

                return result.ToList();

            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse("MISAE-006", Guid.NewGuid());

                return StatusCode(500, errorResponse);
            }
        }

        /// <summary>
        /// [GET] https://localhost:7250/api/v1/Employees/:id
        /// <param name="employeeId" type="Guid">employeeId</param>
        /// Get employee by EmployeeId
        /// Created by: mf1733-gtnghia (10/9/2023)
        /// </summary>
        /// <returns>EmployeeEntity</returns>
        [HttpGet]
        [Route("{employeeId}")]
        public async Task<dynamic> GetEmployeeAsync(Guid employeeId)
        {
            var connection = _dbConnectionService.CreateConnection();

            var sql = $"CALL Proc_Employee_GetById(@employeeId)";

            // create param defense sql injection
            var param = new DynamicParameters();
            param.Add("employeeId", employeeId);

            try
            {

                // get data
                var result = await connection.QueryFirstOrDefaultAsync<EmployeeEntity>(sql, param);
                if (result == null)
                {
                    throw new NotFoundException("Employee id not found!");
                }

                return result;
            }
            catch (NotFoundException ex)
            {
                var errorResponse = new ErrorResponse("MISAE-005", Guid.NewGuid(), ex.Message, "Không tìm thấy mã nhân viên", "Mã nhân viên muốn xóa không tồn tại");

                return StatusCode(404, errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse("MISAE-006", Guid.NewGuid());

                return StatusCode(500, errorResponse);
            }

        }

        /// <summary>
        /// [GET] https://localhost:7250/api/v1/Employees/NewEmployeeCode
        /// Get new employee code ( max of EmployeeCode + 1 )
        /// Created by: mf1733-gtnghia (10/9/2023)
        /// </summary>
        /// <returns type="int">EmployeeCode</returns>
        [HttpGet]
        [Route("NewEmployeeCode")]
        public async Task<dynamic> GetNewEmployeeCodeAsync()
        {
            var connection = _dbConnectionService.CreateConnection();

            // create sql
            var sql = "CALL Proc_Employee_NewEmployeeCode()";
            try
            {

                // get data
                var result = await connection.QueryFirstOrDefaultAsync<string>(sql);

                string input = result;
                // Pattern explain: \d+$
                // \d : [0-9]
                // + : many characters
                // $ : iterator from the last index
                string pattern = @"\d+$";

                Match match = Regex.Match(input, pattern);

                if (match.Success)
                {
                    string numberPart = match.Value;
                    // TryParse: convert string to int and return boolean value
                    // second args: out int number = result
                    if (int.TryParse(numberPart, out int number))
                    {
                        result = input.Replace(numberPart, (number + 1).ToString());
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy mã nhân viên");
                    }
                }
                else
                {
                    Console.WriteLine("Không tìm thấy mã nhân viên");
                }

                return result;
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse("MISAE-003", Guid.NewGuid());


                return StatusCode(500, errorResponse);
            }
        }

        /// <summary>
        /// [POST] https://localhost:7250/api/v1/Employees
        /// <param name="employee">Employee Object from request</param>
        /// Create new employee
        /// Created by: mf1733-gtnghia (10/9/2023)
        /// </summary>
        /// <returns>1</returns>
        /// <exception cref="Exception">Msg:Tên đầy đủ không hợp lệ</exception>
        [HttpPost]
        public async Task<dynamic> CreateEmployeeAsync([FromBody] EmployeeEntity employee)
        {

            var connection = _dbConnectionService.CreateConnection();

            #region handle request body
            if (employee.EmployeeCode == null)
            {
                throw new Exception("Không được phép để trống Mã nhân viên");
            }

            employee.EmployeeId = Guid.NewGuid();

            int lastSpaceIndex = employee.FullName.LastIndexOf(' '); // split fullname by last space (' ') index

            string firstName = "";
            string lastName = "";

            if (lastSpaceIndex != -1)
            {
                firstName = employee.FullName.Substring(lastSpaceIndex + 1); // Lấy phần từ cuối cùng trong tên
                lastName = employee.FullName.Substring(0, lastSpaceIndex);    // Lấy phần còn lại

            }
            else
            {
                Console.WriteLine("Tên đầy đủ không hợp lệ");
            }

            string genderName = Enum.GetName(typeof(GenderEnum), employee.Gender);

            Console.WriteLine("EmployeeId: " + employee.EmployeeId);
            Console.WriteLine("EmployeeCode: " + employee.EmployeeCode);
            Console.WriteLine("FullName: " + employee.FullName);
            Console.WriteLine("FirstName: " + firstName);
            Console.WriteLine("LastName: " + lastName);
            Console.WriteLine("DateOfBirth: " + employee.DateOfBirth);
            Console.WriteLine("Gender: " + employee.Gender);
            Console.WriteLine("GenderName: " + genderName);
            Console.WriteLine("IdentityNumber: " + employee.IdentityNumber);
            Console.WriteLine("IdentityDate: " + employee.IdentityDate);
            Console.WriteLine("IdentityPlace: " + employee.IdentityPlace);
            Console.WriteLine("Salary: " + employee.Salary);
            Console.WriteLine("PhoneNumber: " + employee.PhoneNumber);
            Console.WriteLine("Email: " + employee.Email);
            Console.WriteLine("DepartmentId: " + employee.DepartmentId);
            Console.WriteLine("PositionName: " + employee.PositionName);
            Console.WriteLine("BankAccount: " + employee.BankAccount);
            Console.WriteLine("BankName: " + employee.BankName);
            Console.WriteLine("BankBranch: " + employee.BankBranch);
            #endregion

            var sql = $"CALL Proc_Employee_Create(" +
                $"'{employee.EmployeeId}'," + // employee id
                $"'{employee.EmployeeCode}'," +
                $"'{employee.FullName}'," +
                $"'{firstName}'," +
                $"'{lastName}'," +
                $"'{employee.Gender}'," +
                $"'{genderName}'," +
                $"'{employee.DateOfBirth}'," +
                $"'{employee.DepartmentId}'," +
                $"'{employee.CreatedBy}'," +
                $"'{employee.CreatedDate}'," +
                $"'{employee.ModifiedBy}'," +
                $"'{employee.ModifiedDate}'," +
                $"'{employee.Email}'," +
                $"'{employee.PhoneNumber}'," +
                $"'{employee.IdentityNumber}'," +
                $"'{employee.IdentityDate}'," +
                $"'{employee.IdentityPlace}'," +
                $"'{employee.Address}'," +
                $"'{employee.Salary}'," +
                $"'{employee.PersonalTaxCode}'," +
                $"1," + // work status
                $"'{Guid.NewGuid()}'," + // position id
                $"'{employee.PositionCode}'," +
                $"'{employee.PositionName}'," +
                $"'{employee.joinDate}'," +
                $"'{employee.MaritalStatus}'," + // marital status
                $"'married'," + // marital status name
                $"'{employee.EducationalBackground}'," +
                $"'bachelor'," + // educational background name
                $"'{employee.QualificationName}'," +
                $"'{employee.BankAccount}'," +
                $"'{employee.BankName}'," +
                $"'{employee.BankBranch}')";

            try
            {

                // get data
                var result = await connection.ExecuteAsync(sql);

                return result;
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse("MISAE-004", Guid.NewGuid());


                return StatusCode(500, errorResponse);
            }
        }

        /// <summary>
        /// [DELETE] https://localhost:7250/api/v1/Employees
        /// <param name="employeeId" type="Guid">EmployeeId</param>
        /// Delete employee by EmployeeId
        /// Created by: mf1733-gtnghia (10/9/2023)
        /// </summary>
        /// <returns>1 (success), 0 (fail)</returns>
        /// <exception cref="Exception">Msg: "Không tồn tại nhân viên"</exception>
        [HttpDelete]
        [Route("{employeeId}")]
        public async Task<dynamic> DeleteEmployeeAsync(Guid employeeId)
        {
            var connection = _dbConnectionService.CreateConnection();

            // Check employee exist before DELETE, but we need have await keyword before call to GetEmployeeAsync
            // to ensure that API GetEmployeeAsync complete execute before if(employee == null) was run
            var employee = await GetEmployeeAsync(employeeId);
            try
            {

                if (employee == null)
                {
                    throw new NotFoundException("Employee id not found");
                }
                Console.WriteLine("Id: " + employeeId);
                // create sql
                var sql = $"CALL Proc_Employee_DeleteById('{employeeId}')";

                // get data
                var result = await connection.ExecuteAsync(sql);

                return result;
            }
            catch (NotFoundException ex)
            {
                var errorResponse = new ErrorResponse("MISAE-005", Guid.NewGuid(),ex.Message, "Không tìm thấy mã nhân viên", "Mã nhân viên muốn xóa không tồn tại");

                return StatusCode(404, errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse("MISAE-006", Guid.NewGuid());

                return StatusCode(500, errorResponse);
            }
        }

    }
}
