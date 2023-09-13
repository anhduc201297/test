using MISA.Web07.Core.Entities;
using MISA.Web07.Core.Models;
using MISA.Web07.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.Core.Interfaces.Infrastructure
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Lấy toàn bộ 
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NTVU (01/09/2023)
        Task<IEnumerable<TViewModel>> GetAllAsync<TViewModel>();
        /// <summary>
        /// Lấy theo paging và searchValue
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        /// CreatedBy: NTVU (01/09/2023)
        Task<FilterDataResponse<TViewModel>> FilterAsync<TViewModel>(int pageSize, int page, string search);

        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NTVU (01/09/2023)
        Task<T> GetByIdAsync(Guid entityId);
        /// <summary>
       
        /// <param name="entityCode"></param>
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NTVU (01/09/2023)
        Task<T> GetByCodeAsync(string entityCode);
        /// <summary>
        /// Thêm 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy: NTVU (01/09/2023)
        Task<int> InsertAsync(T entity);
        /// <summary>
        /// Sửa
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy: NTVU (01/09/2023)
        Task<int> UpdateAsync(T entity,Guid entityId);
        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// CreatedBy: NTVU (01/09/2023)
        Task<int> DeleteOneAsync(Guid entityId);
        /// <summary>

        /// </summary>
        /// <param name="entityCode"></param>
        /// <returns></returns>
        Task<int> DeleteOneByCodeAsync(string entityCode);
        /// <summary>
       
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns>
        /// CreatedBy: NTVU (01/09/2023)

        Task<int> DeleteMultiAsync(List<Guid> entityIds);
        /// <summary>
     
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NTVU (01/09/2023)
        Task<int> DeleteAllAsync();
    }
}
