namespace MISA.WebFresher202307.MF1741.TTKIEN
{
    public class NewEmployeeEntity
    {
        /// <summary>
        /// Mã của nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên của nhân viên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Ngày sinh của nhân viên
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính của nhân viên
        /// </summary>
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// Định danh đơn vị
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Số căn cước công dân
        /// </summary>
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp căn cước công dân
        /// </summary>
        public DateTime IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp căn cước công dân
        /// </summary>
        public DateTime IdentityPlace { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public DateTime Address { get; set; }

        /// <summary>
        /// Điện thoại cố định
        /// </summary>
        public DateTime TelephoneNumber { get; set; }

        /// <summary>
        /// Điện thoại di động
        /// </summary>
        public DateTime MobilePhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public DateTime Email { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        public string BankBranch { get; set; }
    }
}
