namespace HuaweiARInternal
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using HuaweiARUnitySDK;
    using System.Collections;
    using System.Collections.Generic;

    internal class ARPlaneAdapter
    {
        private const int m_maxPolygonSize = 1024;
        private float[] m_tmpPoints;
        private GCHandle m_tmpPointsHandle;
        NDKSession m_ndkSession;

        public ARPlaneAdapter(NDKSession session)
        {
            m_ndkSession = session;
            m_tmpPoints = new float[m_maxPolygonSize * 2];
            m_tmpPointsHandle = GCHandle.Alloc(m_tmpPoints, GCHandleType.Pinned);
        }
        ~ARPlaneAdapter()
        {
            m_tmpPointsHandle.Free();
        }

        public ARPlane AcquireSubsumedBy(IntPtr planeHandle)
        {
            IntPtr parentHandle = IntPtr.Zero;
            NDKAPI.HwArPlane_acquireSubsumedBy(m_ndkSession.SessionHandle, planeHandle, ref parentHandle);
            return m_ndkSession.TrackableManager.ARTrackableFactory(parentHandle, false) as ARPlane;
        }
        public ARPlane.ARPlaneType GetPlaneType(IntPtr planeHandle)
        {
            int planeType = 0;
            NDKAPI.HwArPlane_getType(m_ndkSession.SessionHandle, planeHandle, ref planeType);
            return (ARPlane.ARPlaneType)planeType;
        }
        public Pose GetCenterPose(IntPtr planeHandle)
        {
            IntPtr poseHandle = m_ndkSession.PoseAdapter.Create();
            NDKAPI.HwArPlane_getCenterPose(m_ndkSession.SessionHandle, planeHandle, poseHandle);
            Pose p = m_ndkSession.PoseAdapter.GetPoseValue(poseHandle);
            m_ndkSession.PoseAdapter.Destroy(poseHandle);
            return p;
        }
        public float GetExtentX(IntPtr planeHandle)
        {
            float f = 0f;
            NDKAPI.HwArPlane_getExtentX(m_ndkSession.SessionHandle, planeHandle, ref f);
            return f;
        }

        public float GetExtentZ(IntPtr planeHandle)
        {
            float f = 0f;
            NDKAPI.HwArPlane_getExtentZ(m_ndkSession.SessionHandle, planeHandle, ref f);
            return f;
        }

        public void GetPlanePolygon(IntPtr planeHandle, List<Vector3> points)
        {
            points.Clear();
            int pointCount = 0;
            NDKAPI.HwArPlane_getPolygonSize(m_ndkSession.SessionHandle, planeHandle, ref pointCount);
            if (pointCount < 1)
            {
                return;
            }
            else if (pointCount > m_maxPolygonSize)
            {
                ARDebug.LogInfo("GetPlanePolygon: plane polygon size exceeds.");
                pointCount = m_maxPolygonSize;
            }
            NDKAPI.HwArPlane_getPolygon(m_ndkSession.SessionHandle, planeHandle, m_tmpPointsHandle.AddrOfPinnedObject());

            for (int i = pointCount - 2; i >= 0; i -= 2)
            {
                var point = new Vector3(m_tmpPoints[i], 0, -m_tmpPoints[i + 1]);
                points.Add(point);
            }
        }

        public bool IsPoseInExtents(IntPtr planeHandle, Pose pose)
        {
            int isPoseInExtents = 0;
            var poseHandle = m_ndkSession.PoseAdapter.Create(pose);
            NDKAPI.HwArPlane_isPoseInExtents(m_ndkSession.SessionHandle, planeHandle, poseHandle, ref isPoseInExtents);
            m_ndkSession.PoseAdapter.Destroy(poseHandle);
            return isPoseInExtents != 0;
        }

        public bool IsPoseInPolygon(IntPtr planeHandle, Pose pose)
        {
            int isPoseInPolygon = 0;
            var poseHandle = m_ndkSession.PoseAdapter.Create(pose);
            NDKAPI.HwArPlane_isPoseInPolygon(m_ndkSession.SessionHandle, planeHandle, poseHandle, ref isPoseInPolygon);
            m_ndkSession.PoseAdapter.Destroy(poseHandle);
            return isPoseInPolygon != 0;
        }

        public ARPlane.ARPlaneSemanticLabel GetLabel(IntPtr planeHandle)
        {
            int planeLable = 0;
            NDKAPI.HwArPlane_getLabel(m_ndkSession.SessionHandle, planeHandle, ref planeLable);
            return (ARPlane.ARPlaneSemanticLabel)planeLable;
        }

        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPlane_acquireSubsumedBy(IntPtr sessionHandle, IntPtr planeHandle,
                                 ref IntPtr outSubsumedByHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPlane_getType(IntPtr sessionHandle, IntPtr planeHandle,
                       ref int outPlaneTypeHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPlane_getCenterPose(IntPtr sessionHandle, IntPtr planeHandle,
                             IntPtr outPoseHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPlane_getExtentX(IntPtr sessionHandle, IntPtr planeHandle,
                          ref float outExtentX);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPlane_getExtentZ(IntPtr sessionHandle, IntPtr planeHandle,
                          ref float outExtentZ);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPlane_getPolygonSize(IntPtr sessionHandle, IntPtr planeHandle,
                              ref int outPolygonSize);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPlane_getPolygon(IntPtr sessionHandle, IntPtr planeHandle,
                           IntPtr outPolygonXZ);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPlane_isPoseInExtents(IntPtr sessionHandle, IntPtr planeHandle,
                               IntPtr poseHandle, ref int outPoseInExtents);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPlane_isPoseInPolygon(IntPtr sessionHandle, IntPtr planeHandle,
                               IntPtr poseHandle, ref int outPoseInPolygon);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPlane_getLabel(IntPtr sessionHandle, IntPtr planeHandle, ref int outPlanelabel);
        }
    }
}
