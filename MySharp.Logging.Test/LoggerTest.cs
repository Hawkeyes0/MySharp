using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MySharp.Logging.Test
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void TestLogging()
        {
            Logger logger = LoggerFactory.GetLogger(GetType());

            logger.Debug("this is a debug");
            logger.Trace("this is a trace");
            logger.Info("this is a info");
            logger.Warning("this is a warning");
            logger.Error("this is an error");
        }
    }
}
