using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MySharp.Logging.Logback.Core.Helpers
{
    public static class ExceptionToStringArray
    {
        internal static string[] Convert(Exception ex)
        {
            List<string> strList = new List<string>();
            Extract(strList, ex, null);
            return strList.ToArray();
        }

        private static void Extract(List<string> strList, Exception ex, StackFrame[] parentFrames)
        {
            StackFrame[] frames = new StackTrace(ex).GetFrames();
            int numberOfCommonFrames = FindNumberOfCommonFrames(frames, parentFrames);

            strList.Add(FormatFirstLine(ex, parentFrames));
            Debug.Assert(frames != null, nameof(frames) + " != null");
            for (int i = 0; i < frames.Length - numberOfCommonFrames; i++)
            {
                strList.Add($"\tat {frames[i]}");
            }

            if (numberOfCommonFrames != 0)
            {
                strList.Add($"\t... {numberOfCommonFrames} common frames omitted");
            }

            Exception inner = ex.InnerException;
            if (inner != null)
            {
                Extract(strList, inner, frames);
            }
        }

        private static string FormatFirstLine(Exception exception, StackFrame[] parentFrames)
        {
            string prefix = "";
            if (parentFrames != null)
            {
                prefix = CoreConstants.CauseBy;
            }

            string result = prefix + exception.GetType().Name;
            if (string.IsNullOrEmpty(exception.Message))
            {
                result += $": {exception.Message}";
            }

            return result;
        }

        private static int FindNumberOfCommonFrames(StackFrame[] frames, StackFrame[] parentFrames)
        {
            if (parentFrames == null)
                return 0;

            int fidx = frames.Length - 1;
            int pidx = parentFrames.Length - 1;
            int count = 0;
            while (fidx>=0&&pidx>=0)
            {
                if (frames[fidx].Equals(parentFrames[pidx]))
                    count++;
                else
                    break;
                fidx--;
                pidx--;
            }

            return count;
        }
    }
}