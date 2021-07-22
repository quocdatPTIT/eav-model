using System.Linq;

namespace DynamicField.ExtentionMethods
{
    public static class StringExtentions
    {
        public static string LowerFirstLetter(this string str)
        {
            return str.First().ToString().ToLower() + str[1..];
        }
    }
}