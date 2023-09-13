using FluentValidation;
using MISA.SME.Application.Interfaces;
using MISA.SME.Domain.Entities;

namespace MISA.SME.Infrastructure.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        private readonly IValidator<Department> _validator;

        public DepartmentRepository(IValidator<Department> validator) : base(validator)
        {
            _validator = validator;
        }
    }
}
