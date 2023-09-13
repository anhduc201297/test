using MISA.SME.Application.Interfaces;

namespace MISA.SME.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IEmployeeRepository Employees { get; }
        public IDepartmentRepository Departments { get; }

        public UnitOfWork(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            Employees = employeeRepository;
            Departments = departmentRepository;
        }
    }
}
