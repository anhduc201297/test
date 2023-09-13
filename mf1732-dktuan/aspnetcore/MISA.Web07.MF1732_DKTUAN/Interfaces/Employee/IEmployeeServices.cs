using Microsoft.AspNetCore.Mvc;

namespace MISA.Web07.MF1732_DKTUAN
{
    public interface IEmployeeServices
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task<dynamic> AddEmployeeAsync(Employee employee);
        Task<dynamic> UpdateEmployeeAsync(Guid employeeId, [FromBody] Employee employee);
        Task<dynamic> DeleteEmployeeAsync(Guid id);
    }
}
