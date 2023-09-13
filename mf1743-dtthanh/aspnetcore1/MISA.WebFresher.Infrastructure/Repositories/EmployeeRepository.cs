using Dapper;
using MISA.WebFresher.Application;
using MISA.WebFresher.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.Infrastructure
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<int> CreateEmployeeAsync(Employee employee)
        {
            var procedure = $"Proc_Employee_Create";
            var param = new DynamicParameters();
            param.Add("p_EmployeeId", employee.EmployeeId);
            param.Add("p_EmployeeCode", employee.EmployeeCode);
            param.Add("p_FullName", employee.FullName);
            param.Add("p_DepartmentId", employee.DepartmentID);
            param.Add("p_Position", employee.Position);
            param.Add("p_DateOfBirth", employee.DateOfBirth);
            param.Add("P_Gender", employee.Gender);
            param.Add("p_IdentityNumber", employee.IdentityNumber);
            param.Add("p_IdentityDate", employee.IdentityDate);
            param.Add("p_IdentityPlace", employee.IdentityPlace);
            param.Add("p_Address", employee.Address);
            param.Add("p_PhoneNumber", employee.PhoneNumber);
            param.Add("p_Email", employee.Email);
            param.Add("p_CreatedBy", employee.CreatedBy);
            param.Add("p_CreatedDate", DateTime.Now);

            var result = await connection.ExecuteAsync(procedure, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> UpdateEmployeeAsync(Employee employee, Guid id)
        {
            var procedure = $"Proc_Employee_Update";
            var param = new DynamicParameters();
            param.Add("p_EmployeeId", employee.EmployeeId);
            param.Add("p_EmployeeCode", employee.EmployeeCode);
            param.Add("p_FullName", employee.FullName);
            param.Add("p_DepartmentId", employee.DepartmentID);
            param.Add("p_Position", employee.Position);
            param.Add("p_DateOfBirth", employee.DateOfBirth);
            param.Add("P_Gender", employee.Gender);
            param.Add("p_IdentityNumber", employee.IdentityNumber);
            param.Add("p_IdentityDate", employee.IdentityDate);
            param.Add("p_IdentityPlace", employee.IdentityPlace);
            param.Add("p_Address", employee.Address);
            param.Add("p_PhoneNumber", employee.PhoneNumber);
            param.Add("p_Email", employee.Email);
            param.Add("p_CreatedBy", employee.CreatedBy);
            param.Add("p_CreatedDate", employee.CreatedDate);
            param.Add("p_ModifiedBy", employee.ModifiedBy);
            param.Add("p_ModifiedDate", DateTime.Now);


            var result = await connection.ExecuteAsync(procedure, param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
