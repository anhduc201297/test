using Dapper;
using MISA.WebFresher.Application;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.Infrastructure
{

    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        protected readonly DbConnection connection;

        public BaseRepository(DbConnection connection)
        {
            this.connection = connection;
        }

        public string Procedure { get; set; } = typeof(TEntity).Name;

        /// <summary>
        /// Lấy tất cả các bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// AUTHOR: DTTHANH(11/09/2023)
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var procedure = $"Proc_{Procedure}_GetAll";
            var result = await connection.QueryAsync<TEntity>(procedure, commandType: CommandType.StoredProcedure);
            return result;
        }

        /// <summary>
        /// Lấy ra một nhân viên theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>1 đối tượng</returns>
        /// AUTHOR: DTTHANH(11/09/2023)
        public async Task<TEntity> GetOneAsync(Guid id)
        {
            var procedure = $"Proc_{Procedure}_GetOne";

            var param = new DynamicParameters();
            param.Add($"p_{Procedure}Id", id);

            var result = await connection.QueryFirstOrDefaultAsync<TEntity>(procedure, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<int> DeleteOneAsync(Guid id)
        {
            var procedure = $"Proc_{Procedure}_DeleteOne";
            var param = new DynamicParameters();
            param.Add($"p_{Procedure}Id", id);

            var result = await connection.ExecuteAsync(procedure, param, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
