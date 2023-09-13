using System.ComponentModel.DataAnnotations;

namespace MISA.Web202307
{
    /// <summary>
    /// Phòng ban
    /// </summary>
    public class DepartmentEntity : BaseEntity
    {
        /// <summary>
        /// ID phòng ban
        /// </summary>
        [Key]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [Required(ErrorMessage = "Tên phòng ban không được để trống")]
        public string DepartmentName { get; set; }
    }
}
