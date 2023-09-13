namespace MISA.Web07.MVDuy
{
    public class EmployeeEntity
    {
        public Guid EmployeeId { get; set; }

        public string FullName { get; set; }

        public string EmployeeCode { get; set; }

        public string? DepartmentId { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public GenderEnum? Gender { get; set; }

        public string? IdentityNumber { get; set; }

        public DateTime? IdentityDate { get; set; }

        public string? IdentityPlace { get; set; }

        public string? PositionName { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? BankName { get; set; }

        public string? BankNumber { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set;}

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set;}

    }
}
