
namespace HuaweiARInternal
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using HuaweiARUnitySDK;
    using System.Collections;
    using System.Collections.Generic;

    internal class ARFrameAdapter
    {
        private NDKSession m_ndkSession;

        public ARFrameAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public IntPtr Create()
        {
            IntPtr frameHandle = IntPtr.Zero;
            NDKAPI.HwArFrame_create(m_ndkSession.SessionHandle, ref frameHandle);
            return frameHandle;
        }

        public void Destroy(IntPtr frameHandle)
        {
            NDKAPI.HwArFrame_destroy(frameHandle);
        }

        public bool GetDisplayGeometryChanged()
        {
            int changed = 0;
            NDKAPI.HwArFrame_getDisplayGeometryChanged(m_ndkSession.SessionHandle, m_ndkSession.FrameHandle, ref changed);
            return changed == 0 ? false : true;
        }
        public long GetTimestamp()
        {
            long timestamp = 0;
            NDKAPI.HwArFrame_getTimestamp(m_ndkSession.SessionHandle, m_ndkSession.FrameHandle, ref timestamp);
            return timestamp;
        }

        public float[] TransformDisplayUvCoords(float[] inUV)
        {
            int numOfUV = 8;
            float[] tmpOut = new float[numOfUV];
            float[] tmpIn = new float[numOfUV];
            Array.Copy(inUV, tmpIn, inUV.Length);
            GCHandle unmanagedInUVHandle = GCHandle.Alloc(tmpIn, GCHandleType.Pinned);
            GCHandle unmanagedOutUVHandle = GCHandle.Alloc(tmpOut, GCHandleType.Pinned);

            NDKAPI.HwArFrame_transformDisplayUvCoords(m_ndkSession.SessionHandle, m_ndkSession.FrameHandle, numOfUV,
                unmanagedInUVHandle.AddrOfPinnedObject(), unmanagedOutUVHandle.AddrOfPinnedObject());

            float[] outUV = new float[numOfUV];
            Array.Copy(tmpOut, outUV, tmpOut.Length);
            unmanagedInUVHandle.Free();
            unmanagedOutUVHandle.Free();
            return outUV;
        }

        public void HitTest(float pixelX, float pixelY, List<ARHitResult> hitResultList)
        {
            hitResultList.Clear();

            IntPtr hitResultListHandle = m_ndkSession.HitResultAdapter.CreateList();
            NDKAPI.HwArFrame_hitTest(m_ndkSession.SessionHandle, m_ndkSession.FrameHandle, pixelX, pixelY, hitResultListHandle);
            int cntOfResult = m_ndkSession.HitResultAdapter.GetListSize(hitResultListHandle);
            ARDebug.LogInfo("HitTest hitresult {0}", cntOfResult);
            for (int i = 0; i < cntOfResult; i++)
            {
                //todo refactor the following code when arplaneHitResult is removed
                IntPtr hitResultHandle = m_ndkSession.HitResultAdapter.AcquireListItem(hitResultListHandle, i);
                IntPtr trackableHandle = m_ndkSession.HitResultAdapter.GetTrackbaleHandle(hitResultHandle);
                ARDebug.LogInfo("HitTest hittype! {0}", m_ndkSession.TrackableAdapter.GetType(trackableHandle), i);
                switch (m_ndkSession.TrackableAdapter.GetType(trackableHandle))
                {
                    case NDKARTrackableType.Plane:
                        hitResultList.Add(new ARPlaneHitResult(hitResultHandle, m_ndkSession));
                        break;
                    case NDKARTrackableType.Point:
                        hitResultList.Add(new ARPointCloudHitResult(hitResultHandle, m_ndkSession, ARFrame.AcquirePointCloud()));
                        break;
                    case NDKARTrackableType.Invalid:
                        break;
                    case (NDKARTrackableType)0x41520105:
                        break;
                    default:
                        hitResultList.Add(new ARHitResult(hitResultHandle, m_ndkSession));
                        break;
                }
            }
            m_ndkSession.HitResultAdapter.DestroyList(hitResultListHandle);
        }

        public void HitTestArea(float[] inPoints, List<ARHitResult> hitResultList)
        {
            hitResultList.Clear();

            if (inPoints == null || inPoints.Length == 0) {
                ARDebug.LogError("HitTestArea inPoints is empty");
                return;
            }

            float[] tmpIn = new float[inPoints.Length];
            inPoints.CopyTo(tmpIn, 0);
            GCHandle unmanagedInUVHandle = GCHandle.Alloc(tmpIn, GCHandleType.Pinned);

            IntPtr hitResultListHandle = m_ndkSession.HitResultAdapter.CreateList();
            NDKARStatus status = NDKAPI.HwArFrame_hitTestArea(m_ndkSession.SessionHandle, m_ndkSession.FrameHandle, 
                unmanagedInUVHandle.AddrOfPinnedObject(), tmpIn.Length, hitResultListHandle);
            unmanagedInUVHandle.Free();

            if (status != NDKARStatus.HWAR_SUCCESS) {
                ARDebug.LogError("HitTestArea status is error:{}", status);
                return;
            }

            int cntOfResult = m_ndkSession.HitResultAdapter.GetListSize(hitResultListHandle);
            ARDebug.LogInfo("HitTestArea hit result count{0}", cntOfResult);
            for (int i = 0; i < cntOfResult; i++)
            {
                //todo refactor the following code when arplaneHitResult is removed
                IntPtr hitResultHandle = m_ndkSession.HitResultAdapter.AcquireListItem(hitResultListHandle, i);
                IntPtr trackableHandle = m_ndkSession.HitResultAdapter.GetTrackbaleHandle(hitResultHandle);
                ARDebug.LogInfo("HitTestArea hittype! {0}", m_ndkSession.TrackableAdapter.GetType(trackableHandle), i);
                switch (m_ndkSession.TrackableAdapter.GetType(trackableHandle))
                {
                    case NDKARTrackableType.Plane:
                        hitResultList.Add(new ARPlaneHitResult(hitResultHandle, m_ndkSession));
                        break;
                    case NDKARTrackableType.Point:
                        hitResultList.Add(new ARPointCloudHitResult(hitResultHandle, m_ndkSession, ARFrame.AcquirePointCloud()));
                        break;
                    case NDKARTrackableType.Invalid:
                        break;
                    case (NDKARTrackableType)0x41520105:
                        break;
                    default:
                        hitResultList.Add(new ARHitResult(hitResultHandle, m_ndkSession));
                        break;
                }
            }
            m_ndkSession.HitResultAdapter.DestroyList(hitResultListHandle);
        }

        public ARLightEstimate GetLightEstimate()
        {
            IntPtr lightEstimateHandle = m_ndkSession.LightEstimateAdapter.Create();
            NDKAPI.HwArFrame_getLightEstimate(m_ndkSession.SessionHandle, m_ndkSession.FrameHandle, lightEstimateHandle);
            bool valid = m_ndkSession.LightEstimateAdapter.GetState(lightEstimateHandle);
            float intensity = m_ndkSession.LightEstimateAdapter.GetPixelIntensity(lightEstimateHandle);
            m_ndkSession.LightEstimateAdapter.Destroy(lightEstimateHandle);
            return new ARLightEstimate(valid, intensity);
        }

        public IntPtr AcquirePointCloudHandle()
        {
            IntPtr pointcouldHandle = IntPtr.Zero;
            NDKARStatus status = NDKAPI.HwArFrame_acquirePointCloud(m_ndkSession.SessionHandle,
                m_ndkSession.FrameHandle, ref pointcouldHandle);
            ARExceptionAdapter.ExtractException(status);
            return pointcouldHandle;
        }

        public IntPtr AcquireCameraHandle()
        {
            IntPtr cameraHandle = IntPtr.Zero;
            NDKAPI.HwArFrame_acquireCamera(m_ndkSession.SessionHandle, m_ndkSession.FrameHandle, ref cameraHandle);
            return cameraHandle;
        }

        public IntPtr AcquireImageMetadata()
        {
            IntPtr imageMetadataHandle = IntPtr.Zero;
            NDKARStatus status = NDKAPI.HwArFrame_acquireImageMetadata(m_ndkSession.SessionHandle, m_ndkSession.FrameHandle, ref imageMetadataHandle);
            ARExceptionAdapter.ExtractException(status);
            return imageMetadataHandle;
        }

        public void GetUpdatedAnchors(List<ARAnchor> anchorList)
        {
            IntPtr anchorListHandle = m_ndkSession.AnchorAdapter.CreateList();
            NDKAPI.HwArFrame_getUpdatedAnchors(m_ndkSession.SessionHandle, m_ndkSession.FrameHandle, ref anchorListHandle);
            int sizeOfList = m_ndkSession.AnchorAdapter.GetListSize(anchorListHandle);
            anchorList.Clear();

            for (int i = 0; i < sizeOfList; i++)
            {
                //todo add new function 
                IntPtr anchorHandle = m_ndkSession.AnchorAdapter.AcquireListItem(anchorListHandle, i);
                ARAnchor anchor = m_ndkSession.AnchorManager.ARAnchorFactory(anchorHandle, false);
                anchorList.Add(anchor);
            }

            m_ndkSession.AnchorAdapter.DestroyList(anchorListHandle);
        }

        public void GetUpdatedTrackables(List<ARTrackable> trackableList)
        {

            IntPtr trackableListHandle = m_ndkSession.TrackableAdapter.CreateList();
            NDKAPI.HwArFrame_getUpdatedTrackables(m_ndkSession.SessionHandle, m_ndkSession.FrameHandle,
                NDKARTrackableType.BaseTrackable, trackableListHandle);
            int sizeOfList = m_ndkSession.TrackableAdapter.GetListSize(trackableListHandle);
            trackableList.Clear();
            for (int i = 0; i < sizeOfList; i++)
            {
                IntPtr trackableHandle = m_ndkSession.TrackableAdapter.AcquireListItem(trackableListHandle, i);

                trackableList.Add(m_ndkSession.TrackableManager.ARTrackableFactory(trackableHandle, false));
            }
            m_ndkSession.TrackableAdapter.DestroyList(trackableListHandle);
        }

        public IntPtr AcquireCameraImage()
        {
            IntPtr imageHandle = IntPtr.Zero;
            NDKARStatus status = NDKAPI.HwArFrame_acquireCameraImage(m_ndkSession.SessionHandle, m_ndkSession.FrameHandle, ref imageHandle);
            ARExceptionAdapter.ExtractException(status);
            return imageHandle;
        }

        public IntPtr AcquireDepthImage()
        {
            IntPtr imageHandle = IntPtr.Zero;
            NDKARStatus status = NDKAPI.HwArFrame_acquireDepthImage(m_ndkSession.SessionHandle, m_ndkSession.FrameHandle, ref imageHandle);
            ARExceptionAdapter.ExtractException(status);
            return imageHandle;
        }

        public ARWorldMappingState GetMappingState()
        {
            int ret = (int)ARWorldMappingState.NOT_AVAILABLE;
            NDKAPI.HwArFrame_getMappingState(m_ndkSession.SessionHandle, m_ndkSession.FrameHandle, ref ret);
            ARDebug.LogError("NDK world map status " + ret);
            if (!ValueLegalityChecker.CheckInt("GetMappingState",ret,
                AdapterConstants.Enum_ARWorldMappingState_MinIntValue,
                AdapterConstants.Enum_ARWorldMappingState_MaxIntValue))
            {
                return ARWorldMappingState.NOT_AVAILABLE;
            }
            return (ARWorldMappingState)ret;
        }
        public ARAlignState GetAlignState()
        {
            int ret = (int)ARAlignState.NONE;
            NDKAPI.HwArFrame_getAlignState(m_ndkSession.SessionHandle, m_ndkSession.FrameHandle, ref ret);
            if(!ValueLegalityChecker.CheckInt("GetAlignState",ret,
                AdapterConstants.Enum_ARAlignState_MinIntValue, AdapterConstants.Enum_ARAlignState_MaxIntValue))
            {
                return ARAlignState.FAILED;
            }
            return (ARAlignState)ret;
        }

        public IntPtr AcquireSceneMesh()
        {
            IntPtr sceneMeshHandle = IntPtr.Zero;
            NDKARStatus status = NDKAPI.HwArFrame_acquireSceneMesh(m_ndkSession.SessionHandle, m_ndkSession.FrameHandle, ref sceneMeshHandle);
            ARExceptionAdapter.ExtractException(status);
            return sceneMeshHandle;
        }

        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFrame_create(IntPtr sessionHandle, ref IntPtr outFrameHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFrame_destroy(IntPtr frameHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFrame_getDisplayGeometryChanged(IntPtr sessionHandle, IntPtr frameHandle,
                ref int outGometryChanged);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFrame_getTimestamp(IntPtr sessionHandle, IntPtr frameHandle,
                ref long outTimestampNs);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFrame_transformDisplayUvCoords(IntPtr sessionHandle, IntPtr frameHandle,
                                        int numElements, IntPtr uvsIn, IntPtr uvsOut);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFrame_hitTest(IntPtr sessionHandle, IntPtr frameHandle, float pixelX,
                float pixelY, IntPtr hitResultListHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArFrame_hitTestArea(IntPtr sessionHandle, IntPtr frameHandle, IntPtr input2DPoints, int inputLength, IntPtr hitResultListHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFrame_getLightEstimate(IntPtr sessionHandle, IntPtr frameHandle,
                IntPtr outLightEstimateHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArFrame_acquirePointCloud(IntPtr sessionHandle, IntPtr frameHandle,
                                       ref IntPtr outPointCloudHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFrame_acquireCamera(IntPtr sessionHandle, IntPtr frameHandle,
                ref IntPtr outCameraHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArFrame_acquireImageMetadata(IntPtr sessionHandle, IntPtr frameHandle,
                ref IntPtr outMetadataHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFrame_getUpdatedAnchors(IntPtr sessionHandle, IntPtr frameHandle,
                ref IntPtr outAnchorListHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFrame_getUpdatedTrackables(IntPtr sessionHandle, IntPtr frameHandle,
                NDKARTrackableType filterType, IntPtr outTrackableListHandle);

			[DllImport(AdapterConstants.HuaweiARNativeApi)]
			public static extern NDKARStatus HwArFrame_acquireCameraImage(IntPtr sessionHandle, IntPtr frameHandle,
				ref IntPtr ImageHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArFrame_acquireDepthImage(IntPtr sessionHandle, IntPtr frameHandle,
                ref IntPtr ImageHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFrame_getMappingState(IntPtr sessionHandle, IntPtr frameHandle,
                ref int outMappingState);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFrame_getAlignState(IntPtr sessionHandle, IntPtr frameHandle,
                ref int outAlignState);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArFrame_acquireSceneMesh(IntPtr sessionHandle, IntPtr frameHandle, ref IntPtr outSceneMesh);
        }
    }
}
