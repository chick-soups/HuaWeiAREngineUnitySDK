namespace HuaweiARInternal
{
    using System;
    using System.Runtime.InteropServices;
    using HuaweiARUnitySDK;

    internal class HuaweiArApkAdapter
    {
        public ARAvailability CheckAvailability()
        {
            int availability = 0;
            NDKAPI.HwArEnginesApk_checkAvailability(ARUnityHelper.Instance.GetJEnv(),
                ARUnityHelper.Instance.GetActivityHandle(), ref availability);
            return (ARAvailability)availability;
        }
       
        public ARInstallStatus RequestInstall(bool userRequestedInstall)
        {
            int installState = 0;
            //NDKARStatus status = NDKAPI.HwArApk_requestInstall(ARUnityHelper.Instance.GetJEnv(),
            //    ARUnityHelper.Instance.GetActivityHandle(), userRequestedInstall, ref installState);
            //ARExceptionAdapter.ExtractException(status);
            return (ARInstallStatus)installState;
        }

        public Boolean IsAREngineApkReady()
        {
            return NDKAPI.HwArEnginesApk_isAREngineApkReady(ARUnityHelper.Instance.GetJEnv(),
                ARUnityHelper.Instance.GetActivityHandle());
        }

        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArEnginesApk_checkAvailability(IntPtr env,
                                IntPtr application_context,
                                ref int out_availability);
            [Obsolete]
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArEnginesApk_requestInstall(IntPtr env,
                                                IntPtr application_activity,
                                                bool user_requested_install,
                                                ref int out_install_status);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern Boolean HwArEnginesApk_isAREngineApkReady(IntPtr env,
                                                IntPtr application_activity);
        }
    }
}
