using MISA.Web07.Core.Interfaces.MAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Web07.Core.MAttributes
{
  
    /// <summary>
    /// Thuộc tính Tên hiển thị
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyName : Attribute
    {
        public string Name = string.Empty;
        public PropertyName(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// Thuộc tính yêu cầu không được rỗng
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmpty : Attribute, IRuleAttributes
    {
        public string GetErrorMessage(string fieldName)
        {
            return $"{fieldName} {Resources.ResourceVN.Error_Empty}";
        }

        public bool Validate(string value)
        {

            if (string.IsNullOrEmpty(value)) return false;
           
            
            return !string.IsNullOrEmpty(value.Trim());
        }
    }


    /// <summary>
    /// Thuộc tính yêu cầu độ dài tối đa
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLength : Attribute, IRuleAttributes
    {
        public int Length = int.MinValue;
        public MaxLength(int length)
        {
            Length = length;
        }

        public bool Validate(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            return value.Length < Length;
        }
        public string GetErrorMessage(string fieldName)
        {
            return $"{fieldName} {Resources.ResourceVN.Error_MaxLength} {Length}.";
        }

    }


    /// <summary>
    /// Thuộc tính yêu cầu độ dài tối thiểu
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MinLength : Attribute, IRuleAttributes
    {
        public int Length = int.MaxValue;
        public MinLength(int len)
        {
            Length = len;
        }

        public bool Validate(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            return value.Length > Length;


        }
        public string GetErrorMessage(string fieldName)
        {
            return $"{fieldName} {Resources.ResourceVN.Error_MinLength} {Length}.";
        }
    }


    /// <summary>
    /// Thuộc tính yêu cầu độ dài nằm trong khoảng
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class InRange : Attribute, IRuleAttributes
    {
        public int Max = int.MaxValue;
        public int Min = int.MinValue;
        public InRange(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public bool Validate(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            try
            {
                int number = int.Parse(value);
                return number >= Min && number <= Max;
            }
            catch (FormatException)
            {
                return false;
            }


        }
        public string GetErrorMessage(string fieldName)
        {
            return $"{fieldName} {Resources.ResourceVN.Error_MinLength} [{Min}, {Max}].";
        }
    }


    /// <summary>
    /// Thuộc tính yêu cầu thời gian sớm hơn thời gian hiện tại
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EarlierNow : Attribute, IRuleAttributes
    {
        public bool Validate(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
          
            try
            {
                DateTime dateTime = DateTime.Parse(value);
                return (dateTime < DateTime.Now);
            }
            catch (FormatException)
            {
                return false;
            }
           
        }
        public string GetErrorMessage(string fieldName)
        {
            return $"{fieldName} {Resources.ResourceVN.Error_DateEarly}";
        }
    }


    /// <summary>
    /// Thuộc tính yêu cầu định dạng dữ liệu
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FormatType : Attribute, IRuleAttributes
    {

        public string Pattern = string.Empty;
        public FormatType(string regex)
        {
            if(regex.ToLower() == "email")
            {
                Pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            }
            else
            {
            Pattern = regex;
            }
        }

        public bool Validate(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            return Regex.IsMatch(value, Pattern);
        }
        public string GetErrorMessage(string fieldName)
        {
            return $"{fieldName} {Resources.ResourceVN.Error_Format}";
        }
    }

}
