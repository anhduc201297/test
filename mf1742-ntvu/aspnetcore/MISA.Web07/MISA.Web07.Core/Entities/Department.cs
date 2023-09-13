using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.Core.Entities
{
    public class Department : BaseEntity

    {
        #region Constructor
        public Department() { }
        #endregion

        #region Property

        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public string DepartmentCode { get; set; }
        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName { get; set; }

        ///// <summary>
        ///// Những nhân viên thuộc đơn vị
        ///// </summary>
        //public List<Employee> Employees { get; set; }
        #endregion
    }
}
