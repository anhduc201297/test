
using MISA.Web07.Core.Entities;
using MISA.Web07.Core.Exceptions;
using MISA.Web07.Core.Interfaces.Infrastructure;
using MISA.Web07.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Web07.Core.Services
{
    public class DepartmentService : BaseService<Department, Department, Department>, IDepartmentService
    {
        #region Fields

        private IDepartmentRepository _departmentRepository;
        #endregion

        #region Constructor

        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
            _departmentRepository = departmentRepository;
        } 
        #endregion
    }
}
