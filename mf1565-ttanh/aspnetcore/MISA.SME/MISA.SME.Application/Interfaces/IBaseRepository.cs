using MISA.SME.Application.Wrappers;

namespace MISA.SME.Application.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<Response> GetByIdAsync(Guid id);
        Task<Response> GetAllAsync();
        Task<Response> AddAsync(T entity);
        Task<Response> UpdateAsync(T entity);
        Task<Response> DeleteAsync(Guid id);
    }
}
