using MySharp.Logging.Slf4net.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using MySharp.Logging.Slf4net.Impl;

namespace MySharp.Logging.Slf4net
{
    public static class LoggerFactory
    {
        private const int Uninitialized = 0;
        private const int Initializing = 1;
        private const int FailedInitialization = 2;
        private const int SuccessfulInitialization = 3;
        private const int NopFallbackInitialization = 4;

        private static int InitializationState = Uninitialized;
        private static readonly SubstituteLoggerFactory SubstFactory = new SubstituteLoggerFactory();
        private static readonly NopLoggerFactory NopFallbackFactory = new NopLoggerFactory();

        private static readonly Version[] ApiCompatibilityList = new Version[] { Version.Parse("1.0"), Version.Parse("1.1"), };

        private static void FailedBinding(Exception e)
        {
            InitializationState = FailedInitialization;
            Util.Report("Failed to instantiate LoggerFactory.", e);
        }

        private static void FixSubstituteLoggers()
        {
            lock (SubstFactory)
            {
                SubstFactory.PostInitialization();
                foreach (SubstituteLogger substLogger in SubstFactory.GetLoggers())
                {
                    Logger logger = GetLogger(substLogger.Name);
                    substLogger.SetDelegate(logger);
                }
            }
        }

        static void NoType()
        {
            Assembly.GetAssembly(typeof(LoggerFactory)).CreateInstance("");
        }

        static void Bind()
        {
            try
            {
                StaticLoggerBinder.GetSingleton();
                InitializationState = SuccessfulInitialization;
                FixSubstituteLoggers();
                //ReplayEvents();
                SubstFactory.Clear();
            }
            catch (MissingMethodException noMethod)
            {
                string msg = noMethod.Message;
                InitializationState = FailedInitialization;
                Util.Report("Incompatible with binding.");
            }
            catch (Exception e)
            {
                FailedBinding(e);
                throw new Exception("Unexpected initialization failure.", e);
            }
        }

        private static void VersionSanityCheck()
        {
            Version requested = StaticLoggerBinder.RequestApiVersion;

            bool isMatch = false;
            foreach (Version version in ApiCompatibilityList)
            {
                if (requested > version)
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
            if (InitializationState == SuccessfulInitialization)
                VersionSanityCheck();
        }

        public static ILoggerFactory GetLoggerFactory()
        {
            if (InitializationState == Uninitialized)
            {
                Type t = typeof(LoggerFactory);
                lock (t)
                {
                    if (InitializationState == Uninitialized)
                    {
                        InitializationState = Initializing;
                        Initialize();
                    }
                }
            }

            switch (InitializationState)
            {
                case SuccessfulInitialization:
                    return StaticLoggerBinder.Singleton.GetLoggerFactory();
                case NopFallbackInitialization:
                    return NopFallbackFactory;
                case FailedInitialization:
                    throw new OperationCanceledException("LoggerFactory in failed state.");
                case Initializing:
                    return SubstFactory;
            }
            throw new Exception("Unreachable code.");
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
    }
}
