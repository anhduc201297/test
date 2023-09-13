using System.ComponentModel.DataAnnotations;
using System.Reflection;
using MISA.WebFresher072023.Demo.Enum;

namespace MISA.WebFresher072023.Demo.Entity
{
    public class EmployeeEntity
    {
        /// <summary>
        /// Id nhân viên
        /// </summary>
        [Required]
        public Guid EmployeeID { get; set; }

        [Required(ErrorMessage = "Mã nhân viên là bắt buộc")]
        [MaxLength(20)]
        [RegularExpression(@".*\d$", ErrorMessage = "Mã nhân viên viên phải kết thúc bằng chữ số")]
        public string? EmployeeCode { get; set; }

        [Required(ErrorMessage = "Tên nhân viên là bắt buộc")]
        public string? FullName { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }


        public string? IdentityNumber { get; set; }

        public DateTime? IdentityIssuedDate { get; set; }

        public string? IdentityIssuedPlace { get; set; }

        public string? Address { get; set; }

        public string? Mobile { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? BankAccount { get; set; }

        public string? BankName { get; set; }

        public string? BankBranch { get; set; }

        /// <summary>
        /// ID phòng ban
        /// </summary>
        [Required(ErrorMessage = "Bộ phận là bắt buộc")]
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// Có phải là khách hàng không
        /// </summary>
        public bool? IsCustomer { get; set; }

        /// <summary>
        /// Có phải là nhà cung cấp không
        /// </summary>
        public bool? IsSupplier { get; set; }


        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa gần nhất
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa gần nhất
        /// </summary>
        public string? ModifiedBy { get; set; }

    }
}

