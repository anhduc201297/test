namespace MISA.SME.Application.Wrappers
{
    public class PagedResult<T>
    {
        public IReadOnlyList<T> Data { get; set; }

        public int TotalRecord { get; set; }

        public PagedResult(IReadOnlyList<T> data, int totalRecord)
        {
            Data = data;
            TotalRecord = totalRecord;
        }
    }
}
