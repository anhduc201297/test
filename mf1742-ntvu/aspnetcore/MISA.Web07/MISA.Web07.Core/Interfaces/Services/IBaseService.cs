using MISA.Web07.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.Core.Interfaces.Services
{
    public interface IBaseService<T, TInsertModel> where T : BaseEntity where TInsertModel : T
    {
        Task<int> InsertAsync(TInsertModel entity);
        Task<int> UpdateAsync(TInsertModel entity, Guid entityId);

        Task<MemoryStream> ExportToExcelAsync();
    }
}
