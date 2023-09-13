using Dapper;
using MISA.Web07.Core.Entities;
using MISA.Web07.Core.Exceptions;
using MISA.Web07.Core.Interfaces.Infrastructure;
using MISA.Web07.Core.Resources;
using MISA.Web07.Core.ViewModels;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Methods

        /// <summary>
        /// Lấy code nhân viên mới
        /// </summary>
        /// <returns>
        /// Code nhân viên mới 
        /// </returns>
        /// Created: NTVU - 05/09/2023
        public async Task<string> GetNewCodeAsync()
        {
            //DEMO
            try
            {
                var sqlCommand = "Proc_Employee_GetNewCode";

                using (_mySqlConnection = new(_connectionString))
                {

                    var res = await _mySqlConnection.QueryFirstAsync<string>(sql: sqlCommand, commandType: CommandType.StoredProcedure);

                    if (string.IsNullOrEmpty(res))
                    {
                        throw new Exception(ResourceVN.Error_GetNewCode);
                    }
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new MISADatabaseException(ex.Message);
            }
        }
        #endregion
    }
}
