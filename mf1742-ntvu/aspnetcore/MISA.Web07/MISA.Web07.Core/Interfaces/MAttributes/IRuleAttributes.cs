using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.Core.Interfaces.MAttributes
{
    public interface IRuleAttributes
    {
        bool Validate(string value);
        string GetErrorMessage(string fieldName);
    }

}
