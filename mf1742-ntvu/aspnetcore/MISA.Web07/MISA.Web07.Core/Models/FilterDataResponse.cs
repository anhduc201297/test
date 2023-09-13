using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.Core.Models
{
    public class FilterDataResponse<TViewModel>
    {
        public int TotalRecords { get; set; }
        public IEnumerable<TViewModel>? Data { get; set; }
    }
}
