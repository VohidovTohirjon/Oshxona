using System.Text.RegularExpressions;

namespace OshXona.Extension
{
    public static class Capitalize
        {
            static public string UpperCase(this string text) 
            {
                return Regex.Replace(text, "^[a-z]", 
                    m => m.Value.ToUpper());
            }
        }
    }
