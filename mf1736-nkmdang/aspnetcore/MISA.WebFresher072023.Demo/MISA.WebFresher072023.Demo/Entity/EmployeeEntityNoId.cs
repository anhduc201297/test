using MISA.WebFresher072023.Demo.Enum;
using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher072023.Demo.Entity
{
    public class EmployeeEntityNoId
    {
        [Required(ErrorMessage = "EmployeeCode is required.")]
        [StringLength(20, ErrorMessage = "Employee code must be less than 20 characters.")]
        public string EmployeeCode { get; set; }


        [Required(ErrorMessage = "FullName is required.")]
        [StringLength(100, ErrorMessage = "Full name must be less than 100 characters.")]
        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public GenderEnum Gender { get; set; }

        public Guid DepartmentId { get; set; }

        //public string DepartmentName { get; set; }  

        public string LandLinePhone { get; set; }

        public string Mobile { get; set; }

        public string PositionName { get; set; }

        public string Address { get; set; }

        public string BankAccount { get; set; }

        public string BankBranch { get; set; }

        public string BankName { get; set; }

        public string PersonalIdentification { get; set; }

        public DateTime PIDateCreated { get; set; }

        public string PIPlaceCreated { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
