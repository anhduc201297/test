namespace MISA.Web07.MF1732_DKTUAN
{
    public class CustomErrorMessage
    {
        public string errorCode { get; set; } = "1";
        public string UserMsg { get; set; } = "Có lỗi xảy ra! Vui lòng liên hệ với MISA.";
        public string MoreInfo { get; set; } = "https://openapi.misa.com.vn/errorcode/e001";
        public string TraceId { get; set; } = "";

        public string DevMsg { get; set; } = "";

    }
}
