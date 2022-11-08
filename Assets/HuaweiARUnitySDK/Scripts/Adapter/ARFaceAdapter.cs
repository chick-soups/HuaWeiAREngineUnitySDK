namespace HuaweiARInternal
{
    using System.Runtime.InteropServices;
    using UnityEngine;
    using System;

    internal class ARFaceAdapter
    {
        private NDKSession m_ndkSession;

        public ARFaceAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public Pose GetPose(IntPtr faceHandle)
        {
            IntPtr poseHandle = m_ndkSession.PoseAdapter.Create();
            NDKAPI.HwArFace_getPose(m_ndkSession.SessionHandle, faceHandle, poseHandle);
            Pose p = m_ndkSession.PoseAdapter.GetPoseValue(poseHandle);
            //set z to negative since GetPoseValue makes z negative
            p.position.z = -p.position.z;
            m_ndkSession.PoseAdapter.Destroy(poseHandle);
            return p;
        }

        public IntPtr AcquireGeometry(IntPtr faceHandle)
        {
            IntPtr geometry = IntPtr.Zero;
            NDKAPI.HwArFace_acquireGeometry(m_ndkSession.SessionHandle, faceHandle, ref geometry);
            return geometry;
        }
        public IntPtr AcquireBlendShape(IntPtr faceHandle)
        {
            IntPtr blendshape = IntPtr.Zero;
            NDKAPI.HwArFace_acquireBlendShapes(m_ndkSession.SessionHandle, faceHandle, ref blendshape);
            return blendshape;
        }


        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFace_getPose(IntPtr sessionHandle, IntPtr faceHandle, IntPtr poseHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFace_acquireGeometry(IntPtr sessionHandle, IntPtr faceHandle, ref IntPtr geometryHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFace_acquireBlendShapes(IntPtr sessionHandle, IntPtr faceHandle, ref IntPtr blendshapesHandle);
        }
    }
}
