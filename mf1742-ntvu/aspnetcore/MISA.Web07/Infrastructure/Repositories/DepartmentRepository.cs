using Dapper;
using MISA.Web07.Core.Entities;
using MISA.Web07.Core.Exceptions;
using MISA.Web07.Core.Interfaces.Infrastructure;
using MISA.Web07.Core.Resources;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {

        #region Methods
        /// <summary>
        /// Lấy phòng ban theo tên
        /// </summary>
        /// <param name="departmentName"></param>
        /// <returns>
        /// Dữ liệu phòng ban hoặc null nếu không tồn tại
        /// </returns>
        public async Task<Department> GetByNameAsync(string departmentName)
        {
            try
            {
                using var sqlConnection = new MySqlConnection(_connectionString);
                var sqlCommand = "SELECT * FROM department WHERE UPPER(departmentName) = UPPER(TRIM(@departmentName))";
                var res = await sqlConnection.QueryFirstOrDefaultAsync<Department>(sql: sqlCommand, param: new { departmentName });
                return res;
            }
            catch (Exception ex)
            {


                throw new MISADatabaseException(ex.Message);
            }
         
        } 
        #endregion



    }
}
