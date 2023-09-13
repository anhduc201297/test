using MISA.Web07.Core.Entities;
using MISA.Web07.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.Core.Interfaces.Infrastructure
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<string> GetNewCodeAsync();
    
    }
}
