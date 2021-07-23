using System.Linq;

namespace DynamicField.ExtentionMethods
{
    public static class StringExtentions
    {
        public static string LowerFirstLetter(this string str)
        {
            return str.First().ToString().ToLower() + str[1..];
        }

        public static string RemoveUnicode(this string str)
        {
            var vietnamChar = new[]
            {
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ"
            };

            for (int i = 1; i < vietnamChar.Length; i++)
            {
                for (int j = 0; j < vietnamChar[i].Length; j++)
                {
                    str = str.Replace(vietnamChar[i][j], vietnamChar[0][i - 1]);
                }
            }

            var strSplit = str.Split(' ');
            
            for (var i = 0; i < strSplit.Length; i++)
            {
                strSplit[i] = strSplit[i].First().ToString().ToUpper() + strSplit[i][1..];
            }

            return string.Join("", strSplit);
        }
    }
}