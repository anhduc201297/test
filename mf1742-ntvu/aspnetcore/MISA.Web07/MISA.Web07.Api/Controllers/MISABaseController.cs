using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web07.Core.Resources;
using MISA.Web07.Core.Dtos;
using MISA.Web07.Core.Entities;
using MISA.Web07.Core.Exceptions;
using MISA.Web07.Core.Interfaces.Infrastructure;
using MISA.Web07.Core.Interfaces.Services;
using MISA.Web07.Core.ViewModels;
using System.Diagnostics;

namespace MISA.Web07.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MISABaseController<T, TInsertModel, TViewModel> : ControllerBase where T : BaseEntity where TInsertModel : T
    {
        #region Fields
        private readonly IBaseRepository<T> _baseRepository;
        private readonly IBaseService<T, TInsertModel> _baseService;
        #endregion

        #region Constructor
        public MISABaseController(IBaseRepository<T> baseRepository, IBaseService<T, TInsertModel> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>
        /// 200 - Lấy dự liệu thành công
        /// 500 - Lỗi xảy ra
        /// </returns>
        /// Created: NTVU - 05/09/2023
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var res = await _baseRepository.GetAllAsync<TViewModel>();

                return Ok(res);
            }
    
    
            catch (Exception ex)
            {
                var error = new ErrorService()
                {
                    UserMsg = ResourceVN.Error_Exception,
                    DevMsg = ex.Message,
                    Data = ex.Data,
                    TraceId = Guid.NewGuid().ToString(),
                    MoreInfo = "GetAllAsync in MISABaseController have problem.",
                    ErrorCode = 22
                };
                return StatusCode(500, error);
            }

        }


        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>
        /// 200 - Lấy dự liệu thành công
        /// 500 - Lỗi xảy ra
        /// </returns>
        /// Created: NTVU - 05/09/2023
        [HttpGet("{entityId}")]
        public async Task<IActionResult> GetByIdAsync(Guid entityId)
        {
            try
            {
                var res = await _baseRepository.GetByIdAsync(entityId);
                if(res == null)
                {
                    return StatusCode(404, ResourceVN.Error_NotFound);
                }

                return Ok(res);
            }
      
            catch (Exception ex)
            {
                var error = new ErrorService()
                {
                    UserMsg = ResourceVN.Error_Exception,
                    DevMsg = ex.Message,
                    Data = ex.Data,
                    TraceId = Guid.NewGuid().ToString(),
                    MoreInfo = "GetByIdAsync in base have problem.",
                    ErrorCode = 23
                };
                return StatusCode(500, error);
            }

        }

        /// <summary>
        /// Lấy dữ liệu theo paging, lọc, trả về totalRecords và data
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="search"></param>
        /// <returns>
        /// 200 - Lấy dự liệu thành công
        /// 500 - Lỗi xảy ra
        /// </returns>
        /// Created: NTVU - 05/09/2023
        [HttpGet("Filter")]
        public async Task<IActionResult> FilterAsync(string? search, int pageSize = 100, int page = 1)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    search = " ";
                }

                var data = await _baseRepository.FilterAsync<TViewModel>(search: search, page: page, pageSize: pageSize);

                return Ok(data);


            }
     
            catch (Exception ex)
            {
                var error = new ErrorService()
                {
                    UserMsg = ResourceVN.Error_Exception,
                    DevMsg = ex.Message,
                    Data = ex.Data,
                    TraceId = Guid.NewGuid().ToString(),
                    MoreInfo = "FilterAsync in base have problem.",
                    ErrorCode = 21
                };
                return StatusCode(500, error);
            }

        }
        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// 201 - Thêm mới thành công
        /// 400 - Lỗi dữ liệu từ client
        /// 500 - Lỗi nghiệp vụ
        /// </returns>
        /// Created: NTVu - 05/09/2023
        [HttpPost]
        public async Task<IActionResult> InsertAsync(TInsertModel entity)
        {
            try
            {
                var res = await _baseService.InsertAsync(entity);
                if (res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode(500, new ErrorService()
                    {
                        UserMsg = ResourceVN.Error_Exception,
                        DevMsg = ResourceVN.Error_Exception,
                        Data = entity,
                        TraceId = Guid.NewGuid().ToString(),
                        MoreInfo = "InsertAsync in base have problem.",
                        ErrorCode = 20
                    });
                }
            }
            catch (MISAValidateException ex)
            {
                return StatusCode(400, new ErrorService()
                {
                    UserMsg = ex.Message,
                    DevMsg = ex.Message,
                    Data = ex.Data
                });
            }
    
            catch (Exception ex)
            {

                return StatusCode(500, new ErrorService()
                {
                    UserMsg = ResourceVN.Error_Exception,
                    DevMsg = ex.Message,
                    Data = ex.Data,
                    TraceId = Guid.NewGuid().ToString(),
                    MoreInfo = "InsertAsync in BaseController have problem.",
                    ErrorCode = 30,
                });
            }
        }

        /// <summary>
        /// Update bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns>
        /// 200 - Cập nhật thành công
        /// 409 - Lỗi, dữ liệu đã được cập nhật trước đó
        /// 400 - Lỗi dữ liệu truyền từ client
        /// 500 - Lỗi nghiệp vụ server
        /// </returns>
        /// Created: NTVU - 05/09/2023
        [HttpPut("{entityId}")]
        public async Task<IActionResult> UpdateAsync(TInsertModel entity, Guid entityId)
        {
            try
            {
                var res = await _baseService.UpdateAsync(entity, entityId);

                if (res > 0)
                {
                    return Ok(res);
                }
                else
                {
                    return StatusCode(500, new ErrorService()
                    {
                        UserMsg = ResourceVN.Error_Exception,
                        DevMsg = ResourceVN.Error_Exception,
                        Data = entity,
                        TraceId = Guid.NewGuid().ToString(),
                        MoreInfo = "UpdateAsync in base have problem.",
                        ErrorCode = 19
                    });
                }

            }
            catch (MISAUpdateException ex)
            {
                return StatusCode(409, new ErrorService()
                {
                    UserMsg = ex.Message,
                    DevMsg = ex.Message,
                    Data = ex.Data,
                    TraceId = Guid.NewGuid().ToString(),
                    MoreInfo = "UpdateAsync in base have problem.",
                    ErrorCode = 34
                });
            }
            catch (MISANotFoundException ex)
            {
                return StatusCode(404, new ErrorService()
                {
                    UserMsg = ex.Message,
                    DevMsg = ex.Message,
                    Data = ex.Data,
                    TraceId = Guid.NewGuid().ToString(),
                    MoreInfo = "UpdateAsync in base have problem.",
                    ErrorCode = 37
                });
            }
            catch (MISAValidateException ex)
            {
                return BadRequest(new ErrorService()
                {
                    UserMsg = ex.Message,
                    DevMsg = ex.Message,
                    Data = ex.Data,
                    TraceId = Guid.NewGuid().ToString(),
                    MoreInfo = "UpdateAsync in base have problem.",
                    ErrorCode = 30
                });
            }
     
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorService()
                {
                    UserMsg = ResourceVN.Error_Exception,
                    DevMsg = ex.Message,
                    Data = ex.Data,
                    TraceId = Guid.NewGuid().ToString(),
                    MoreInfo = "UpdateAsync in base have problem.",
                    ErrorCode = 31
                });
            }

        }

        /// <summary>
        /// Xóa bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>
        /// 204 - Xóa thành công
        /// 400 - Lỗi dữ liệu từ client, bản ghi có thể đã bị xóa trước đó
        /// 500 - Lỗi nghiệp vụ
        /// </returns>
        /// Created: NTVu - 05/09/2023
        [HttpDelete("{entityId}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid entityId)
        {
            try
            {
                var res = await _baseRepository.DeleteOneAsync(entityId);
                if (res > 0)
                {
                    return StatusCode(204, res);
                }
                else
                {
                    return BadRequest(new ErrorService()
                    {
                        UserMsg = ResourceVN.Error_ValidateData,
                        DevMsg = ResourceVN.Error_Exception,
                        Data = entityId,

                    });
                }
            }
      
            catch(MISANotFoundException ex)
            {
                return StatusCode(404, new ErrorService()
                {
                    UserMsg = ResourceVN.Error_Exception,
                    DevMsg = ex.Message,
                    Data = ex.Data,
                    TraceId = Guid.NewGuid().ToString(),
                    MoreInfo = "DeleteByIdAsync in BaseController have problem.",
                    ErrorCode = 39
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorService()
                {
                    UserMsg = ResourceVN.Error_Exception,
                    DevMsg = ex.Message,
                    Data = ex.Data,
                    TraceId = Guid.NewGuid().ToString(),
                    MoreInfo = "DeleteByIdAsync in base have problem.",
                    ErrorCode = 18
                });
            }
        }

        /// <summary>
        /// Xóa bản ghi theo mã
        /// </summary>
        /// <param name="entityCode"></param>
        /// <returns>
        /// 204 - Xóa thành công
        /// 400 - Lỗi dữ liệu từ client, bản ghi có thể đã bị xóa trước đó
        /// 500 - Lỗi nghiệp vụ
        /// </returns>
        /// Created: NTVu - 05/09/2023
        [HttpDelete("Code/{entityCode}")]
        public async Task<IActionResult> DeleteOneByCodeAsync(string entityCode)
        {
            try
            {
                var res = await _baseRepository.DeleteOneByCodeAsync(entityCode);
                if (res > 0)
                {
                    return StatusCode(204, res);
                }
                else
                {
                    return BadRequest(new ErrorService()
                    {
                        UserMsg = ResourceVN.Error_ValidateData,
                        DevMsg = ResourceVN.Error_Exception,
                        Data = entityCode,
                        TraceId = Guid.NewGuid().ToString(),
                        MoreInfo = "DeleteOneByCodeAsync in base have problem.",
                        ErrorCode = 17
                    });
                }
            }
  
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorService()
                {
                    UserMsg = ResourceVN.Error_Exception,
                    DevMsg = ex.Message,
                    Data = ex.Data,
                    TraceId = Guid.NewGuid().ToString(),
                    MoreInfo = "UpdateAsync in base have problem.",
                    ErrorCode = 19
                });
            }
        }

        /// <summary>
        /// Xóa nhiều bản ghi theo id
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns>
        /// 204 - Xóa thành công
        /// 400 - Lỗi dữ liệu từ client, bản ghi có thể đã bị xóa trước đó
        /// 500 - Lỗi nghiệp vụ
        /// </returns>
        /// Created: NTVu - 05/09/2023
        [HttpDelete()]
        public async Task<IActionResult> DeleteMultiAsync(List<Guid> entityIds)
        {
            try
            {
                var res = await _baseRepository.DeleteMultiAsync(entityIds);
                if (res > 0)
                {
                    return StatusCode(204, res);
                }
                else
                {
                    return BadRequest(new ErrorService()
                    {
                        UserMsg = ResourceVN.Error_ValidateData,
                        DevMsg = ResourceVN.Error_Exception,
                        Data = entityIds,
                        TraceId = Guid.NewGuid().ToString(),
                        MoreInfo = "DeleteMultiAsync in base have problem.",
                        ErrorCode = 16
                    });
                }
            }
  
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorService()
                {
                    UserMsg = ResourceVN.Error_Exception,
                    DevMsg = ex.Message,
                    Data = ex.Data
                });
            }
        }

        /// <summary>
        /// Xóa toàn bộ bản ghi
        /// </summary>
        /// <returns>
        /// 204 - Xóa thành công
        /// 400 - Lỗi dữ liệu từ client, bản ghi có thể đã bị xóa trước đó
        /// 500 - Lỗi nghiệp vụ
        /// </returns>
        /// Created: NTVu - 05/09/2023
        [HttpDelete("All")]
        public async Task<IActionResult> DeleteAllAsync()
        {
            try
            {
                var res = await _baseRepository.DeleteAllAsync();
                if (res > 0)
                {
                    return StatusCode(204, res);
                }
                else
                {
                    return BadRequest(new ErrorService()
                    {
                        UserMsg = ResourceVN.Error_ValidateData,
                        DevMsg = ResourceVN.Error_Exception,
                        Data = res
                    });
                }
            }
    
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorService()
                {
                    UserMsg = ResourceVN.Error_Exception,
                    DevMsg = ex.Message,
                    Data = ex.Data,
                    TraceId = Guid.NewGuid().ToString(),
                    MoreInfo = "DeleteAllAsync in base have problem.",
                    ErrorCode = 15
                });
            }
        }

        /// <summary>
        /// Xuất ra file excel
        /// </summary>
        /// <returns>
        /// 200 - File Excel
        /// 500 - Lỗi nghiệp vụ
        /// </returns>
        /// Created: NTVU - 05/09/2023
        [HttpGet("Export")]
        public async Task<IActionResult> ExportToExcelAsync()
        {
            try
            {

                return Ok(await _baseService.ExportToExcelAsync());

            }
        
            catch (Exception ex)
            {
                var error = new ErrorService()
                {
                    DevMsg = ex.Message,
                    UserMsg = ResourceVN.Error_Exception,
                    Data = ex.Data,
                    TraceId = Guid.NewGuid().ToString(),
                    MoreInfo = "ExportToExcelAsync in base have problem.",
                    ErrorCode = 14
                };
                return StatusCode(500, error);
            }
        }
        #endregion

    }
}
