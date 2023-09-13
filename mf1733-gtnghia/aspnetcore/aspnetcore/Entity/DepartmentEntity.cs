namespace aspnetcore
{
    /// <summary>
    /// Thực thể Phòng ban
    /// Created by: mf1733-gtnghia (11/9/2023)
    /// </summary>
    public class DepartmentEntity
    {
        /// <summary>
        /// Mã định danh phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã  phòng ban
        /// </summary>
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }

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
