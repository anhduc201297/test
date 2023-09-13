namespace MISA.SME.Domain.Entities
{
    public class Department : BaseEntity
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
    }
}
