using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher072023.Entity
{
	/// <summary>
	/// Cung cấp class chứa thông tin nhân viên
	/// </summary>
	/// Created by: NVDUNG (09/09/2023)
	public class EmployeeEntity
	{
		#region Property

		/// <summary>
		/// Định danh nhân viên
		/// </summary>
		public Guid EmployeeId { get; set; }

		/// <summary>
		/// Mã nhân viên
		/// </summary>
		[RegularExpression(@"\d$", ErrorMessage = "Mã nhân viên phải kết thúc bằng số.")]
		public string? EmployeeCode { get; set; }

		/// <summary>
		/// Họ, đệm nhân viên
		/// </summary>
		public string? FirstName { get; set; }

		/// <summary>
		/// Tên nhân viên
		/// </summary>
		public string? LastName { get; set; }

		/// <summary>
		/// Họ và tên nhân viên
		/// </summary>
		[Required(ErrorMessage = "Họ tên không được để trống.")]
		public string FullName { get; set; }

		/// <summary>
		/// Ngày sinh nhân viên
		/// </summary>
		public DateTime DateOfBirth { get; set; }

		/// <summary>
		/// Giới tính nhân viên
		/// <options>0, 1, 2</options>
		/// </summary>
		public GenderEnum Gender { get; set; }

		/// <summary>
		/// Số chứng minh nhân dân
		/// </summary>
		public string? IdentityNumber { get; set; }

		/// <summary>
		/// Ngày cấp chứng minh nhân dân
		/// </summary>
		public DateTime? IdentityDate { get; set; }

		/// <summary>
		/// Nơi cấp chứng minh nhân dân
		/// </summary>
		public string? IdentityPlace { get; set; }

		/// <summary>
		/// Địa chỉ
		/// </summary>
		public string? Address { get; set; }

		/// <summary>
		/// Số điện thoại
		/// </summary>
		public string? PhoneNumber { get; set; }

		/// <summary>
		/// Email
		/// </summary>
		public string? Email { get; set; }


		/// <summary>
		/// Số tài khoản ngân hàng
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
		/// Định danh phòng ban của nhân viên
		/// </summary>
		
		public string DepartmentId { get; set; }


		/// <summary>
		/// Định danh chức vụ của nhân viên
		/// </summary>
		public string? PositionId { get; set; }

		/// <summary>
		/// Ngày tạo dữ liệu
		/// </summary>
		public DateTime CreatedDate { get; set; }


		/// <summary>
		/// Người tạo dữ liệu
		/// </summary>
		public string? CreatedBy { get; set; }

		/// <summary>
		/// Ngày sửa dữ liệu
		/// </summary>
		public DateTime ModifiedDate { get; set; }


		/// <summary>
		/// Người sửa dữ liệu
		/// </summary>
		public string? ModifiedBy { get; set; }

		#endregion
	}
}
