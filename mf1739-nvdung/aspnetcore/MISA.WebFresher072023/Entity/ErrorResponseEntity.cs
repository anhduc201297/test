namespace MISA.WebFresher072023.Entity
{
    /// <summary>
    /// Đối tượng trả về lỗi chung
    /// </summary>
    /// Create by: NVDUNG (09/09/2023)
    public class ErrorResponseEntity
    {
        #region Property

        /// <summary>
        /// Định danh của mã lỗi nội bộ
        /// </summary>
        /// Create by: NVDUNG (09/09/2023)
        public int errorCode { get; set; }

        /// <summary>
        /// Thông báo cho Dev. Thông báo ngắn gọn để thông báo cho Dev biết vấn đề đang gặp phải
        /// </summary>
        /// Create by: NVDUNG (09/09/2023)
        public string? devMsg { get; set; }

        /// <summary>
        /// Thông báo cho user. Không bắt buộc, tùy theo đặc thù từng dịch vụ và client tích hợp
        /// </summary>
        /// Create by: NVDUNG (09/09/2023)
        public string? userMsg { get; set; }

        /// <summary>
        /// Thông tin chi tiết hơn về vấn đề. Ví dụ: Đường dẫn mô tả về mã lỗi
        /// </summary>
        /// Create by: NVDUNG (09/09/2023)
        public string? moreInfo { get; set; }

        /// <summary>
        /// Mã để tra cứu thông tin log: ELK, file log,...
        /// </summary>
        /// Create by: NVDUNG (09/09/2023)
        public string? traceId { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Hàm tạo không có tham số
        /// </summary>
        public ErrorResponseEntity() { }


        /// <summary>
        /// Hàm tạo có tham số cho class ErrorResponseEntity
        /// </summary>
        /// <param name="errorCode">Định danh của mã lỗi nội bộ</param>
        /// <param name="devMsg">Thông báo cho Dev. Thông báo ngắn gọn để thông báo cho Dev biết vấn đề đang gặp phải</param>
        /// <param name="userMsg">Thông báo cho user. Không bắt buộc, tùy theo đặc thù từng dịch vụ và client tích hợp</param>
        /// <param name="moreInfo">Thông tin chi tiết hơn về vấn đề. Ví dụ: Đường dẫn mô tả về mã lỗi</param>
        /// <param name="traceId">Mã để tra cứu thông tin log: ELK, file log,...</param>
        /// Created by: NVDUNG (09/09/2023)
        public ErrorResponseEntity(int errorCode, string devMsg, string userMsg, string moreInfo, string traceId)
        {
            this.errorCode = errorCode;
            this.devMsg = devMsg;
            this.userMsg = userMsg;
            this.moreInfo = moreInfo;
            this.traceId = traceId;
        }

        #endregion
    }
}
