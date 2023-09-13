using Microsoft.AspNetCore.Mvc;
using MISA.SME.Application;
using MISA.SME.Application.Interfaces;
using MISA.SME.Domain.Common.Enums;
using MISA.SME.Domain.Entities;
using MISA.SME.Domain.Exceptions;

namespace MISA.SME.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentsController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string keyword)
        {
            try
            {
                var data = await _unitOfWork.Departments.GetAllAsync();

                // Xử lý kết quả trả về
                // Thành công
                if (data != null)
                    return Ok(data);
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
            // Try catch để bắt exception
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _unitOfWork.Departments.GetByIdAsync(id);

            // Xử lý kết quả trả về
            // Thành công
            if (data != null)
                return Ok(data);
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

        [HttpPost]
        public async Task<IActionResult> Add(Department department)
        {
            var data = await _unitOfWork.Departments.AddAsync(department);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await _unitOfWork.Departments.DeleteAsync(id);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Department department)
        {
            var data = await _unitOfWork.Departments.UpdateAsync(department);
            return Ok(data);
        }

    }
}
