using FluentValidation;
using MISA.SME.Domain.Entities;

namespace MISA.SME.Application.Validators
{
    public class DepartmentValidator : AbstractValidator<Department>
    {
        public DepartmentValidator()
        {
            RuleFor(d => d.DepartmentName).NotEmpty();
        }
    }
}
