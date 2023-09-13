using MISA.Web07.Core.Entities;
using MISA.Web07.Core.MAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.Core.ViewModels
{
    public class EmployeeViewModel : Employee
    {
        [PropertyName("Tên đơn vị")]
        [MaxLength(255)]
        public string DepartmentName { get; set; } = string.Empty;

        [PropertyName("Mã đơn vị")]
        [MaxLength(20)]
        public string DepartmentCode { get; set; } = string.Empty;
    }
}
