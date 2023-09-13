using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.Core.Exceptions
{
    public class MISAUpdateException : MISAException
    {
        #region Constructor

        public MISAUpdateException(string msg) : base(msg)
        {
        }
        #endregion
    }
}
