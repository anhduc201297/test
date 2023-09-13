using Dapper;
using MISA.Web07.Core.Entities;
using MISA.Web07.Core.Exceptions;
using MISA.Web07.Core.Interfaces.Infrastructure;
using MISA.Web07.Core.Models;
using MISA.Web07.Core.Resources;
using MISA.Web07.Core.ViewModels;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        #region Fields

        protected string _connectionString = "User Id=root;Host=localhost;Character Set=utf8; Database=misa.mf1742";
        protected MySqlConnection? _mySqlConnection;

        private readonly string _className;
        #endregion

        #region Constructor
        public BaseRepository()
        {
            _className = typeof(T).Name;
        }
        #endregion

        #region Methods

        #region Đọc dữ liệu từ DB

        /// <summary>
        /// Lấy bản ghi theo mã
        /// </summary>
        /// <param name="entityCode"></param>
        /// <returns>
        /// Bản ghi 
        /// </returns>
        /// Created: NTVU - 05/09/2023
        public async Task<T> GetByCodeAsync(string entityCode)
        {
            try
            {
                using (_mySqlConnection = new(_connectionString))
                {
                    var sqlCommand = $"SELECT * FROM {_className} WHERE {_className}Code = @entityCode";
                    var res = await _mySqlConnection.QueryFirstOrDefaultAsync<T>(sql: sqlCommand, param: new { entityCode });
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new MISADatabaseException(ex.Message); 
            }


        }


        /// <summary>
        /// Lấy toàn bộ bản ghi
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <returns>
        /// Toàn bộ bản ghi
        /// </returns>
        /// Created: NTVU - 05/09/2023

        public async Task<IEnumerable<TViewModel>> GetAllAsync<TViewModel>()
        {
            try
            {
                using (_mySqlConnection = new(_connectionString))
                {
                    string sqlCommandText = $"Proc_{_className}_GetAll";
                    var entities = await _mySqlConnection.QueryAsync<TViewModel>(sql: sqlCommandText, commandType: System.Data.CommandType.StoredProcedure);
                    return entities;


                }
            }
            catch (Exception ex)
            {

            
                throw new MISADatabaseException(ex.Message);
              
            }

        }
        /// <summary>
        /// Lọc ra các bản ghi phù hợp, lấy paging
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="search"></param>
        /// <returns>
        /// Created: NTVU - 05/09/2023
        /// </returns>

        public async Task<FilterDataResponse<TViewModel>> FilterAsync<TViewModel>(int pageSize, int page, string search)
        {

            try
            {
                using (_mySqlConnection = new(_connectionString))
                {
                    string sqlCommandText = $"Proc_{_className}_Filter";

                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("@q", search);
                    dynamicParams.Add("@page", page);
                    dynamicParams.Add("@pageSize", pageSize);

                    var multi = await _mySqlConnection.QueryMultipleAsync(sql: sqlCommandText, commandType: System.Data.CommandType.StoredProcedure, param: dynamicParams);
                    var totalRecords = await multi.ReadFirstAsync<int>();
                    var res = await multi.ReadAsync<TViewModel>();

                    return new FilterDataResponse<TViewModel>()
                    {
                        TotalRecords = totalRecords,
                        Data = res
                    };
                }
            }
            catch (Exception ex)
            {


                throw new MISADatabaseException(ex.Message);
            }

        }

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>
        /// Thông tin bản ghi
        /// </returns>
        public async Task<T> GetByIdAsync(Guid entityId)
        {
            try
            {
                using (_mySqlConnection = new(_connectionString))
                {
                    var sqlCommand = $"SELECT * FROM {_className} WHERE {_className}Id = @entityId";
                    var res = await _mySqlConnection.QuerySingleAsync<T>(sql: sqlCommand, param: new { entityId });
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new MISADatabaseException(ex.Message);
            }




        } 
        #endregion

        /// <summary>
        /// Thêm bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// Số bản ghi thêm được
        /// </returns>
        /// Created: NTVU - 05/09/2023
        public async Task<int> InsertAsync(T entity)
        {

            var props = typeof(T).GetProperties();
            var columnsName = new StringBuilder();
            var columnsValue = new StringBuilder();
            var dynamicParams = new DynamicParameters();
            var delimiter = "";

            try
            {
                foreach (var prop in props)
                {
                    var propName = prop.Name;


                    var paramName = $"@{propName}";

                    columnsName.Append($"{delimiter} {propName}");

                    columnsValue.Append($"{delimiter} {paramName}");
                    delimiter = ",";

                    if (propName != $"{_className}Id")
                    {
                        var propValue = prop.GetValue(entity);
                        dynamicParams.Add(paramName, propValue);

                    }
                    else
                    {

                        var entityId = Guid.NewGuid();
                        dynamicParams.Add(paramName, entityId);
                    }

                }

                using (_mySqlConnection = new(_connectionString))
                {
                    string sqlCommandText = $"INSERT INTO {_className} ({columnsName}) VALUES ({columnsValue})";
                    var res = await _mySqlConnection.ExecuteAsync(sql: sqlCommandText, param: dynamicParams);
                    return res;
                }
            }
            catch (Exception ex)
            {


                throw new MISADatabaseException(ex.Message);
            }


        }


        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns>
        /// Số bản ghi bị ảnh hưởng
        /// </returns>
        /// Created: NTVU - 05/09/2023
        public async Task<int> UpdateAsync(T entity, Guid entityId)
        {
            var props = typeof(T).GetProperties();
            var columnsName = new StringBuilder();

            var dynamicParams = new DynamicParameters();

            var delimiter = "";

            try
            {
                foreach (var prop in props)
                {
                    var propName = prop.Name;

                    if (propName != $"{_className}Id")
                    {
                        var paramName = $"@{propName}";

                        columnsName.Append($"{delimiter} {propName} =  {paramName} ");


                        delimiter = ",";

                        var propValue = prop.GetValue(entity);
                        dynamicParams.Add(paramName, propValue);
                    }
                }
                dynamicParams.Add($"{_className}Id", entityId);
                using (_mySqlConnection = new(_connectionString))
                {
                    string sqlCommandText = $"UPDATE {_className} SET {columnsName} WHERE {_className}Id = @{_className}Id";
                    var res = await _mySqlConnection.ExecuteAsync(sql: sqlCommandText, param: dynamicParams);
                    return res;
                }
            }
            catch (Exception ex)
            {


                throw new MISADatabaseException(ex.Message);
            }


        }

        #region Delete

        /// <summary>
        /// Xóa bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>
        /// Số bản ghi bị ảnh hưởng
        /// </returns>
        /// Created: NTVU - 05/09/2023
        public async Task<int> DeleteOneAsync(Guid entityId)
        {
            try
            {
                using (_mySqlConnection = new(_connectionString))
                {
                    string sqlCommandText = $"DELETE QUICK FROM {_className} WHERE {_className}Id = @entityId";
                    var res = await _mySqlConnection.ExecuteAsync(sql: sqlCommandText, param: new { entityId });
                   
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new MISADatabaseException(ex.Message);
            }

        }

        /// <summary>
        /// Xóa bản ghi theo mã
        /// </summary>
        /// <param name="entityCode"></param>
        /// <returns>Số bản ghi bị ảnh hướng</returns>
        /// Created: NTVU - 05/09/2023
        public async Task<int> DeleteOneByCodeAsync(string entityCode)
        {
            try
            {
                using (_mySqlConnection = new(_connectionString))
                {
                    string sqlCommandText = $"DELETE QUICK FROM {_className} WHERE {_className}Code = @entityCode";
                    var res = await _mySqlConnection.ExecuteAsync(sql: sqlCommandText, param: new { entityCode });
                    return res;
                }
            }
            catch (Exception ex)
            {

                throw new MISADatabaseException(ex.Message);
            }

        }

        /// <summary>
        /// Xóa nhiều bản ghi theo id
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns>
        /// Số bản ghi bị xóa
        /// </returns>
        /// Created: NTVU - 05/09/2023
        public async Task<int> DeleteMultiAsync(List<Guid> entityIds)
        {
            try
            {
                using (_mySqlConnection = new(_connectionString))
                {
                    string sqlCommandText = $"DELETE QUICK FROM {_className} WHERE {_className}Id IN @entityIds";
                    var res = await _mySqlConnection.ExecuteAsync(sql: sqlCommandText, param: new { entityIds });
                    return res;
                }
            }
            catch (Exception ex)
            {


                throw new MISADatabaseException(ex.Message);
            }

        }

        /// <summary>
        /// Xóa toàn bộ bản ghi
        /// </summary>
        /// <returns>
        /// Số bản ghi bị xóa
        /// </returns>
        /// Created: NTVU - 05/09/2023
        public async Task<int> DeleteAllAsync()
        {
            try
            {
                using (_mySqlConnection = new(_connectionString))
                {
                    string sqlCommandText = $"DELETE QUICK FROM {_className}";
                    var res = await _mySqlConnection.ExecuteAsync(sql: sqlCommandText);
                    return res;
                }
            }
            catch (Exception ex)
            {


                throw new MISADatabaseException(ex.Message);
            }

        } 
        #endregion

        #endregion

    }
}
