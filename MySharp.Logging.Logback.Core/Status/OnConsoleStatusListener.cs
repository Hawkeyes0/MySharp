using System;
using System.IO;

namespace MySharp.Logging.Logback.Core.Status
{
    public class OnConsoleStatusListener : OnPrintStreamStatusListenerBase
    {
        protected override TextWriter GetWriter()
        {
            return Console.Out;
        }
    }
}