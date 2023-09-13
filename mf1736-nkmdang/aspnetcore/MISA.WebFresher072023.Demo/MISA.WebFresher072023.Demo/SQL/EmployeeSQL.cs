using MISA.WebFresher072023.Demo.Entity;

namespace MISA.WebFresher072023.Demo.SQL
{
    public class EmployeeSQL
    {
        /// <summary>
        /// Tạo câu lệnh SQL lấy toàn bộ thông tin nhân viên trong bảng Employee
        /// </summary>
        /// <returns>sql (string)</returns>
        public static string GetAllEmployeesSQL()
        {
            var sql = "CALL Proc_Read_GetAllEmployees()";
            //var sql = "SELECT * FROM Employee";
            return sql;
        }

        /// <summary>
        /// Tạo câu lệnh SQL lấy thông tin nhân viên theo Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>sql (string)</returns>
        public static string GetEmployeeByIdSQL(Guid employeeId) 
        {
            //var sql = $"SELECT * FROM Employee WHERE EmployeeId = '{employeeId}'";
            string sql = $"CALL Proc_Read_EmployeeById('{employeeId}')";
            return sql;
        }

        /// <summary>
        /// Tạo câu lệnh SQL lấy thông tin nhân viên theo Mã nhân viên
        /// Cách sử dụng: await connection.ExecuteAsync(sql, employeeFormData);
        /// Author: Minh Đăng 11/09/2023
        /// </summary>
        /// <returns>sql (string)</returns>
        public static string GetEmployeeByEmployeeCodeSQL()
        {
            //string sql = $"SELECT * FROM Employee WHERE EmployeeCode = @EmployeeCode";
            string sql = "CALL Proc_Read_GetEmployeeByEmployeeCode(@EmployeeCode)";
            return sql;
        }


        /// <summary>
        /// Tạo câu lệnh SQL thay đổi thông tin nhân viên theo Id
        /// </summary>
        /// <returns>sql (string)</returns>
        public static string UpdateEmployeeByIdSQL(Guid employeeId)
        {
            //string sql = $"UPDATE Employee SET EmployeeCode = '{employeeFormData.EmployeeCode}', FullName = '{employeeFormData.FullName}', DateOfBirth = '{employeeFormData.DateOfBirth}', Gender = '{employeeFormData.Gender}' WHERE EmployeeId = '{employeeId}'";
            //Console.WriteLine(sql);
            //string sql = $"UPDATE Employee SET EmployeeCode = @EmployeeCode, FullName = @FullName, DateOfBirth = @DateOfBirth, Gender = @Gender WHERE EmployeeId = '{employeeId}'";
            //string execProcedure = "EXEC Proc_Employee_AddNewEmployee @EmployeeCode, @FullName, @DateOfBirth, @Gender";
            string sql = $"CALL Proc_Update_UpdateOneEmployeeById('{employeeId}', @EmployeeCode,  @FullName,  @DepartmentId,  @Gender,  @DateOfBirth,  @CreatedDate,  @CreatedBy, @ModifiedDate, @ModifiedBy,  @Address,  @BankName,  @BankBranch,  @AccountNumber,  @Email,  @Mobile,  @LandLinePhone,  @PersonalIdentification,  @PIDateCreated,  @PIPlaceCreated,  @PositionName)";
            return sql;
        }

        
        //public static string CreateOneEmployeeSQL()
        //{
        //    var sql = $"INSERT INTO Employee (FullName, DateOfBirth, Gender, EmployeeCode, EmployeeId) VALUES (@FullName, @DateOfBirth, @Gender, @EmployeeCode, @EmployeeId)";
        //    return sql;
        //    //string execProcedure = "CALL Proc_TenProc(@EmployeeCode, )";
        //    //return execProcedure;
        //}


        /// <summary>
        /// Tạo câu lệnh SQL để thêm mới một nhân viên
        /// Author: NKMDANG 10/09/2023
        /// </summary>
        /// <returns>sql (string)</returns>
        public static string CreateOneEmployeeWithDepartmentIdSQL()
        {
            //string sql = $"Call Proc_Create_CreateOneEmployeeWithoutDepartmentId(@EmployeeCode, @FullName, @DateOfBirth, @Gender)";
            string sql = $"Call Proc_Create_CreateOneEmployeeWithDepartmentId(@EmployeeCode,  @FullName,  @DepartmentId,  @Gender,  @DateOfBirth,  @CreatedDate,  @CreatedBy, @ModifiedDate, @ModifiedBy,  @Address,  @BankName,  @BankBranch,  @AccountNumber,  @Email,  @Mobile,  @LandLinePhone,  @PersonalIdentification,  @PIDateCreated,  @PIPlaceCreated,  @PositionName)";

            return sql;
        }

        /// <summary>
        /// Tạo câu lệnh SQL để xóa thông tin một nhân viên theo EmployeeId
        /// Author: NKMDANG 10/09/2023
        /// </summary>
        /// <param name="employeeId">Id của nhân viên (Guid)</param>
        /// <returns>sql (string)</returns>
        public static string DeleteOneEmployeeByEmployeeIdSQL(Guid employeeId)
        {
            string sql = $"CALL Proc_Delete_DeleteEmployeeById('{employeeId}')";
            return sql;
        }
    }
}
