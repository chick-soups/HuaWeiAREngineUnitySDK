namespace HuaweiARInternal
{
    using System;
    using System.Runtime.InteropServices;
    using HuaweiARUnitySDK;
    using UnityEngine;

    internal class ARCameraIntrinsicsAdapter
    {
        private NDKSession m_ndkSession;

        public ARCameraIntrinsicsAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public IntPtr Create()
        {
            IntPtr cameraIntrinsicsHandle = IntPtr.Zero;
            NDKAPI.HwArCameraIntrinsics_create(m_ndkSession.SessionHandle, ref cameraIntrinsicsHandle);
            return cameraIntrinsicsHandle;
        }

        public void Destory(IntPtr cameraIntrinsicsHandle)
        {
            NDKAPI.HwArCameraIntrinsics_destroy(cameraIntrinsicsHandle);
        }

        public Vector2 GeFocalLength(IntPtr cameraIntrinsicsHandle)
        {
            Vector2 focalLength=Vector2.zero;
            NDKAPI.HwArCameraIntrinsics_getFocalLength(m_ndkSession.SessionHandle, cameraIntrinsicsHandle, ref focalLength.x, ref focalLength.y);
            return focalLength;
        }

        public Vector2 GetPrincipalPoint(IntPtr cameraIntrinsicsHandle)
        {
            Vector2 principalPoint=Vector2.zero;
            NDKAPI.HwArCameraIntrinsics_getPrincipalPoint(m_ndkSession.SessionHandle, cameraIntrinsicsHandle, ref principalPoint.x, ref principalPoint.y);
            return principalPoint;
        }

         public Vector2Int GetImageDimensions(IntPtr cameraIntrinsicsHandle)
        {
            int x=0;
            int y=0;
            NDKAPI.HwArCameraIntrinsics_getImageDimensions(m_ndkSession.SessionHandle, cameraIntrinsicsHandle, ref x, ref y);
            return new Vector2Int(x,y);
        }


        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCameraIntrinsics_create(IntPtr sessionHandle, ref IntPtr intrinsics);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCameraIntrinsics_destroy(IntPtr intrinsics);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCameraIntrinsics_getFocalLength(IntPtr sessionHandle, IntPtr intrinsics,
                ref float focalX, ref float focalY);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCameraIntrinsics_getPrincipalPoint(IntPtr sessionHandle, IntPtr intrinsics,
                ref float principalX, ref float principalY);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCameraIntrinsics_getImageDimensions(IntPtr sessionHandle, IntPtr intrinsics,
                ref int width, ref int height);

        }
    }
}

