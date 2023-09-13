using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MISA.SME.Application.Validators;

namespace MISA.SME.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<EmployeeValidator>();
            services.AddValidatorsFromAssemblyContaining<DepartmentValidator>();
        }
    }
}
