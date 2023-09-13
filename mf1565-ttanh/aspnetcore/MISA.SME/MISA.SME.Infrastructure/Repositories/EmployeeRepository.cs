using Dapper;
using FluentValidation;
using MISA.SME.Application.Interfaces;
using MISA.SME.Application.Wrappers;
using MISA.SME.Domain.DTOs.Employee;
using MISA.SME.Domain.Entities;
using System.Data;

namespace MISA.SME.Infrastructure.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly IValidator<Employee> _validator;

        public EmployeeRepository(IValidator<Employee> validator) : base(validator)
        {
            _validator = validator;
        }

        public async Task<Response> GetPaginationAsync(int limit = 20, int offset = 0)
        {
            // Chuẩn bị tên store procedure
            string storeProcudureName = "Proc_Employee_GetPagination";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@Limit", limit);
            parameters.Add("@Offset", offset);

            // Khởi tạo kết nối tới Database
            var dbConnection = GetOpenConnection();

            // Thực hiện gọi vào Database để chạy stored procedure
            var result = await dbConnection.QueryAsync<EmployeeResponseDto>(storeProcudureName, parameters, commandType: CommandType.StoredProcedure);

            // Khởi tạo dữ liệu trả về
            var pagedResult = new PagedResult<EmployeeResponseDto>(result.ToList(), result.ToList().Count);

            // Trả về kết quả
            return new Response(pagedResult);
        }

        public async Task<Response> GetFilteringAndPaginationAsync(string? keyword, int limit = 10, int offset = 0)
        {
            // Chuẩn bị tên store procedure
            string storeProcudureName = "Proc_Employee_GetFilteringAndPagination";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@Keyword", keyword);
            parameters.Add("@Limit", limit);
            parameters.Add("@Offset", offset);

            // Khởi tạo kết nối tới Database
            var dbConnection = GetOpenConnection();

            // Thực hiện gọi vào Database để chạy stored procedure
            var result = await dbConnection.QueryAsync<EmployeeResponseDto>(storeProcudureName, parameters, commandType: CommandType.StoredProcedure);

            // Khởi tạo dữ liệu trả về
            var pagedResult = new PagedResult<EmployeeResponseDto>(result.ToList(), result.ToList().Count);

            // Trả về kết quả
            return new Response(pagedResult);
        }

        /// <summary>
        /// lấy dữ liệu nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="code">mã nhân viên</param>
        /// <returns>dữ liệu nhân viên</returns>
        /// created by: TTANH - 11/09/2023
        public async Task<Response> GetByCodeAsync(string code)
        {
            // Chuẩn bị tên store procedure
            string storeProcudureName = $"Proc_Employee_GetByCode";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeCode", code);

            // Khởi tạo kết nối tới Database
            var dbConnection = GetOpenConnection();

            // Thực hiện gọi vào Database để chạy stored procedure
            var result = await dbConnection.QuerySingleOrDefaultAsync<Employee>(storeProcudureName, parameters, commandType: CommandType.StoredProcedure);

            Console.WriteLine(result);

            // Trả về kết quả
            return new Response(result);
        }
    }
}
