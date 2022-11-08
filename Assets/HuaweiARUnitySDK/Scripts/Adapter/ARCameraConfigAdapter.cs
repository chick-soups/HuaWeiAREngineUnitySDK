namespace HuaweiARInternal
{
    using System;
    using System.Runtime.InteropServices;
    using HuaweiARUnitySDK;
    using UnityEngine;

    internal class ARCameraConfigAdapter
    {
        private NDKSession m_ndkSession;

        public ARCameraConfigAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public IntPtr Create()
        {
            IntPtr cameraConfigHandle = IntPtr.Zero;
            NDKAPI.HwArCameraConfig_create(m_ndkSession.SessionHandle, ref cameraConfigHandle);
            return cameraConfigHandle;
        }

        public void Destory(IntPtr cameraConfigHandle)
        {
            NDKAPI.HwArCameraConfig_destroy(cameraConfigHandle);
        }

        public Vector2Int GetImageDimensions(IntPtr cameraConfigHandle)
        {
            int width = 0;
            int height = 0;
            NDKAPI.HwArCameraConfig_getImageDimensions(m_ndkSession.SessionHandle, cameraConfigHandle, ref width, ref height);
            return new Vector2Int(width, height);
        }

        public Vector2Int GetTextureDimensions(IntPtr cameraConfigHandle)
        {
            int width = 0;
            int height = 0;
            NDKAPI.HwArCameraConfig_getTextureDimensions(m_ndkSession.SessionHandle, cameraConfigHandle, ref width, ref height);
            return new Vector2Int(width, height);
        }


        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCameraConfig_create(IntPtr sessionHandle, ref IntPtr cameraConfigHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCameraConfig_destroy(IntPtr cameraConfigHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCameraConfig_getImageDimensions(IntPtr sessionHandle, IntPtr cameraConfigHandle,
                ref int outWidth, ref int outHeight);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCameraConfig_getTextureDimensions(IntPtr sessionHandle, IntPtr cameraConfigHandle,
                ref int outWidth, ref int outHeight);

        }
    }
}
