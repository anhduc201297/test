
using MISA.Web07.Core.Dtos;
using MISA.Web07.Core.Entities;
using MISA.Web07.Core.Exceptions;
using MISA.Web07.Core.Interfaces.Infrastructure;
using MISA.Web07.Core.Interfaces.Services;
using MISA.Web07.Core.MAttributes;
using MISA.Web07.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Web07.Core.Services
{
    public class EmployeeService : BaseService<Employee, EmployeeInsertDto, EmployeeViewModel>, IEmployeeService
    {
        #region Fields
        public IEmployeeRepository _employeeRepository;
        public IDepartmentRepository _departmentRepository;

        #endregion
        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        } 
        #endregion

        #region Methods

        /// <summary>
        /// Kiểm tra trùng mã, tên đơn vị tồn tại
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns>
        /// true - Validate thành công
        /// </returns>
        /// <exception cref="MISAValidateException"></exception>
        /// Created: NTVU (05/09/2023)
        protected override async Task<bool> SelfValidateAsync(EmployeeInsertDto entity, Guid entityId)
        {
           

            // Kiểm tra trùng mã
            var errorList = new Dictionary<string, string>();
            var employee = await _employeeRepository.GetByCodeAsync(entity.EmployeeCode);
            if(employee != null && employee.EmployeeId != entityId)
            {
              
                errorList.Add("EmployeeCode", Resources.ResourceVN.Error_DuplicateEmployeeCode);

                throw new MISAValidateException(Resources.ResourceVN.Error_DuplicateEmployeeCode)
                {
                   
                    
                    Data = errorList
                };
            }

            // Kiểm tra tên đơn vị phòng ban
            var department = await _departmentRepository.GetByNameAsync(entity.DepartmentName);
            if(department == null)
            {
               
                errorList.Add("Department", Resources.ResourceVN.Error_DepartmentNotExists);
                throw new MISAValidateException(Resources.ResourceVN.Error_DepartmentNotExists)
                {
                    
                   
                    Data = errorList
                };
            }
            else
            {
                entity.DepartmentId = department.DepartmentId;
            }
       

            return true;
        }
        #endregion
    }
}
