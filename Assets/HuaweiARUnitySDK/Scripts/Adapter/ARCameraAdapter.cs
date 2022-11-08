namespace HuaweiARInternal
{
    using System;
    using UnityEngine;
    using System.Runtime.InteropServices;
    using HuaweiARUnitySDK;

    internal class ARCameraAdapter
    {
        private NDKSession m_ndkSession;

        public ARCameraAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public ARTrackable.TrackingState GetTrackingState(IntPtr cameraHandle)
        {
            int state = (int)ARTrackable.TrackingState.STOPPED;
            NDKAPI.HwArCamera_getTrackingState(m_ndkSession.SessionHandle, cameraHandle, ref state);
            if (!ValueLegalityChecker.CheckInt("GetTrackingState", state,
                AdapterConstants.Enum_TrackingState_MinIntValue, AdapterConstants.Enum_TrackingState_MaxIntValue))
            {
                return ARTrackable.TrackingState.STOPPED;
            }
            return (ARTrackable.TrackingState)state;
        }

        public Pose GetPose(IntPtr cameraHandle)
        {
            if (cameraHandle == IntPtr.Zero)
            {
                return Pose.identity;
            }

            IntPtr poseHandle = m_ndkSession.PoseAdapter.Create();
            NDKAPI.HwArCamera_getDisplayOrientedPose(m_ndkSession.SessionHandle, cameraHandle,
                poseHandle);
            Pose resultPose = m_ndkSession.PoseAdapter.GetPoseValue(poseHandle);
            m_ndkSession.PoseAdapter.Destroy(poseHandle);
            return resultPose;
        }

        public Matrix4x4 GetProjectionMatrix(IntPtr cameraHandle, float near, float far)
        {
            Matrix4x4 matrix = Matrix4x4.identity;
            NDKAPI.HwArCamera_getProjectionMatrix(m_ndkSession.SessionHandle, cameraHandle,
                near, far, ref matrix);
            return matrix;
        }

        public void Release(IntPtr cameraHandle)
        {
            NDKAPI.HwArCamera_release(cameraHandle);
        }

        private struct NDKAPI
        {
            //this function is useless in unity 
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCamera_getPose(IntPtr sessionHandle, IntPtr cameraHandle, IntPtr outPoseHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCamera_getDisplayOrientedPose(IntPtr sessionHandle, IntPtr cameraHandle,
                                       IntPtr outPoseHandle);
            //this function is unused in unity
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCamera_getViewMatrix(IntPtr sessionHandle, IntPtr cameraHandle,
                              ref Matrix4x4 outColMajor_4x4);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCamera_getTrackingState(IntPtr sessionHandle, IntPtr cameraHandle,
                                 ref int outTrackingState);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCamera_getProjectionMatrix(IntPtr sessionHandle, IntPtr cameraHandle,
                                    float near, float far, ref Matrix4x4 outColMajor_4x4);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCamera_release(IntPtr cameraHandle);

        }
    }
}
