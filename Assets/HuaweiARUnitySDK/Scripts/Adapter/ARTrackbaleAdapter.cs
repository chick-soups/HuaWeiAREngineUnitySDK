
namespace HuaweiARInternal
{
    using System;
    using System.Runtime.InteropServices;
    using HuaweiARUnitySDK;
    using UnityEngine;
    using System.Collections.Generic;

    internal class ARTrackbaleAdapter
    {
        private NDKSession m_ndkSession;

        public ARTrackbaleAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public NDKARTrackableType GetType(IntPtr trackableHandle)
        {
            NDKARTrackableType type = NDKARTrackableType.BaseTrackable;
            NDKAPI.HwArTrackable_getType(m_ndkSession.SessionHandle, trackableHandle, ref type);
            return type;
        }

        public ARTrackable.TrackingState GetTrackingState(IntPtr trackableHandle)
        {
            ARTrackable.TrackingState state = ARTrackable.TrackingState.STOPPED;
            NDKAPI.HwArTrackable_getTrackingState(m_ndkSession.SessionHandle, trackableHandle,
                ref state);
            return state;
        }

        public bool AcquireNewAnchor(IntPtr trackableHandle, Pose pose, out IntPtr anchorHandle)
        {
            IntPtr poseHandle = m_ndkSession.PoseAdapter.Create(pose);
            anchorHandle = IntPtr.Zero;
            int status = NDKAPI.HwArTrackable_acquireNewAnchor(m_ndkSession.SessionHandle, trackableHandle, poseHandle,
                ref anchorHandle);
            m_ndkSession.PoseAdapter.Destroy(poseHandle);
            return status == (int)NDKARStatus.HWAR_SUCCESS;
        }

        public void GetAnchors(IntPtr trackableHandle, List<ARAnchor> anchors)
        {
            IntPtr anchorListHandle = m_ndkSession.AnchorAdapter.CreateList();
            NDKAPI.HwArTrackable_getAnchors(m_ndkSession.SessionHandle, trackableHandle, anchorListHandle);

            anchors.Clear();
            int cntOfAnchor = m_ndkSession.AnchorAdapter.GetListSize(anchorListHandle);
            for (int i = 0; i < cntOfAnchor; i++)
            {
                IntPtr anchorHandle = m_ndkSession.AnchorAdapter.AcquireListItem(anchorListHandle, i);

                ARAnchor anchor = m_ndkSession.AnchorManager.ARAnchorFactory(anchorHandle, false);

                if (anchorHandle == IntPtr.Zero)
                {
                    Debug.LogFormat("anchor {0} is not valid", anchorHandle);
                }
                else
                {
                    anchors.Add(anchor);
                }
            }
            m_ndkSession.AnchorAdapter.DestroyList(anchorListHandle);
        }

        public void Release(IntPtr trackableHandle)
        {
            NDKAPI.HwArTrackable_release(trackableHandle);
        }

        //trackable list

        public IntPtr CreateList()
        {
            IntPtr handle = IntPtr.Zero;
            NDKAPI.HwArTrackableList_create(m_ndkSession.SessionHandle, ref handle);
            return handle;
        }

        public void DestroyList(IntPtr listHandle)
        {
            NDKAPI.HwArTrackableList_destroy(listHandle);
        }

        public int GetListSize(IntPtr listHandle)
        {
            int count = 0;
            NDKAPI.HwArTrackableList_getSize(m_ndkSession.SessionHandle, listHandle, ref count);
            return count;
        }

        public IntPtr AcquireListItem(IntPtr listHandle, int index)
        {
            IntPtr trackableHandle = IntPtr.Zero;
            NDKAPI.HwArTrackableList_acquireItem(m_ndkSession.SessionHandle, listHandle, index,
                ref trackableHandle);
            return trackableHandle;
        }

        private struct NDKAPI
        {
            //trackable 

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArTrackable_getType(IntPtr sessionHandle, IntPtr trackableHandle,
                ref NDKARTrackableType trackableType);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArTrackable_getTrackingState(IntPtr sessionHandle,
                IntPtr trackableHandle, ref ARTrackable.TrackingState trackingState);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern int HwArTrackable_acquireNewAnchor(IntPtr sessionHandle, IntPtr trackableHandle,
                IntPtr poseHandle, ref IntPtr anchorHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArTrackable_getAnchors(IntPtr sessionHandle, IntPtr trackableHandle,
                IntPtr outputListHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArTrackable_release(IntPtr trackableHandle);

            //trackbale list 
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArTrackableList_create(IntPtr sessionHandle, ref IntPtr trackableListHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArTrackableList_destroy(IntPtr trackableListHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArTrackableList_getSize(IntPtr sessionHandle, IntPtr trackableListHandle,
                ref int outSize);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArTrackableList_acquireItem(IntPtr sessionHandle, IntPtr trackableListHandle,
                int index, ref IntPtr outTrackable);
        }
    }
}
