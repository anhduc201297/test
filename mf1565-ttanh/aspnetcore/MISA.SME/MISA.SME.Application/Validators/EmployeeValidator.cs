using FluentValidation;
using MISA.SME.Domain.Entities;

namespace MISA.SME.Application.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        //private static readonly IEmployeeRepository _employeeRepository;

        public EmployeeValidator()
        {
            // validate EmployeeCode
            RuleFor(e => e.EmployeeCode).NotEmpty().WithMessage(ValidationResource.Empty_EmployeeCode);

            //RuleFor(e => e.EmployeeCode).MustAsync(async (employeeCode, cancellation) =>
            //{
            //    var result = await _employeeRepository.GetByCodeAsync(employeeCode);
            //    Console.WriteLine(result);
            //    return result.IsSuccess;
            //}).WithMessage(e => $"Mã nhân viên <{e.EmployeeCode}> đã tồn tại trong hệ thống, vui lòng kiểm tra lại.");

            // validate FullName
            RuleFor(e => e.FullName).NotEmpty().WithMessage(ValidationResource.Empty_FullName);

            // validate DepartmentID
            RuleFor(e => e.DepartmentID).NotEmpty().WithMessage(ValidationResource.Empty_Department);

            // validate DateOfBirth
            RuleFor(e => e.DateOfBirth).NotEmpty();
        }
    }
}
