using MISA.SME.Domain.Common.Enums;

namespace MISA.SME.Domain.Exceptions
{
    public class ErrorResponse
    {
        public ErrorCode ErrorCode;

        public string? DevMsg { get; set; }
        public string? UserMsg { get; set; }
        public string? MoreInfo { get; set; }
        public string? TraceId { get; set; }
    }
}
