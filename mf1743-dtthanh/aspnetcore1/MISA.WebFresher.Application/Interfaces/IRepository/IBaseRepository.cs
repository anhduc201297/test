using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.Application
{
    /// <summary>
    /// Interface cơ sở trong lớp application
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Lấy tất cả đối tượng
        /// </summary>
        /// <returns>List</returns>
        /// AUTHOR: DTTHANH (11/09/2023)
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Lấy 1 đối tượng với id
        /// </summary>
        /// <param Name="id"></param>
        /// <returns>1 nhân viên</returns>
        /// AUTHOR: DTTHANH (11/09/2023)
        Task<TEntity> GetOneAsync(Guid id);

        /// <summary>
        /// Xóa 1 đối tượng với id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>số thay đổi</returns>
        /// AUTHOR: DTTHANH (11/09/2023)
        Task<int> DeleteOneAsync(Guid id);

        /// <summary>
        /// Thêm mới đối tượng
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>số thay đổi</returns>
        /// AUTHOR: DTTHANH (11/09/2023)
        //Task<int> CreateAsync (TEntity entity);

        /// <summary>
        /// Sửa thông tin đối tượng
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>số thay đổi</returns>
        /// AUTHOR: DTTHANH (11/09/2023)
        //Task<int> UpdateAsync (TEntity entity);
    }
}
