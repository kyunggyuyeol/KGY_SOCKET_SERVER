using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Socket_Server
{
    public class Util
    {

        public static string GetMiddleString(string str, string begin, string end)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            if (str.IndexOf(begin) > -1)
            {
                str = str.Substring(str.IndexOf(begin) + begin.Length);
                if (str.IndexOf(end) > -1) result = str.Substring(0, str.IndexOf(end));
                else result = str;
            }
            return result;
        }
    }
}
