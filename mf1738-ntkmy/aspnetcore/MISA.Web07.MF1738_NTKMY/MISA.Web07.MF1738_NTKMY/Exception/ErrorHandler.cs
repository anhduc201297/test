using System.Text.Json;

namespace MISA.Web07.MF1738_NTKMY
{
    /// <summary>
    /// Lớp lỗi cơ sở
    /// Author: NTKMY
    /// 11/09/2023
    /// </summary>
    public class ErrorHandler
    {
        #region Properties

        public ErrorCodeEnum ErrorCode { get; set; }

        public string? DevMsg { get; set; }

        public string? UserMsg { get; set; }

        public string? TraceId { get; set; }

        public string? MoreInfo { get; set; }

        #endregion

        #region Contructors
        public ErrorHandler( string? devMsg, string? userMsg)
        {
            DevMsg = devMsg;
            UserMsg = userMsg;
            MoreInfo = "";
            TraceId = "";
        }
        #endregion
    }
}
