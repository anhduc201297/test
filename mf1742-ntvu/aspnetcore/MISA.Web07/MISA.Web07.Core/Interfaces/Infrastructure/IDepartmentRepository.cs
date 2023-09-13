using MISA.Web07.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.Core.Interfaces.Infrastructure
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        
        Task<Department> GetByNameAsync(string name);
    }
}
