namespace MISA.WebFresher202307.MF1741.TTKIEN
{
    public class EmployeeEntity
    {
        /// <summary>
        /// Định danh của nhân viên
        /// </summary>
        public Guid EmployeeId { get; set; }

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
        public Guid DepartmentId {  get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName { get; set;}

        /// <summary>
        /// Chức danh
        /// </summary>
        public string PositionName { get; set;}
        
        /// <summary>
        /// Số căn cước công dân
        /// </summary>
        public string IdentityNumber { get; set; }

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

