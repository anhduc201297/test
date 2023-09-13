using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.Core.Exceptions
{
    public class ErrorService
    {
        #region Fields

        /// <summary>
        /// Định danh mã lỗi nội bộ
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Chi tiết về mã lỗi
        /// </summary>
        public string MoreInfo { get; set; } = string.Empty;

        /// <summary>
        /// Mã dẫn  
        /// </summary>
        public string TraceId { get; set; } = string.Empty;

        /// <summary>
        /// Thông tin cho dev
        /// </summary>
        public string DevMsg { get; set; } = string.Empty;
        /// <summary>
        /// Thông tin báo cho người dùng
        /// </summary>
        public string UserMsg { get; set; } = string.Empty;
        /// <summary>
        /// Data
        /// </summary>
        public dynamic? Data { get; set; }
    
        #endregion

    }
}
