
namespace HuaweiARInternal
{
    using System.Runtime.InteropServices;
    using UnityEngine;
    using System;
    internal class ARLightEstimateAdapter
    {

        private NDKSession m_ndkSession;

        public ARLightEstimateAdapter(NDKSession Session)
        {
            m_ndkSession = Session;
        }

        public IntPtr Create()
        {
            IntPtr lightEstimateHandle = IntPtr.Zero;
            NDKAPI.HwArLightEstimate_create(m_ndkSession.SessionHandle, ref lightEstimateHandle);
            return lightEstimateHandle;
        }

        public void Destroy(IntPtr lightEstimateHandle)
        {
            NDKAPI.HwArLightEstimate_destroy(lightEstimateHandle);
        }

        public bool GetState(IntPtr lightEstimateHandle)
        {
            int state = 0;
            NDKAPI.HwArLightEstimate_getState(m_ndkSession.SessionHandle, lightEstimateHandle, ref state);
            return state == 1 ? true : false;
        }

        public float GetPixelIntensity(IntPtr lightEstimateHandle)
        {
            float pixelIntensity = 0;
            NDKAPI.HwArLightEstimate_getPixelIntensity(m_ndkSession.SessionHandle,
                lightEstimateHandle, ref pixelIntensity);
            return pixelIntensity;
        }

        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArLightEstimate_create(IntPtr sessionHandle, ref IntPtr outLightEstimateHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArLightEstimate_destroy(IntPtr lightEstimateHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArLightEstimate_getState(IntPtr sessionHandle, IntPtr lightEstimateHandle,
                                ref int LightEstimateState);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArLightEstimate_getPixelIntensity(IntPtr sessionHandle, IntPtr lightEstimateHandle,
                                         ref float outPixelIntensity);
        }
    }
}
