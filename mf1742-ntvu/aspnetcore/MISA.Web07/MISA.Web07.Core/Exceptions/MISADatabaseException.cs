using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.Core.Exceptions
{
    public class MISADatabaseException : MISAException
    {
        #region Constructor
        public MISADatabaseException(string msg) : base(msg)
        {
        } 
        #endregion

    }
}
