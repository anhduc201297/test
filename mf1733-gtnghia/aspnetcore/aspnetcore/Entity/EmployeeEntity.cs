namespace aspnetcore
{
    public class EmployeeEntity
    {
        /// <summary>
        /// Định danh của nhân viên
        /// </summary>
        public Guid EmployeeId { get; set; } 

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string? EmployeeCode { get; set; } 

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string? FullName { get; set; } 

        /// <summary>
        /// Ngày sinh nhân viên
        /// </summary>
        public DateTime DateOfBirth { get; set; } 

        /// <summary>
        /// Giới tính nhân viên
        /// </summary>
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// Số Căn cước công dân/Chứng minh thư
        /// </summary>
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp CCCD/CMT
        /// </summary>
        public DateTime IdentityDate { get; set; } //need to fix: to date only

        /// <summary>
        /// Nơi cấp CCCD/CMT
        /// </summary>
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Lương nhân viên
        /// </summary>
        public int Salary { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Mã định danh Phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Mã số thuế cá nhân
        /// </summary>
        public string? PersonalTaxCode { get; set; }

        /// <summary>
        /// Mã vị trí
        /// </summary>
        public string? PositionCode { get; set; }

        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// Ngày vào làm
        /// </summary>
        public DateTime joinDate { get; set; }

        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public int MaritalStatus { get; set; }

        /// <summary>
        /// Trình độ học vấn
        /// </summary>
        public string? EducationalBackground { get; set; }

        /// <summary>
        /// Tên trình độ chuyên môn
        /// </summary>
        public string? QualificationName { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        public string? BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        public string? BankBranch { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người sửa lần cuối
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Thời gian sửa lần cuối
        /// </summary>
        public DateTime ModifiedDate { get; set; }


    }
}
