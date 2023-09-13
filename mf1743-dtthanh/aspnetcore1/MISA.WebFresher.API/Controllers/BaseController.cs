using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher.Application;

namespace MISA.WebFresher.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<TEntity> : ControllerBase
    {
        private readonly IBaseRepository<TEntity> baseRepository;

        public BaseController(IBaseRepository<TEntity> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        /// <summary>
        /// Lấy toàn bộ danh sách nhân viên
        /// </summary>
        /// <returns>List nhân viên</returns>
        /// AUTHOR: DTTHANH(11/09/2023)
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var entities = await baseRepository.GetAllAsync();
            return Ok(entities);
        }

        /// <summary>
        /// Lấy một nhân viên theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>1 nhân viên</returns>
        /// AUTHOR: DTTHANH(11/09/2023)
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOneAsync(Guid id)
        {
            try
            {
                var entity = await baseRepository.GetOneAsync(id);
                if (entity == null)
                {
                    return NotFound();
                }
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOneAsync(Guid id)
        {
            try
            {
                var res = await baseRepository.GetOneAsync(id);
                if (res == null)
                {
                    return NotFound();
                }
                var entity = await baseRepository.DeleteOneAsync(id);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
