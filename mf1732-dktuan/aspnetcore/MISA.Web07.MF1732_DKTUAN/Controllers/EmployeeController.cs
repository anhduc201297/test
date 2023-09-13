using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Diagnostics;

namespace MISA.Web07.MF1732_DKTUAN
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employeeServices;
        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }

        /// <summary>
        /// lấy tất cả nhân viên
        /// Created by: mf1732-dktuan (11/9/2023)
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Employee>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            try
            {
                var results = await _employeeServices.GetAllEmployeesAsync();
                return Ok(results);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CustomErrorMessage
                {
                    TraceId = HttpContext.TraceIdentifier,
                });
            }
        }

        /// <summary>
        /// Lấy 1 nhân viên theo id
        /// Created by: mf1732-dktuan (11/9/2023)
        /// </summary>
        [HttpGet]
        [Route("{employeeId}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployeeByIdAsync(Guid employeeId)
        {
            try
            {
                var result = await _employeeServices.GetEmployeeByIdAsync(employeeId);
                if (result == null) return NotFound(new CustomErrorMessage
                {
                    UserMsg = $"Nhân viên có id {employeeId.ToString()} không tồn tại!",
                    TraceId = HttpContext.TraceIdentifier,
                });
                return Ok(result);

            } catch(Exception ex)
            {
                return BadRequest(new CustomErrorMessage
                {
                    TraceId = HttpContext.TraceIdentifier,
                });

            }

        }

        /// <summary>
        /// Thêm mới 1 nhân viên
        /// Created by: mf1732-dktuan (11/9/2023)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] Employee employee)
        {
            try
            {
                var result = await _employeeServices.AddEmployeeAsync(employee);
                return StatusCode(StatusCodes.Status201Created);

            } catch (Exception ex)
            {
                return BadRequest(new CustomErrorMessage{ TraceId = HttpContext.TraceIdentifier });
            }
        }

        /// <summary>
        /// Sửa thông tin nhân viên
        /// Created by: mf1732-dktuan (11/9/2023)
        /// </summary>
        [HttpPut]
        [Route("{employeeId}")]
        public async Task<IActionResult> UpdateEmployeeAsync(Guid employeeId, [FromBody] Employee employee)
        {
            try
            {
                var result = await _employeeServices.UpdateEmployeeAsync(employeeId, employee);
                if (result == null) return NotFound(new CustomErrorMessage
                {
                    UserMsg = $"Nhân viên có id {employeeId.ToString()} không tồn tại!",
                });
                return Ok();
            } catch(Exception ex)
            {
                return BadRequest(new CustomErrorMessage{ TraceId = HttpContext.TraceIdentifier });
            }
        }

        /// <summary>
        /// Xóa 1 nhân viên theo id
        /// Created by: mf1732-dktuan (11/9/2023)
        /// </summary>
        [HttpDelete]
        [Route("employeeId")]
        public async Task<IActionResult> DeleteEmployeeAsync(Guid employeeId)
        {
            try
            {
                var result = await _employeeServices.DeleteEmployeeAsync(employeeId);
                if(result == null) return NotFound(new CustomErrorMessage
                {
                    UserMsg = $"Nhân viên có id {employeeId.ToString()} không tồn tại!",
                });
                return Ok();
            } catch(Exception ex)
            {
                return BadRequest(new CustomErrorMessage { 
                    TraceId = HttpContext.TraceIdentifier 
                });
            }
        }
    }
}
