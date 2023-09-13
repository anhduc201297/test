using Microsoft.Extensions.DependencyInjection;
using MISA.SME.Application.Interfaces;
using MISA.SME.Infrastructure.Repositories;

namespace MISA.SME.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
