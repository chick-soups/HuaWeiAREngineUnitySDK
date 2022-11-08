namespace HuaweiARInternal
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using HuaweiARUnitySDK;

    class ARPointAdapter
    {
        private NDKSession m_ndkSession;

        public ARPointAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public Pose GetPose(IntPtr pointHandle)
        {
            IntPtr poseHandle = m_ndkSession.PoseAdapter.Create();
            NDKAPI.HwArPoint_getPose(m_ndkSession.SessionHandle, pointHandle, poseHandle);
            Pose pose = m_ndkSession.PoseAdapter.GetPoseValue(poseHandle);
            m_ndkSession.PoseAdapter.Destroy(poseHandle);
            return pose;
        }

        public ARPoint.OrientationMode GetOrientationMode(IntPtr pointHandle)
        {
            int mode = 0;
            NDKAPI.HwArPoint_getOrientationMode(m_ndkSession.SessionHandle, pointHandle, ref mode);
            return (ARPoint.OrientationMode) mode;
        }

        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPoint_getPose(IntPtr sessionHandle,IntPtr pointHandle,
                       IntPtr outPoseHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPoint_getOrientationMode(IntPtr sessionHandle, IntPtr pointHandle,
                                  ref int out_orientation_mode);
        }
    }
}
