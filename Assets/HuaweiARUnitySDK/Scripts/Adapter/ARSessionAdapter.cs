
namespace HuaweiARInternal
{
    using System.Runtime.InteropServices;
    using UnityEngine;
    using System;
    using HuaweiARUnitySDK;
    using System.Collections.Generic;
    internal class ARSessionAdapter
    {
        private NDKSession m_ndkSession;

        public ARSessionAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public IntPtr Create()
        {
            IntPtr sessionHandle= IntPtr.Zero;
            ARDebug.LogInfo("native create seesion begin");

            IntPtr jEnv = ARUnityHelper.Instance.GetJEnv();
            IntPtr activity = ARUnityHelper.Instance.GetActivityHandle();
            NDKARStatus status= NDKAPI.HwArSession_create(jEnv,activity, ref sessionHandle);
            ARDebug.LogInfo("native create seesion returns status {0}",status);
            ARExceptionAdapter.ExtractException(status);
            return sessionHandle;
        }

        public void Destroy()
        {
            IntPtr sessionHandle = m_ndkSession.SessionHandle;
            ARDebug.LogInfo("native destroy session begin, handle =0x{0}",sessionHandle.ToString("x8"));
            NDKAPI.HwArSession_destroy(sessionHandle);
            ARDebug.LogInfo("native destroy session end");
        }

        public bool IsSupported(ARConfigBase unityConfig)
        {
            return true;
        }

        public void Config(ARConfigBase unityConfig)
        {
            //ARDebug.LogInfo("native config begin" + unityConfig.ToString());
            IntPtr configHandle = m_ndkSession.ConfigBaseAdapter.Create();
            m_ndkSession.ConfigBaseAdapter.UpdateNDKConfigWithUnityConfig(configHandle, unityConfig);
            NDKARStatus status = NDKAPI.HwArSession_configure(m_ndkSession.SessionHandle, configHandle);
            m_ndkSession.ConfigHandle = configHandle;
            ARDebug.LogInfo("native config end with value:{0}", status);
            m_ndkSession.ConfigBaseAdapter.UpdateUnityConfigWithNDKConfig(configHandle, unityConfig);
            //m_ndkSession.ConfigBaseAdapter.Destroy(configHandle);
            ARExceptionAdapter.ExtractException(status);
        }

        public void Resume()
        {
            NDKARStatus status = NDKAPI.HwArSession_resume(m_ndkSession.SessionHandle);
            ARExceptionAdapter.ExtractException(status);
        }

        public void Pause()
        {
            NDKARStatus status = NDKAPI.HwArSession_pause(m_ndkSession.SessionHandle);
            ARDebug.LogInfo("native pause end with value:{0}",status);
            ARExceptionAdapter.ExtractException(status);

        }

        public void Stop()
        {
            NDKARStatus status = NDKAPI.HwArSession_stop(m_ndkSession.SessionHandle);
            ARDebug.LogInfo("native stop end with value:{0}", status);
            ARExceptionAdapter.ExtractException(status);
        }

        public void Update()
        {
            //release form handle
            if (m_ndkSession.FrameHandle != IntPtr.Zero)
            {
                m_ndkSession.FrameAdapter.Destroy(m_ndkSession.FrameHandle);
            }

            //IntPtr frameHandle = m_ndkSession.FrameAdapter.Create();
            //GL.InvalidateState();
            //NDKARStatus status = NDKAPI.HwArSession_update(m_ndkSession.SessionHandle, frameHandle);
            //GL.InvalidateState();
            IntPtr frameHandle = IntPtr.Zero;
            NDKAPI.SetCurrentSessionHandle(m_ndkSession.SessionHandle);
            GL.IssuePluginEvent(NDKAPI.GetRenderEventFunc(), 1);
            NDKAPI.WaitForRenderingThreadFinished();
            NDKARStatus status = NDKARStatus.HWAR_SUCCESS;
            NDKAPI.GetCurrentFrameHandleAndStatus(ref frameHandle, ref status);
            ARExceptionAdapter.ExtractException(status);
            m_ndkSession.FrameHandle = frameHandle;
        }

        public void SetCameraTextureName(int textureId)
        {
            NDKAPI.HwArSession_setCameraTextureName(m_ndkSession.SessionHandle, textureId);
        }

        public void SetDisplayGeometry(ScreenOrientation orientation, int width, int height)
        {
            const int androidRotation0 = 0;
            const int androidRotation90 = 1;
            const int androidRotation180 = 2;
            const int androidRotation270 = 3;

            int androidOrientation = 0;
            switch (orientation)
            {
                case ScreenOrientation.LandscapeLeft:
                    androidOrientation = androidRotation90;
                    break;
                case ScreenOrientation.LandscapeRight:
                    androidOrientation = androidRotation270;
                    break;
                case ScreenOrientation.Portrait:
                    androidOrientation = androidRotation0;
                    break;
                case ScreenOrientation.PortraitUpsideDown:
                    androidOrientation = androidRotation180;
                    break;

            }
            ARDebug.LogInfo("native set display geometry begin with android oritentation:{0}, width={1}, height={2}",
                androidOrientation, width, height);
            NDKAPI.HwArSession_setDisplayGeometry(m_ndkSession.SessionHandle, androidOrientation, width, height);
        }

        public ARAnchor CreateAnchor(Pose pose)
        {
            IntPtr poseHandle = m_ndkSession.PoseAdapter.Create(pose);
            IntPtr anchorHandle = IntPtr.Zero;
            ARDebug.LogInfo("native acquire anchor begin with pose:{0}",pose.ToString());
            NDKARStatus status = NDKAPI.HwArSession_acquireNewAnchor(m_ndkSession.SessionHandle,
                poseHandle,ref anchorHandle);
            ARDebug.LogInfo("native acquire anchor end with status={0}", status);
            m_ndkSession.PoseAdapter.Destroy(poseHandle);
            ARExceptionAdapter.ExtractException(status);
            var anchor = m_ndkSession.AnchorManager.ARAnchorFactory(anchorHandle, true);
            return anchor;
        }

        public void GetAllAnchors(List<ARAnchor> anchorList)
        {
            anchorList.Clear();
            IntPtr anchorListHandle = m_ndkSession.AnchorAdapter.CreateList();
            NDKAPI.HwArSession_getAllAnchors(m_ndkSession.SessionHandle, anchorListHandle);

            int cntOfAnchor = m_ndkSession.AnchorAdapter.GetListSize(anchorListHandle);
            for(int i = 0; i < cntOfAnchor; i++)
            {
                IntPtr anchorHandle = m_ndkSession.AnchorAdapter.AcquireListItem(anchorListHandle, i);
                anchorList.Add( m_ndkSession.AnchorManager.ARAnchorFactory(anchorHandle, false));
            }
            m_ndkSession.AnchorAdapter.DestroyList(anchorListHandle);
        }

        //this function will add newly trackable into trackableManager
        public void GetAllTrackables(List<ARTrackable> trackableList)
        {
            trackableList.Clear();
            IntPtr trackableListHandle = m_ndkSession.TrackableAdapter.CreateList();
            NDKAPI.HwArSession_getAllTrackables(m_ndkSession.SessionHandle, 
                NDKARTrackableType.BaseTrackable, trackableListHandle);

            int cntOfTrackable = m_ndkSession.TrackableAdapter.GetListSize(trackableListHandle);
            for(int i = 0; i < cntOfTrackable; i++)
            {
                IntPtr trackableHandle = m_ndkSession.TrackableAdapter.AcquireListItem(trackableListHandle, i);

                trackableList.Add(m_ndkSession.TrackableManager.ARTrackableFactory(trackableHandle,true));
            }
            m_ndkSession.TrackableAdapter.DestroyList(trackableListHandle);
        }

        public long GetSaveLimit()
        {
            long ret=0;
            NDKARStatus status = NDKAPI.HwArSession_getSaveLimit(m_ndkSession.SessionHandle, ref ret);
            ARExceptionAdapter.ExtractException(status);
            return ret;
        }

        public ARSharedData SaveSharedData()
        {
            long dataSize = GetSaveLimit();
            long usedSize = 0;
            ARSharedData.ARRawData rawdata = new ARSharedData.ARRawData(dataSize);
            NDKARStatus status= NDKAPI.HwArSession_save(m_ndkSession.SessionHandle, rawdata.m_pinAddr, dataSize,
                ref usedSize);
            ARExceptionAdapter.ExtractException(status);

            rawdata.DataSize = usedSize > 0? usedSize : 0;
            return new ARSharedData(rawdata);
        }
        public void LoadSharedData(ARSharedData sharedData)
        {
            NDKARStatus status = NDKAPI.HwArSession_load(m_ndkSession.SessionHandle, sharedData.RawData.m_pinAddr,
                sharedData.RawData.DataSize);
            ARExceptionAdapter.ExtractException(status);
        }

        public long GetSerializeAnchorsLimit()
        {
            long ret = 0;
            NDKARStatus status = NDKAPI.HwArSession_getSerializeAnchorsLimit(m_ndkSession.SessionHandle, ref ret);
            ARExceptionAdapter.ExtractException(status);
            return ret;
        }

        public ARSharedData SerializeAnchors(List<ARAnchor> anchorList, bool isNeedAlign)
        {
            long dataSize = GetSerializeAnchorsLimit();
            long usedSize = 0;
            IntPtr anchorListHandle = m_ndkSession.AnchorAdapter.CreateList();
            foreach(ARAnchor anchor in anchorList)
            {
                m_ndkSession.AnchorAdapter.AddListItem(anchorListHandle, anchor.m_anchorHandle);
            }
            ARSharedData.ARRawData rawData = new ARSharedData.ARRawData(dataSize);
            NDKARStatus status = NDKAPI.HwArSession_serializeAnchors(m_ndkSession.SessionHandle,
                anchorListHandle, isNeedAlign,rawData.m_pinAddr,dataSize,ref usedSize);
            ARExceptionAdapter.ExtractException(status);
            rawData.DataSize = usedSize;
            return new ARSharedData(rawData);
        }

        public void DeSerializeAnchors(ARSharedData sharedData,List<ARAnchor> anchors)
        {
            IntPtr anchorListHandle = m_ndkSession.AnchorAdapter.CreateList();
            NDKARStatus status = NDKAPI.HwArSession_deserializeAnchors(m_ndkSession.SessionHandle,
                sharedData.RawData.m_pinAddr, sharedData.RawData.DataSize, anchorListHandle);
            ARExceptionAdapter.ExtractException(status);
            int anchorListSize = m_ndkSession.AnchorAdapter.GetListSize(anchorListHandle);
            for(int i = 0; i < anchorListSize; i++)
            {
                IntPtr anchorHandle = m_ndkSession.AnchorAdapter.AcquireListItem(anchorListHandle, i);
                ARAnchor anchor = m_ndkSession.AnchorManager.ARAnchorFactory(anchorHandle, true);
                anchors.Add(anchor);
            }
            m_ndkSession.AnchorAdapter.DestroyList(anchorListHandle);
        }

        public ARCameraConfig GetCameraConfig()
        {
            IntPtr cameraConfigHandle = m_ndkSession.CameraConfigAdapter.Create();
            NDKAPI.HwArSession_getCameraConfig(m_ndkSession.SessionHandle, cameraConfigHandle);
            ARCameraConfig cameraConfig = new ARCameraConfig(cameraConfigHandle, m_ndkSession);
            return cameraConfig;
        }

        public int GetSupportedSemanticMode()
        {
            int mode = 0;
            NDKAPI.HwArSession_getSupportedSemanticMode(m_ndkSession.SessionHandle, ref mode);
            return mode;
        }

        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArSession_create(IntPtr envHandle,IntPtr applicationContextHandle,
                                    ref IntPtr sessionHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArSession_destroy(IntPtr sessionHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArSession_checkSupported(IntPtr sessionHandle,IntPtr configHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArSession_configure(IntPtr sessionHandle, IntPtr configHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArSession_resume(IntPtr sessionHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArSession_pause(IntPtr sessionHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArSession_stop(IntPtr sessionHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArSession_update(IntPtr sessionHandle, IntPtr outFrameHandle);


            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArSession_setCameraTextureName(IntPtr sessionHandle, int textureId);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArSession_setDisplayGeometry(IntPtr sessionHandle,int rotation,
                int width,int height);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArSession_acquireNewAnchor(IntPtr sessionHandle,IntPtr poseHandle,
                ref IntPtr outAnchorHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArSession_getAllAnchors(IntPtr sessionHandle,IntPtr outAnchorListHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArSession_getAllTrackables(IntPtr sessionHandle,
                                  NDKARTrackableType filterType,IntPtr outTrackableListHandle);


            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArSession_getSaveLimit(IntPtr sessionHandle,
                ref long outSize);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArSession_save(IntPtr sessionHandle,
                      IntPtr buffer, long bufCap, ref long retBufLen);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArSession_load(IntPtr sessionHandle,
                                  IntPtr buffer, long bufLength);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArSession_serializeAnchors(IntPtr sessionHandle, IntPtr anchorListHandle,
                bool isNeedAlign, IntPtr buffer, long bufLength, ref long outSize);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArSession_deserializeAnchors(IntPtr sessionHandle, IntPtr buffer,
                long bufLength,IntPtr outAnchorList);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArSession_getSerializeAnchorsLimit(IntPtr sessionHandle,
                ref long outSize);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArSession_getCameraConfig(IntPtr sessionHandle, IntPtr outCameraConfigHandle);

            [DllImport(AdapterConstants.UnityPluginApi)]
            public static extern IntPtr GetRenderEventFunc();

            [DllImport(AdapterConstants.UnityPluginApi)]
            public static extern void SetCurrentSessionHandle(IntPtr sessionHandle);

            [DllImport(AdapterConstants.UnityPluginApi)]
            public static extern void GetCurrentFrameHandleAndStatus(ref IntPtr frameHandle, ref NDKARStatus status);

            [DllImport(AdapterConstants.UnityPluginApi)]
            public static extern void WaitForRenderingThreadFinished();

            [DllImport(AdapterConstants.UnityPluginApi)]
            public static extern void HwArSession_getSupportedSemanticMode(IntPtr sessionHandle, ref int mode);
        }

    }
}
