using MISA.Web07.Core.Enums;
using MISA.Web07.Core.MAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.Core.Entities
{
    public class Employee : BaseEntity
    {
        #region Constructor
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Employee()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
         
        }
        #endregion

        #region Property

        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [NotEmpty]
        [MaxLength(20)]
        [PropertyName("Mã nhân viên")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ và tên
        /// </summary>
        [NotEmpty]
        [MaxLength(100)]
        [PropertyName("Tên nhân viên")]
        public string FullName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [EarlierNow]
        [PropertyName("Ngày sinh")]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// giới tính
        /// </summary>
      
        public GenderEnum? Gender { get; set; }

        /// <summary>
        /// Email nhân viên
        /// </summary>
        [FormatType("email")]
        [MaxLength(50)]
        public string? Email { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        [PropertyName("ĐT cố định")]
        [MaxLength(50)]
        public string? PhoneHome { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        [PropertyName("ĐT di động")]
        [MaxLength(50)]
        public string? PhoneMobile { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [PropertyName("Địa chỉ")]
        [MaxLength(255)]
        public string? Address { get; set; }

        /// <summary>
        /// Chức vụ
        /// </summary>
        [PropertyName("Chức vụ")]
        [MaxLength(255)]
        public string? PositionName { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        [PropertyName("Số tài khoản")]
        [MaxLength(25)]
        public string? BankAccountNo { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        [PropertyName("Chi nhánh ngân hàng")]
        [MaxLength(255)]
        public string? BankAccountPlace { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [PropertyName("Tên ngân hàng")]
        [MaxLength(255)]
        public string? BankName { get;set; }

        /// <summary>
        /// Số CCCD/CMND
        /// </summary>
        [PropertyName("Số CCCD/CMND")]
        [MaxLength(25)]
        public string? IdentityNo { get; set; }

        /// <summary>
        /// Ngày cấp CCCD/CMND
        /// </summary>
        [EarlierNow]
        [PropertyName("Ngày cấp")]
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp CCCD/CMND
        /// </summary>
        [PropertyName("Nơi cấp")]
        [MaxLength(255)]
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Khóa ngoại
        /// Phòng ban
        /// </summary>
        [NotEmpty]
        [MaxLength(255)]
        public Guid DepartmentId { get; set; }

        ///// <summary>
        ///// Đơn vị của nhân viên
        ///// </summary>
        //public Department Department { get; set; }
        #endregion
    }
}
