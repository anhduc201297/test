
using Microsoft.AspNetCore.Mvc;
using MISA.Web07.Core.Dtos;
using MISA.Web07.Core.Entities;
using MISA.Web07.Core.Exceptions;
using MISA.Web07.Core.Interfaces.Infrastructure;
using MISA.Web07.Core.Interfaces.Services;
using MISA.Web07.Core.Resources;
using MISA.Web07.Core.ViewModels;
using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace MISA.Web07.Api.Controllers
{

    public class EmployeesController : MISABaseController<Employee, EmployeeInsertDto, EmployeeViewModel>
    {
        #region Fields
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeRepository _employeeRepository;
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeRepository employeeRepository, IEmployeeService employeeService) : base(employeeRepository, employeeService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Lấy code mới 
        /// </summary>
        /// <returns>
        /// 200 - OK
        /// 500 - Lỗi nghiệp vụ
        /// </returns>
        /// Created: NTVu - 05/09/2023
        [HttpGet("GetNewCode")]
        public async Task<IActionResult> GetNewCodeAsync()
        {
            try
            {
                return Ok(await _employeeRepository.GetNewCodeAsync());
            }
     
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorService()
                {
                    UserMsg = ResourceVN.Error_Exception,
                    DevMsg = ex.Message,
                    Data = ex.Data,
                    TraceId = Guid.NewGuid().ToString(),
                    MoreInfo = "GetNewCodeAsync in Employees have problem.",
                    ErrorCode = 23
                });

            }
        }
        #endregion
    }

}
