namespace MISA.SME.Application.Wrappers
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public object? Result { get; set; }
        public int? AffectedRows { get; set; }

        public Response()
        {
            IsSuccess = false;
        }

        public Response(object result, string? message = null)
        {
            IsSuccess = true;
            Message = message;
            Result = result;
        }

        public Response(int affectedRows, string? message = null)
        {
            IsSuccess = true;
            Message = message;
            AffectedRows = affectedRows;
        }

        public Response(string message)
        {
            IsSuccess = false;
            Message = message;
        }
    }
}
