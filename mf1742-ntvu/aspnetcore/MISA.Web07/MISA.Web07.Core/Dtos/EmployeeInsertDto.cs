using MISA.Web07.Core.Entities;
using MISA.Web07.Core.MAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.Core.Dtos
{
    public class EmployeeInsertDto : Employee
    {
        #region Fields
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [NotEmpty]
        [MaxLength(255)]
        [PropertyName("Tên đơn vị")]
        public string DepartmentName { get; set; } = string.Empty;
        #endregion
    }
}
