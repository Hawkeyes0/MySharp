using System;
using System.Collections.Generic;
using MySharp.Logging.Slf4net.Helpers;
using MySharp.Logging.Slf4net.Spi;

namespace MySharp.Logging.Slf4net
{
    public static class LoggerFactory
    {
        private const int Uninitialized = 0;
        private const int Initializing = 1;
        private const int FailedInitialization = 2;
        private const int SuccessfulInitialization = 3;
        private const int NopFallbackInitialization = 4;

        private static volatile int _initializationState = Uninitialized;
        private static readonly SubstituteServiceProvider SubstProvider = new SubstituteServiceProvider();
        private static readonly NopServiceProvider NopFallbackProvider = new NopServiceProvider();

        private static readonly Version[] ApiCompatibilityList = { Version.Parse("1.0"), Version.Parse("1.1") };

        private static volatile SlfServiceProvider _provider;

        private static readonly object InitLock = new object();

        private static void FailedBinding(Exception e)
        {
            _initializationState = FailedInitialization;
            Util.Report("Failed to instantiate LoggerFactory.", e);
        }

        private static void FixSubstituteLoggers()
        {
            lock (SubstProvider)
            {
                ((SubstituteLoggerFactory)SubstProvider.LoggerFactory).PostInitialization();
                foreach (SubstituteLogger substLogger in ((SubstituteLoggerFactory)SubstProvider.LoggerFactory).GetLoggers())
                {
                    Logger logger = GetLogger(substLogger.Name);
                    substLogger.SetDelegate(logger);
                }
            }
        }

        static void Bind()
        {
            try
            {
                List<SlfServiceProvider> providersList = ServiceLoader<SlfServiceProvider>.Load();
                if (providersList != null && providersList.Count > 1)
                {
                    _provider = providersList[0];
                    _provider.Initialize();
                    _initializationState = SuccessfulInitialization;
                    FixSubstituteLoggers();
                    ((SubstituteLoggerFactory)SubstProvider.LoggerFactory).Clear();
                }
                else
                {
                    _initializationState = NopFallbackInitialization;
                }
            }
            catch (Exception e)
            {
                FailedBinding(e);
                throw new Exception("Unexpected initialization failure.", e);
            }
        }

        private static void VersionSanityCheck()
        {
            Version requested = _provider.RequestedApiVersion;

            bool isMatch = false;
            foreach (Version version in ApiCompatibilityList)
            {
                if (requested >= version)
                    isMatch = true;
            }

            if (!isMatch)
            {
                Util.Report($"The requested version {requested} is not compatible.");
            }
        }

        private static void Initialize()
        {
            Bind();
            if (_initializationState == SuccessfulInitialization)
                VersionSanityCheck();
        }

        public static ILoggerFactory GetLoggerFactory()
        {
            return GetProvider().LoggerFactory;
        }

        public static Logger GetLogger(string name)
        {
            ILoggerFactory factory = GetLoggerFactory();
            return factory.GetLogger(name);
        }

        public static Logger GetLogger(Type type)
        {
            Logger logger = GetLogger(type.FullName);
            return logger;
        }

        internal static SlfServiceProvider GetProvider()
        {
            if (_initializationState == Uninitialized)
            {
                lock (InitLock)
                {
                    if (_initializationState == Uninitialized)
                    {
                        _initializationState = Initializing;
                        Initialize();
                    }
                }
            }

            switch (_initializationState)
            {
                case SuccessfulInitialization:
                    return _provider;
                case NopFallbackInitialization:
                    return NopFallbackProvider;
                case FailedInitialization:
                    throw new OperationCanceledException("LoggerFactory initialize failed.");
                case Initializing:
                    return SubstProvider;
            }
            throw new Exception("unreachable code");
        }
    }
}
