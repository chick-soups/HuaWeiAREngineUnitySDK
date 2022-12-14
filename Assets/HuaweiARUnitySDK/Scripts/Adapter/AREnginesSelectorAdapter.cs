namespace HuaweiARInternal
{
    using System;
    using System.Runtime.InteropServices;
    using HuaweiARUnitySDK;

    internal class AREnginesSelectorAdapter
    {
        [Obsolete]
        public AREnginesAvaliblity CheckDeviceExecuteAbility()
        {
            return NDKAPI.HwArEnginesSelector_checkAllAvailableEngines(ARUnityHelper.Instance.GetJEnv(),
                ARUnityHelper.Instance.GetActivityHandle());
        }
        [Obsolete]
        public AREnginesType SetAREngine(AREnginesType executor)
        {
            return NDKAPI.HwArEnginesSelector_setAREngine(executor);
        }
        [Obsolete]
        public AREnginesType GetCreatedEngine()
        {
            return NDKAPI.HwArEnginesSelector_getCreatedEngine();
        }


        private struct NDKAPI
        {
            [Obsolete]
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern AREnginesAvaliblity HwArEnginesSelector_checkAllAvailableEngines(IntPtr envHandle, IntPtr applicationContextHandle);
            [Obsolete]
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern AREnginesType HwArEnginesSelector_setAREngine(AREnginesType executerType);
            [Obsolete]
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern AREnginesType HwArEnginesSelector_getCreatedEngine();
        }
    }
}
