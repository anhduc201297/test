using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace MISA.Web07.MF1732_DKTUAN
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly MySqlConnection _connection;

        public EmployeeServices(MySqlConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.CreateConnection();
            
        }

        /// <summary>
        /// lấy tất cả nhân viên
        /// Created by: mf1732-dktuan (11/9/2023)
        /// </summary>
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            _connection.Open();
            string sql = "CALL Proc_Employee_GetAll";
            var resutl = await _connection.QueryAsync<Employee>(sql);
            _connection.Close();
            return resutl;
        }

        /// <summary>
        /// Lấy 1 nhân viên theo id
        /// Created by: mf1732-dktuan (11/9/2023)
        /// </summary>
        public async Task<Employee> GetEmployeeByIdAsync(Guid employeeId)
        {
            _connection.Open();
            string sql = $"select * from Employee where Employee.EmployeeId=@employeeId";
            var param = new DynamicParameters();
            param.Add("employeeId", employeeId);
            var result = await _connection.QuerySingleAsync<Employee>(sql, param);
            _connection.Close();
            return result;
        }

        /// <summary>
        /// Thêm mới 1 nhân viên
        /// Created by: mf1732-dktuan (11/9/2023)
        /// </summary>
        public async Task<dynamic> AddEmployeeAsync(Employee employee)
        {
            _connection.Open();
            string sql = $"insert into employee (EmployeeId, EmployeeCode, FullName, Gender, DepartmentId, CreatedBy, ModifiedBy) VALUES (@EmployeeId, @EmployeeCode, @FullName, @Gender, @DepartmentId, '', '');";
            var result = await _connection.ExecuteAsync(sql, employee);
            _connection.Close();
            return result;
        }

        /// <summary>
        /// Sửa thông tin nhân viên
        /// Created by: mf1732-dktuan (11/9/2023)
        /// </summary>
        public async Task<dynamic> UpdateEmployeeAsync(Guid employeeId, [FromBody] Employee employee)
        {
            _connection.Open();
            //var check = GetEmployeeByIdAsync(employeeId);
            //if (check == null) return null;
            if (employeeId != employee.EmployeeId)
            {
                _connection.Close();
                return null;
            }
            string sql = $"UPDATE employee e " +
                $"SET" +
                $"    FullName = @FullName," +
                $"    Gender = @Gender," +
                //$"    DateOfBirth = @DateOfBirth," +
                $"    DepartmentId = @DepartmentId," +
                $"    CreatedBy = ''," +
                $"    ModifiedDate = NOW()," +
                $"    ModifiedBy = ''" +
                $"WHERE EmployeeId = @EmployeeId;";
            var result = await _connection.ExecuteAsync(sql, employee);
            _connection.Close();
            if (result == 0) return null;
            return result;
        }

        /// <summary>
        /// Xóa 1 nhân viên theo id
        /// Created by: mf1732-dktuan (11/9/2023)
        /// </summary>
        public async Task<dynamic> DeleteEmployeeAsync(Guid employeeId)
        {
            _connection.Open();
            //var check = GetEmployeeByIdAsync(employeeId);
            //if (check == null) return null;
            string sql = $"delete from employee where employee.EmployeeId=@employeeId";
            var param = new DynamicParameters();
            param.Add("employeeId", employeeId);
            var result = await _connection.ExecuteAsync(sql, param);
            _connection.Close();
            if (result == 0) return null;
            return result;
        }


    }
}
