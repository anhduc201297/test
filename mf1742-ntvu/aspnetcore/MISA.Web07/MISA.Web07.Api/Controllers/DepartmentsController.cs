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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Web07.Api.Controllers
{

    public class DepartmentsController : MISABaseController<Department, Department, Department>
    {
        #region Properties
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDepartmentService _departmentService;
        #endregion

        #region Constructor
        public DepartmentsController(IDepartmentRepository departmentRepository, IDepartmentService departmentService) : base(departmentRepository, departmentService)
        {
            _departmentRepository = departmentRepository;
            _departmentService = departmentService;
        }
        #endregion

        #region Apis

        /// <summary>
        /// Lấy phòng ban theo tên
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        /// 200 - Thông tin về phòng ban 
        /// 400 - Không truyền tên phòng ban
        /// 500 - Lỗi 
        /// </returns>
        [HttpGet("Name/{name}")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest(new ErrorService()
                {
                    UserMsg = ResourceVN.Error_ValidateData,
                    DevMsg = "Name is empty.",
                    TraceId = Guid.NewGuid().ToString(),
                    MoreInfo = @"The 'name' field is required."
                });
            }
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    var error = new ErrorService()
                    {
                        UserMsg = ResourceVN.Error_ValidateData,
                        DevMsg = ResourceVN.Error_ValidateData,

                    };
                    return BadRequest(error);
                }
                return Ok(await _departmentRepository.GetByNameAsync(name));
            }
      
            catch (Exception ex)
            {
                var error = new ErrorService()
                {
                    UserMsg = ResourceVN.Error_Exception,
                    DevMsg = ex.Message,
                    Data = ex.Data,
                    TraceId = "Not set yet.",
                    MoreInfo = "GetByNameAsync in Employees have problem.",
                    ErrorCode = 24
                };
                return StatusCode(500, error);
            }
        }

        #endregion

    }
}
