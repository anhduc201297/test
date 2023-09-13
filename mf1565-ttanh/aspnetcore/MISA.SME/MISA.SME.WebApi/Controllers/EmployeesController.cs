using Microsoft.AspNetCore.Mvc;
using MISA.SME.Application;
using MISA.SME.Application.Interfaces;
using MISA.SME.Application.Wrappers;
using MISA.SME.Domain.Common.Enums;
using MISA.SME.Domain.Entities;
using MISA.SME.Domain.Exceptions;

namespace MISA.SME.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// api lấy tất cả nhân viên theo từ khóa và phân trang
        /// </summary>
        /// <param name="keyword">từ khóa chứa mã nhân viên hoặc tên nhân viên</param>
        /// <param name="limit">số bản ghi trả về</param>
        /// <param name="offset">hàng bắt đầu truy xuất</param>
        /// <returns></returns>
        /// created by: TTANH - 01/09/2023
        /// modified by: TTANH - 02/09/2023
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string keyword, [FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            try
            {
                var data = new Response();

                if (limit < 0 && offset < 0)
                {
                    data = await _unitOfWork.Employees.GetAllAsync();
                }
                else
                {
                    if (keyword == null)
                    {
                        data = await _unitOfWork.Employees.GetPaginationAsync(limit, offset);
                    }
                    else
                    {
                        data = await _unitOfWork.Employees.GetFilteringAndPaginationAsync(keyword, limit, offset);
                    }
                }

                if (data != null)
                    return Ok(data);
                else
                    return NotFound(new ErrorResponse
                    {
                        ErrorCode = ErrorCode.NotFound,
                        DevMsg = Resource.DevMsg_NotFound,
                        UserMsg = Resource.UserMsg_NotFound,
                        TraceId = HttpContext.TraceIdentifier,
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new ErrorResponse
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    TraceId = HttpContext.TraceIdentifier,
                });
            };
        }

        /// <summary>
        /// api lấy nhân viên theo id
        /// </summary>
        /// <param name="id">ID nhân viên</param>
        /// <returns></returns>
        /// created by: TTANH - 01/09/2023
        /// modified by: TTANH - 02/09/2023
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                var data = await _unitOfWork.Employees.GetByIdAsync(id);

                if (data != null)
                    return Ok(data);
                else
                    return NotFound(new ErrorResponse
                    {
                        ErrorCode = ErrorCode.NotFound,
                        DevMsg = Resource.DevMsg_NotFound,
                        UserMsg = Resource.UserMsg_NotFound,
                        TraceId = HttpContext.TraceIdentifier,
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new ErrorResponse
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    TraceId = HttpContext.TraceIdentifier,
                });
            }
        }

        /// <summary>
        /// api thêm nhân viên
        /// </summary>
        /// <param name="employee">dữ liệu nhân viên</param>
        /// <returns></returns>
        /// created by: TTANH - 09/09/2023
        /// modified by: TTANH - 11/09/2023
        [HttpPost]
        public async Task<IActionResult> Add(Employee employee)
        {
            try
            {
                var data = await _unitOfWork.Employees.AddAsync(employee);

                if (data != null)
                    return StatusCode(StatusCodes.Status201Created, data);
                else
                    return NotFound(new ErrorResponse
                    {
                        ErrorCode = ErrorCode.NotFound,
                        DevMsg = Resource.DevMsg_NotFound,
                        UserMsg = Resource.UserMsg_NotFound,
                        TraceId = HttpContext.TraceIdentifier,
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new ErrorResponse
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    TraceId = HttpContext.TraceIdentifier,
                });
            }
        }

        /// <summary>
        /// api cập nhật dữ liệu nhân viên
        /// </summary>
        /// <param name="employee">dữ liệu nhân viên đã thay đổi</param>
        /// <returns></returns>
        /// created by: TTANH - 11/09/2023
        [HttpPut]
        public async Task<IActionResult> Update(Employee employee)
        {
            try
            {
                var data = await _unitOfWork.Employees.UpdateAsync(employee);

                if (data != null)
                    return StatusCode(StatusCodes.Status204NoContent, data);
                else
                    return NotFound(new ErrorResponse
                    {
                        ErrorCode = ErrorCode.NotFound,
                        DevMsg = Resource.DevMsg_NotFound,
                        UserMsg = Resource.UserMsg_NotFound,
                        TraceId = HttpContext.TraceIdentifier,
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new ErrorResponse
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    TraceId = HttpContext.TraceIdentifier,
                });
            }
        }

        /// <summary>
        /// api xóa nhân viên theo ID
        /// </summary>
        /// <param name="id">ID nhân viên bị xóa</param>
        /// <returns></returns>
        /// created by: TTANH - 08/09/2023
        /// modified by: TTANH - 11/09/2023
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var data = await _unitOfWork.Employees.DeleteAsync(id);

                // Xử lý kết quả trả về
                // Thành công
                if (data != null)
                    return StatusCode(StatusCodes.Status204NoContent, data);
                // Thất bại
                else
                    return NotFound(new ErrorResponse
                    {
                        ErrorCode = ErrorCode.NotFound,
                        DevMsg = Resource.DevMsg_NotFound,
                        UserMsg = Resource.UserMsg_NotFound,
                        TraceId = HttpContext.TraceIdentifier,
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new ErrorResponse
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    TraceId = HttpContext.TraceIdentifier,
                });
            }
        }
    }
}