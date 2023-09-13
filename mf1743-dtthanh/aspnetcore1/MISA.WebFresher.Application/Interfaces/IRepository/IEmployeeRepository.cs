using MISA.WebFresher.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.Application
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<int> CreateEmployeeAsync(Employee employee);

        Task<int> UpdateEmployeeAsync(Employee employee, Guid Id);

    }
}
