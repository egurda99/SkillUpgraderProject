using System.Diagnostics;

namespace Tools.Logging
{
    public static partial class Log
    {
        private static readonly Logger DefaultLogger = new("LOGGER");

        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        [Conditional("LOGS_ENABLED")]
        public static void Info(object msg)
        {
            DefaultLogger.Info(msg);
        }

        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        [Conditional("LOGS_ENABLED")]
        public static void Warning(object msg)
        {
            DefaultLogger.Warning(msg);
        }

        public static void Error(object msg)
        {
            DefaultLogger.Error(msg);
        }
    }
}
