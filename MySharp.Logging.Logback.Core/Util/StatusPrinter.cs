using System;
using System.Collections.Generic;
using System.Text;
using MySharp.Logging.Logback.Core.Helpers;
using MySharp.Logging.Logback.Core.Status;

namespace MySharp.Logging.Logback.Core.Util
{
    public class StatusPrinter
    {
        private static string format = "HH:mm:ss,SSS";

        public static void BuildStr(StringBuilder sb, string indentation, IStatus s)
        {
            string prefix;
            if (s.HasChildren)
            {
                prefix = indentation + "+ ";
            }
            else
            {
                prefix = indentation + "|-";
            }

            if (format != null)
            {
                string dateStr = s.Date.ToString(format);
                sb.Append(dateStr).Append(" ");
            }
            sb.Append(prefix).Append(s).AppendLine();

            if (s.Exception != null)
            {
                AppendThrowable(sb, s.Exception);
            }
            if (s.HasChildren)
            {
                using (IEnumerator<IStatus> ite = s.GetEnumerator())
                    while (ite.MoveNext())
                    {
                        IStatus child = ite.Current;
                        BuildStr(sb, indentation + "  ", child);
                    }
            }
        }

        private static void AppendThrowable(StringBuilder sb, Exception ex)
        {
            string[] stringRep = ExceptionToStringArray.Convert(ex);

            foreach (string s in stringRep)
            {
                if (s.StartsWith(CoreConstants.CauseBy))
                {
                    // nothing
                }
                else if (char.IsDigit(s[0]))
                {
                    // if line resembles "48 common frames omitted"
                    sb.Append("\t... ");
                }
                else
                {
                    // most of the time. just add a tab+"at"
                    sb.Append("\tat ");
                }
                sb.Append(s).AppendLine();
            }
        }
    }
}