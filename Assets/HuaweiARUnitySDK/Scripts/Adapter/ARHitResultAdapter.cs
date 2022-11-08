
namespace HuaweiARInternal
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using HuaweiARUnitySDK;
    using System.Collections;
    using System.Collections.Generic;
    internal class ARHitResultAdapter
    {
        private NDKSession m_ndkSession;

        public ARHitResultAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public IntPtr Create()
        {
            IntPtr hitResultHandle = IntPtr.Zero;
            NDKAPI.HwArHitResult_create(m_ndkSession.SessionHandle, ref hitResultHandle);
            return hitResultHandle;
        }
        public void Destroy(IntPtr hitResultHandle)
        {
            NDKAPI.HwArHitResult_destroy(hitResultHandle);
        }

        public float GetDistance(IntPtr hitResultHandle)
        {
            float f = 0f;
            NDKAPI.HwArHitResult_getDistance(m_ndkSession.SessionHandle, hitResultHandle, ref f);
            return f;
        }
        public Pose GetHitPose(IntPtr hitResultHandle)
        {
            IntPtr poseHandle = m_ndkSession.PoseAdapter.Create();
            NDKAPI.HwArHitResult_getHitPose(m_ndkSession.SessionHandle, hitResultHandle, poseHandle);
            Pose p = m_ndkSession.PoseAdapter.GetPoseValue(poseHandle);
            m_ndkSession.PoseAdapter.Destroy(poseHandle);
            return p;
        }
        //this fuction will not produce more reference
        public ARTrackable AcquireTrackable(IntPtr hitResultHandle)
        {
            IntPtr trackableHandle = IntPtr.Zero;
            NDKAPI.HwArHitResult_acquireTrackable(m_ndkSession.SessionHandle, hitResultHandle, ref trackableHandle);
            ARTrackable trackable = m_ndkSession.TrackableManager.ARTrackableFactory(trackableHandle, true);
            return trackable;
        }

        internal IntPtr GetTrackbaleHandle(IntPtr hitResultHandle)
        {
            IntPtr trackableHandle = IntPtr.Zero;
            NDKAPI.HwArHitResult_acquireTrackable(m_ndkSession.SessionHandle, hitResultHandle, ref trackableHandle);
            return trackableHandle;
        }

        //todo changed 
        public ARAnchor AcquireNewAnchor(IntPtr hitResultHandle)
        {
            IntPtr anchorHandle = IntPtr.Zero;
            NDKARStatus status = NDKAPI.HwArHitResult_acquireNewAnchor(m_ndkSession.SessionHandle, hitResultHandle,
                ref anchorHandle);
            ARExceptionAdapter.ExtractException(status);
            ARAnchor anchor = m_ndkSession.AnchorManager.ARAnchorFactory(anchorHandle, true);
            return anchor;
        }

        public IntPtr CreateList()
        {
            IntPtr listHandle = IntPtr.Zero;
            NDKAPI.HwArHitResultList_create(m_ndkSession.SessionHandle, ref listHandle);
            return listHandle;
        }

        public void DestroyList(IntPtr listHandle)
        {
            NDKAPI.HwArHitResultList_destroy(listHandle);
        }

        public int GetListSize(IntPtr listHandle)
        {
            int size = 0;
            NDKAPI.HwArHitResultList_getSize(m_ndkSession.SessionHandle, listHandle, ref size);
            return size;
        }

        public IntPtr AcquireListItem(IntPtr listHandle, int index)
        {
            IntPtr hitResultHandle = m_ndkSession.HitResultAdapter.Create();

            NDKAPI.HwArHitResultList_getItem(m_ndkSession.SessionHandle, listHandle, index, hitResultHandle);
            return hitResultHandle;
        }

        private struct NDKAPI
        {
            //hit result 
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHitResult_create(IntPtr sessionHandle, ref IntPtr outHitResultHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHitResult_destroy(IntPtr hitResultHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHitResult_getDistance(IntPtr sessionHandle, IntPtr hitResultHandle,
                               ref float outDistance);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHitResult_getHitPose(IntPtr sessionHandle, IntPtr hitResultHandle,
                              IntPtr out_pose);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHitResult_acquireTrackable(IntPtr sessionHandle, IntPtr hitResultHandle,
                                    ref IntPtr outTrackableHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArHitResult_acquireNewAnchor(IntPtr sessionHandle,
                                          IntPtr hitResultHandle, ref IntPtr outAnchorHandle);
            //hit result list 
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHitResultList_create(IntPtr sessionHandle, ref IntPtr outHitResultList);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHitResultList_destroy(IntPtr hitResultListHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHitResultList_getSize(IntPtr sessionHandle, IntPtr hitResultListHandle,
                ref int out_size);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHitResultList_getItem(IntPtr sessionHandle, IntPtr hitResultListHandle,
                int index, IntPtr outHitResultHandle);

        }
    }
}
