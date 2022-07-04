using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BookCore.Dtos.Book
{
    /// <summary>
    /// https://stackoverflow.com/questions/27372816/how-to-read-the-value-for-an-enummember-attribute
    /// </summary>
    public enum BookStatus
    {
        [EnumMember(Value ="In Stock")]
        InStock,
        [EnumMember(Value = "Out of Stock")]
        OutOfStock
    }

    public static class Extension
    {
        public static string ToEnumMemberAttrValue(this Enum @enum)
        {
            var attr =
                @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?.
                    GetCustomAttributes(false).OfType<EnumMemberAttribute>().
                    FirstOrDefault();
            if (attr == null)
                return @enum.ToString();
            return attr.Value;
        }
    }
}
