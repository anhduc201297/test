using MISA.Web07.Core.MAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.Web07.Core.Entities
{
    public abstract class BaseEntity
    {
        #region Fields

        /// <summary>
        /// Ngày tạo bản ghi
        /// </summary>
        [PropertyName("Ngày tạo")]
        [NotEmpty]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Người tạo
        /// </summary>
        [MaxLength(255)]
        [NotEmpty]
        [PropertyName("Người tạo")]
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Ngày sửa đổi
        /// </summary>
        [PropertyName("Ngày sửa đổi")]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        [MaxLength(255)]
        [PropertyName("Người sửa đổi")]
        public string? ModifiedBy { get; set; }
        #endregion

        #region Methods

        #endregion
    }
}
