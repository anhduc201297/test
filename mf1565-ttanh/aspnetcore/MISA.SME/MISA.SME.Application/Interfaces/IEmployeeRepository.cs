using MISA.SME.Application.Wrappers;
using MISA.SME.Domain.Entities;

namespace MISA.SME.Application.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<Response> GetPaginationAsync(int limit, int offset);
        Task<Response> GetFilteringAndPaginationAsync(string? keyword, int limit, int offset);
        Task<Response> GetByCodeAsync(string code);
    }
}
