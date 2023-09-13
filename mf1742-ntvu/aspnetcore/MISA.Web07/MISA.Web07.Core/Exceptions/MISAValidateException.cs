using System.Collections;

namespace MISA.Web07.Core.Exceptions
{
    public class MISAValidateException : MISAException
    {
        #region Fields

        #pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public Dictionary<string, string> Data { get; set; }
        #pragma warning restore CS0114 // Member hides inherited member; missing override keyword

        #endregion

        #region Constructor
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public MISAValidateException(string msg) : base(msg) { }
        
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.



        #endregion

        #region Methods

        #endregion
    }
}
