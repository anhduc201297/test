using System.Diagnostics;

namespace aspnetcore
{
    internal class ErrorResponse
    {
        /// <summary>
        /// Thông báo gửi đến người dùng
        /// </summary>
        public string? UserMsg { get; set; }

        /// <summary>
        /// Thông báo kĩ thuật gửi đến cho người phát triển
        /// </summary>
        public string? DevMsg { get; set; }

        /// <summary>
        /// TraceID lưu lại ID lỗi
        /// </summary>
        public Guid TraceId { get; set; }


        /// <summary>
        /// Mã lỗi
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Body response cho FE các thông tin bổ sung
        /// </summary>
        public string? MoreInfo { get; set; }
        public string V { get; }
        public Guid Guid { get; }

        // Constructor Error response
        public ErrorResponse(string errorCode, Guid traceId, string? devMsg, string? userMsg,  string? moreInfo)
        {
            this.ErrorCode = errorCode;
            this.TraceId = traceId;
            this.DevMsg = devMsg;
            this.UserMsg = userMsg;
            this.MoreInfo = moreInfo;
        }

        public ErrorResponse(string errorCode, Guid traceId)
        {
            this.ErrorCode = errorCode;
            this.TraceId = traceId;
            this.DevMsg = "ERROR, please contact us for support!";
            this.UserMsg = "Có lỗi xảy ra, vui lòng liên hệ với chúng tôi để được hỗ trợ";
            this.MoreInfo = "Bug undefined";
        }

    }
}