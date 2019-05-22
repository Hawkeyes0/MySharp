using System;

namespace MySharp.Logging.Slf4net.Spi
{
    public interface SlfServiceProvider
    {
        /// <summary>
        /// 返回接口<see cref="ILoggerFactory"/>的实例，它由<see cref="LoggerFactory"/>负责绑定。
        /// </summary>
        /// <returns>返回接口<see cref="ILoggerFactory"/>的实例，它由<see cref="LoggerFactory"/>负责绑定。</returns>
        ILoggerFactory LoggerFactory { get; }

        /// <summary>
        /// 返回接口<see cref="IMarkerFactory"/>的实例，它由<see cref="MarkerFactory"/>负责绑定。
        /// </summary>
        /// <returns>返回接口<see cref="IMarkerFactory"/>的实例，它由<see cref="MarkerFactory"/>负责绑定。</returns>
        IMarkerFactory MarkerFactory { get; }

        MDCAdapter MdcAdapter { get; }

        Version RequestedApiVersion { get; }

        void Initialize();
    }
}