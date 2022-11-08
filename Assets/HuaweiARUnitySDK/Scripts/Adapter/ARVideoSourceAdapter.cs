namespace HuaweiARInternal
{

    using System;
    using System.Runtime.InteropServices;
    using HuaweiARUnitySDK;
    using UnityEngine;

    internal class ARVideoSourceAdapter
    {
        private NDKSession m_ndkSession;

        public ARVideoSourceAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }


        public void Constructor(IntPtr sessionHandle, IntPtr configHandle, String videoPath)
        {

            NDKAPI.HwArVideoSource_Constructor(ARUnityHelper.Instance.GetJEnv(), ARUnityHelper.Instance.GetActivityHandle(), videoPath, sessionHandle, configHandle);
        }
        public void InitPlayer()
        {
            NDKAPI.HwArVideoSource_InitPlayer(ARUnityHelper.Instance.GetJEnv(), ARUnityHelper.Instance.GetActivityHandle());
        }
        public int GetVideoWidth()
        {
            return NDKAPI.HwArVideoSource_GetVideoWidth(ARUnityHelper.Instance.GetJEnv(), ARUnityHelper.Instance.GetActivityHandle());
        }

        public int GetVideoHeight()
        {
            return NDKAPI.HwArVideoSource_GetVideoHeight(ARUnityHelper.Instance.GetJEnv(), ARUnityHelper.Instance.GetActivityHandle());
        }

        //public void StartVideoPlay(IntPtr sessionHandle, IntPtr configHandle, String videoPath)
        public void StartVideoPlay()
        {
            //byte[] pathByteArray = System.Text.Encoding.Default.GetBytes(videoPath);
            //NDKAPI.HwArVideoSource_StartVideoPlayer(ARUnityHelper.Instance.GetJEnv(), ARUnityHelper.Instance.GetActivityHandle(), pathByteArray, sessionHandle, configHandle);
            NDKAPI.HwArVideoSource_StartVideoPlayer(ARUnityHelper.Instance.GetJEnv(), ARUnityHelper.Instance.GetActivityHandle());
        }
        public void StopVideoPlay()
        {
            NDKAPI.HwArVideoSource_StopVideoPlayer(ARUnityHelper.Instance.GetJEnv(), ARUnityHelper.Instance.GetActivityHandle());
        }
        public void PauseVideoPlay()
        {
            NDKAPI.HwArVideoSource_PauseVideoPlayer(ARUnityHelper.Instance.GetJEnv(), ARUnityHelper.Instance.GetActivityHandle());
        }
        public void ResumeVideoPlay(IntPtr sessionHandle, IntPtr configHandle)
        {
            NDKAPI.HwArVideoSource_ResumeVideoPlayer(ARUnityHelper.Instance.GetJEnv(), ARUnityHelper.Instance.GetActivityHandle(), sessionHandle, configHandle);
        }
        public void SetVideoPath(String videoPath)
        {
            byte[] pathByteArray = System.Text.Encoding.Default.GetBytes(videoPath);
            NDKAPI.HwArVideoSource_SetVideoPath(ARUnityHelper.Instance.GetJEnv(), ARUnityHelper.Instance.GetActivityHandle(), pathByteArray);
        }
        public void VideoPlayerRelease()
        {
            NDKAPI.HwArVideoSource_ReleaseVideoPlayer(ARUnityHelper.Instance.GetJEnv(), ARUnityHelper.Instance.GetActivityHandle());
        }
        private struct NDKAPI
        {
     

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArVideoSource_Constructor(IntPtr envHandle, IntPtr appContext, String videoPath, IntPtr sessionHandle, IntPtr configHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArVideoSource_InitPlayer(IntPtr envHandle, IntPtr appContext);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern int HwArVideoSource_GetVideoWidth(IntPtr envHandle, IntPtr appContext);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern int HwArVideoSource_GetVideoHeight(IntPtr envHandle, IntPtr appContext);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArVideoSource_StartVideoPlayer(IntPtr envHandle, IntPtr appContext);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArVideoSource_StopVideoPlayer(IntPtr envHandle, IntPtr appContext);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArVideoSource_PauseVideoPlayer(IntPtr envHandle, IntPtr appContext);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArVideoSource_ResumeVideoPlayer(IntPtr envHandle, IntPtr appContext, IntPtr sessionHandle, IntPtr configHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArVideoSource_SetVideoPath(IntPtr envHandle, IntPtr appContext, byte[] videoPath);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArVideoSource_ReleaseVideoPlayer(IntPtr envHandle, IntPtr appContext);

        }
    }

}
