using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.Domain
{
    /// <summary>
    /// Class Base chứa thông tin chung của 2 entity
    /// </summary>
    /// AUTHOR: DTTHANH (10/09/2023)
    public abstract class BaseEntity
    {
        /// <summary>
        /// Người tạo
        /// </summary>
        /// AUTHOR: DTTHANH (10/09/2023)
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// AUTHOR: DTTHANH (10/09/2023)
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người chỉnh sửa
        /// </summary>
        /// AUTHOR: DTTHANH (10/09/2023)
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        /// AUTHOR: DTTHANH (10/09/2023)
        public DateTime ModifiedDate { get; set; }
    }

}
