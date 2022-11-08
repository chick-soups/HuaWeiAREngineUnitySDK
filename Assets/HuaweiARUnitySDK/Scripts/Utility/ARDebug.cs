//#define __DEBUG__
//#define __USR_ANDROID_LOG__
namespace HuaweiARInternal
{
    using System;
    using UnityEngine;

    public class ARDebug
    {
        private static AndroidJavaClass AndroidUtilLog = new AndroidJavaClass("android.util.Log");

        private static string DefaultLogTag = "arengine_unity";

        public static void LogInfo(UnityEngine.Object message)
        {
#if __DEBUG__
#if __USR_ANDROID_LOG__
            AndroidUtilLog.CallStatic<int>("i", DefaultLogTag, message);
#else
            Debug.Log(message);
#endif
#endif
        }
        public static void LogInfo(string format, params object[] args)
        {
#if __DEBUG__

#if __USR_ANDROID_LOG__
            AndroidUtilLog.CallStatic<int>("i", DefaultLogTag, String.Format(format, args));
#else
            Debug.LogFormat(format,args);
#endif

#endif
        }

        public static void LogInfo(string tag, string format, params object[] args)
        {
#if __DEBUG__
#if __USR_ANDROID_LOG__
            AndroidUtilLog.CallStatic<int>("i", tag, String.Format(format, args));
#else
            Debug.LogFormat(format,args);
#endif
#endif
        }

        public static void LogWarning(UnityEngine.Object message)
        {
#if __USR_ANDROID_LOG__
            AndroidUtilLog.CallStatic<int>("w", DefaultLogTag, message);
#else
            Debug.LogWarning(message);
#endif

        }
        public static void LogWarning(string format, params object[] args)
        {
#if __USR_ANDROID_LOG__
            AndroidUtilLog.CallStatic<int>("w", DefaultLogTag, String.Format(format, args));
#else
            Debug.LogWarningFormat(format, args);
#endif

        }

        public static void LogError(UnityEngine.Object message)
        {
#if __USR_ANDROID_LOG__
            AndroidUtilLog.CallStatic<int>("e", DefaultLogTag, message);
#else
            Debug.LogError(message);
#endif

        }
        public static void LogError(string format, params object[] args)
        {
#if __USR_ANDROID_LOG__
            AndroidUtilLog.CallStatic<int>("e", DefaultLogTag, String.Format(format, args));
#else
            Debug.LogErrorFormat(format, args);
#endif

        }

    }
}
