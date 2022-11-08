using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiARInternal
{
    using System;
    using System.Runtime.InteropServices;
    using HuaweiARUnitySDK;
    using System.Collections.Generic;
    internal class ARImageAdapter
    {
        private NDKSession m_ndkSession;
        public ARImageAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }
        public void GetImageBuffer(IntPtr imageHandle, out int width, out int height, out IntPtr yPlane,
            out IntPtr uPlane, out IntPtr vPlane, out int yRowStride, out int uvPixelStride, out int uvRowStride)
        {
            IntPtr ndkImageHandle = IntPtr.Zero;
            NDKAPI.HwArImage_getNdkImage(imageHandle, ref ndkImageHandle);

            width = 0;
            NDKAPI.AImage_getWidth(ndkImageHandle, ref width);

            height = 0;
            NDKAPI.AImage_getHeight(ndkImageHandle, ref height);

            const int Y_PLANE = 0;
            const int U_PLANE = 1;
            const int V_PLANE = 2;
            int bufferLength = 0;

            yPlane = IntPtr.Zero;
            NDKAPI.AImage_getPlaneData(ndkImageHandle, Y_PLANE, ref yPlane, ref bufferLength);

            uPlane = IntPtr.Zero;
            NDKAPI.AImage_getPlaneData(ndkImageHandle, U_PLANE, ref uPlane, ref bufferLength);

            vPlane = IntPtr.Zero;
            NDKAPI.AImage_getPlaneData(ndkImageHandle, V_PLANE, ref vPlane, ref bufferLength);

            yRowStride = 0;
            NDKAPI.AImage_getPlaneRowStride(ndkImageHandle, Y_PLANE, ref yRowStride);

            uvPixelStride = 0;
            NDKAPI.AImage_getPlanePixelStride(ndkImageHandle, U_PLANE, ref uvPixelStride);

            uvRowStride = 0;
            NDKAPI.AImage_getPlaneRowStride(ndkImageHandle, U_PLANE, ref uvRowStride);
        }
        public void Release(IntPtr imageHandle)
        {
            //m_NativeSession.MarkHandleReleased(imageHandle);
            NDKAPI.HwArImage_release(imageHandle);
        }
        private struct NDKAPI
        {

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArImage_getNdkImage(IntPtr imageHandle, ref IntPtr ndkImage);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArImage_release(IntPtr imageHandle);

            [DllImport(AdapterConstants.NDKMediaApi)]
            public static extern NdkCameraStatus AImage_getFormat(IntPtr ndkImageHandle,
                 ref int format);
            [DllImport(AdapterConstants.NDKMediaApi)]
            public static extern int AImage_getWidth(IntPtr ndkImageHandle, ref int width);
            [DllImport(AdapterConstants.NDKMediaApi)]
            public static extern int AImage_getHeight(IntPtr ndkImageHandle, ref int width);
            [DllImport(AdapterConstants.NDKMediaApi)]
            public static extern int AImage_getPlaneData(IntPtr imageHandle, int planeIdx, ref IntPtr data,
                ref int dataLength);
            [DllImport(AdapterConstants.NDKMediaApi)]
            public static extern int AImage_getPlanePixelStride(IntPtr imageHandle, int planeIdx, ref int pixelStride);
            [DllImport(AdapterConstants.NDKMediaApi)]
            public static extern int AImage_getPlaneRowStride(IntPtr imageHandle, int planeIdx, ref int rowStride);

        }
    }
}
