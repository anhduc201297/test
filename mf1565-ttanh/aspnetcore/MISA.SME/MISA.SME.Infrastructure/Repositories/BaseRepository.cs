using Dapper;
using FluentValidation;
using FluentValidation.Results;
using MISA.SME.Application.Interfaces;
using MISA.SME.Application.Wrappers;
using MySqlConnector;
using System.Data;

namespace MISA.SME.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly Type _entityType = typeof(T);
        private readonly IValidator<T> _validator;

        public BaseRepository(IValidator<T> validator)
        {
            _validator = validator;
        }

        /// <summary>
        /// mở kết nối database
        /// </summary>
        /// <returns></returns>
        /// created by: TTANH - 01/09/2023
        /// modified by: TTANH - 02/09/2023
        public IDbConnection GetOpenConnection()
        {
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);
            return mySqlConnection;
        }

        /// <summary>
        /// lấy danh sách thực thể từ database
        /// </summary>
        /// <returns></returns>
        /// created by: TTANH - 07/09/2023
        /// modified by: TTANH - 09/09/2023
        public async Task<Response> GetAllAsync()
        {
            // Chuẩn bị tên store procedure
            string storeProcudureName = $"Proc_{_entityType.Name}_GetAll";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();

            // Khởi tạo kết nối tới Database
            var dbConnection = GetOpenConnection();

            // Thực hiện gọi vào Database để chạy stored procedure
            var result = await dbConnection.QueryAsync<T>(storeProcudureName, parameters, commandType: CommandType.StoredProcedure);

            // Trả về kết quả
            return new Response(result.ToList());
        }

        /// <summary>
        /// lấy dữ liệu thực thể theo ID
        /// </summary>
        /// <param name="id">ID thực thể</param>
        /// <returns></returns>
        /// created by: TTANH - 09/09/2023
        /// modified by: TTANH - 10/09/2023
        public async Task<Response> GetByIdAsync(Guid id)
        {
            // Chuẩn bị tên store procedure
            string storeProcudureName = $"Proc_{_entityType.Name}_GetByID";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add($"@{_entityType.Name}ID", id);

            // Khởi tạo kết nối tới Database
            var dbConnection = GetOpenConnection();

            // Thực hiện gọi vào Database để chạy stored procedure
            var result = await dbConnection.QuerySingleOrDefaultAsync<T>(storeProcudureName, parameters, commandType: CommandType.StoredProcedure);

            // Trả về kết quả
            return new Response(result);
        }

        /// <summary>
        /// thêm thực thể vào database
        /// </summary>
        /// <param name="entity">dữ liệu thực thể</param>
        /// <returns></returns>
        /// created by: TTANH - 10/09/2023
        /// modified by: TTANH - 11/09/2023
        public async Task<Response> AddAsync(T entity)
        {
            // Validate dữ liệu đầu vào
            ValidationResult validationResult = await _validator.ValidateAsync(entity);

            if (!validationResult.IsValid)
            {
                return new Response(validationResult);
            }

            // Chuẩn bị tên store procedure
            string storeProcudureName = $"Proc_{_entityType.Name}_InsertOne";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();

            var props = _entityType.GetProperties();

            // Thêm tham số tương ứng với mỗi thuộc tính
            foreach (var prop in props)
            {
                var name = prop.Name;

                if (name == $"{_entityType}ID")
                {
                    // tạo guid mới làm id
                    parameters.Add($"@{name}", Guid.NewGuid().ToString());
                }
                else if (name == "CreatedDate")
                {
                    // lưu thời gian hiện tại vào CreatedDate
                    parameters.Add($"@{name}", DateTime.Now.ToLocalTime());
                }
                else
                {
                    parameters.Add($"@{name}", prop.GetValue(entity));
                }
            }

            // Khởi tạo kết nối tới Database
            var dbConnection = GetOpenConnection();

            // Thực hiện gọi vào Database để chạy stored procedure
            var affectedRows = await dbConnection.ExecuteAsync(storeProcudureName, parameters, commandType: CommandType.StoredProcedure);

            // Trả về kết quả
            return new Response(affectedRows);
        }

        /// <summary>
        /// cập nhật thông tin thực thể
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// created by: TTANH - 11/09/2023
        public async Task<Response> UpdateAsync(T entity)
        {
            // Validate dữ liệu đầu vào
            ValidationResult validationResult = await _validator.ValidateAsync(entity);

            if (!validationResult.IsValid)
            {
                return new Response(validationResult);
            }

            // Chuẩn bị tên store procedure
            string storeProcudureName = $"Proc_{_entityType.Name}_UpdateOne";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();

            var props = _entityType.GetProperties();

            // Thêm tham số tương ứng với mỗi thuộc tính
            foreach (var prop in props)
            {
                var name = prop.Name;

                if (name == "ModifiedDate")
                {
                    // lưu thời gian hiện tại vào ModifiedDate
                    parameters.Add($"@{name}", DateTime.Now.ToLocalTime());
                }
                else
                {
                    parameters.Add($"@{name}", prop.GetValue(entity));
                }
            }

            // Khởi tạo kết nối tới Database
            var dbConnection = GetOpenConnection();

            // Thực hiện gọi vào Database để chạy stored procedure
            var affectedRows = await dbConnection.ExecuteAsync(storeProcudureName, parameters, commandType: CommandType.StoredProcedure);

            // Trả về kết quả
            return new Response(affectedRows);
        }

        /// <summary>
        /// xóa thực thể
        /// </summary>
        /// <param name="id">ID thực thể bị xóa</param>
        /// <returns></returns>
        /// created by: TTANH - 10/09/2023
        /// modified by: TTANH - 11/09/2023
        public async Task<Response> DeleteAsync(Guid id)
        {
            // Chuẩn bị tên store procedure
            string storeProcudureName = $"Proc_{_entityType.Name}_DeleteByID";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add($"@{_entityType.Name}ID", id);

            // Khởi tạo kết nối tới Database
            var dbConnection = GetOpenConnection();

            // Thực hiện gọi vào Database để chạy stored procedure
            var affectedRows = await dbConnection.ExecuteAsync(storeProcudureName, parameters, commandType: CommandType.StoredProcedure);

            // Trả về kết quả
            return new Response(affectedRows);
        }
    }
}
