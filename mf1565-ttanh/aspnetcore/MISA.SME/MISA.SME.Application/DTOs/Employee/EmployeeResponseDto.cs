using MISA.SME.Domain.Common.Enums;
using MISA.SME.Domain.Entities;

namespace MISA.SME.Domain.DTOs.Employee
{
    /// <summary>
    /// Thông tin nhân viên
    /// </summary>
    public class EmployeeResponseDto : BaseEntity
    {
        public Guid EmployeeID { get; set; }

        public string? EmployeeCode { get; set; }

        public string? FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }
        public string? IdentityNumber { get; set; }

        public DateTime IdentityIssuedDate { get; set; }

        public string? IdentityIssuedPlace { get; set; }

        public string? PositionName { get; set; }

        public Guid DepartmentID { get; set; }

        // add DepartmentName
        public string? DepartmentName { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? MobilePhone { get; set; }

        public string? LandlinePhone { get; set; }

        public string? BankAccount { get; set; }

        public string? BankName { get; set; }

        public string? BankBranch { get; set; }
    }
}
