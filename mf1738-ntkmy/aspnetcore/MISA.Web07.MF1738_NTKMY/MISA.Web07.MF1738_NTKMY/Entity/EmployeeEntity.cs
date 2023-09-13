namespace MISA.Web07.MF1738_NTKMY
{
    public class EmployeeEntity : BaseEntity
    {

        public Guid EmployeeId { get; set; }

        public string EmployeeCode { get; set; }

        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public GenderEnum Gender { get; set; }

        public string DepartmenName { get; set; }

        public string IdentityNumber { get; set; }

        public string Position { get; set; }

        public string ProvideAdrress { get; set; }

        public string Adress { get; set; }

        public string PhoneNumber { get; set; }

        public string LandlinePhone { get; set; }

        public string Email { get; set; }

        public string BankAccount { get; set; }

        public string BankName { get; set; }

        public string BankBranch { get; set; }

        public Guid DepartmentId { get; set; }
    }
}
