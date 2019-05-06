using System;
using System.Collections.Generic;
using System.Text;

namespace MySharp.Logging.Util
{
    public static class LoggerNameUtil
    {
        public static int GetSeparatorIndexOf(string name, int startIndex)
        {
            int dotIndex = name.IndexOf(CoreConstants.Dot, startIndex);
            int dollarIndex = name.IndexOf(CoreConstants.Dollar, startIndex);

            if (dotIndex == -1 && dollarIndex == -1)
                return -1;
            if (dotIndex == -1)
                return dollarIndex;
            if (dollarIndex == -1)
                return dotIndex;

            return Math.Min(dotIndex, dollarIndex);
        }
    }
}
