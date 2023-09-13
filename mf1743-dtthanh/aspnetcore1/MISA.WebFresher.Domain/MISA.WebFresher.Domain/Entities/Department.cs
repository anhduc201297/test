using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.Domain
{
    public class Department : BaseEntity
    {
        /// <summary>
        /// ID đơn vị
        /// </summary>
        /// AUTHOR: DTTHANH (10/09/2023)
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Tên đơn vị 
        /// </summary>
        /// AUTHOR: DTTHANH (10/09/2023)
        public string DepartmentName { get; set; }
    }
}
