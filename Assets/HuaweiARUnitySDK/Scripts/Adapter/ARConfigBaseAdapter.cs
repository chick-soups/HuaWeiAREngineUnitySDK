namespace HuaweiARInternal
{
    using System.Runtime.InteropServices;
    using UnityEngine;
    using System;
    using HuaweiARUnitySDK;

    internal class ARConfigBaseAdapter
    {
        private NDKSession m_ndkSession;

        public ARConfigBaseAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public IntPtr Create()
        {
            IntPtr configHandle = IntPtr.Zero;
            NDKAPI.HwArConfig_create(m_ndkSession.SessionHandle, ref configHandle);
            return configHandle;
        }
        public void Destroy(IntPtr configHandle)
        {
            NDKAPI.HwArConfig_destroy(configHandle);
        }

        public void UpdateNDKConfigWithUnityConfig(IntPtr configHandle, ARConfigBase arConfig)
        {
            NDKAPI.HwArConfig_setArType(m_ndkSession.SessionHandle, configHandle, (int)arConfig.GetARType());
            NDKAPI.HwArConfig_setCameraLensFacing(m_ndkSession.SessionHandle, configHandle, (int)arConfig.GetCameraLensFacing());
            NDKAPI.HwArConfig_setLightingMode(m_ndkSession.SessionHandle, configHandle, (int)arConfig.GetLightingMode());
            NDKAPI.HwArConfig_setPlaneFindingMode(m_ndkSession.SessionHandle, configHandle, (int)arConfig.GetPlaneFindingMode());
            NDKAPI.HwArConfig_setUpdateMode(m_ndkSession.SessionHandle, configHandle, (int)arConfig.GetUpdateMode());
            NDKAPI.HwArConfig_setImageInputMode(m_ndkSession.SessionHandle, configHandle, (int)arConfig.GetImageInputMode());


            NDKAPI.HwArConfig_setPowerMode(m_ndkSession.SessionHandle, configHandle, (int)arConfig.GetPowerMode());
            NDKAPI.HwArConfig_setFocusMode(m_ndkSession.SessionHandle, configHandle, (int)arConfig.GetFocusMode());
            NDKAPI.HwArConfig_setEnableItem(m_ndkSession.SessionHandle, configHandle, arConfig.GetConfigEnableItem());
            NDKAPI.HwArConfig_setSemanticMode(m_ndkSession.SessionHandle, configHandle, arConfig.GetConfigSemanticMode());


            //the following code is used to forward compatable
            if ((arConfig.GetARType() & (int)NDKARType.HAND_AR) != 0)
            {
                NDKAPI.HwArConfig_setHandFindingMode(m_ndkSession.SessionHandle, configHandle, (int)arConfig.GetHandFindingMode());
            }

            if (arConfig.GetAugImgDataBaseHandle() != null)
            {
                byte[] RawData = arConfig.GetAugImgDataBaseHandle().GetRawData();
                IntPtr AugImgDatabaseHandle = m_ndkSession.AugmentedImageDatabaseAdapter.CreateAugImgDatabaseFromBytes(RawData);
                NDKAPI.HwArConfig_setAugmentedImageDatabase(m_ndkSession.SessionHandle, configHandle, AugImgDatabaseHandle);
            }
            Vector2Int previewSize = arConfig.GetPreviewSize();
            Vector2Int bestSize=new Vector2Int();
            NDKAPI.CameraHelper_GetBestPreviewSize(previewSize,ref bestSize);
            NDKAPI.HwArConfig_setPreviewSize(m_ndkSession.SessionHandle, configHandle, bestSize.x, bestSize.y);
            arConfig.SetPreviewSize(bestSize);

        }

        public void UpdateUnityConfigWithNDKConfig(IntPtr configHandle, ARConfigBase arconfig)
        {
            int ret = 0;
            NDKAPI.HwArConfig_getCameraLensFacing(m_ndkSession.SessionHandle, configHandle, ref ret);
            arconfig.SetCameraLensFacing((ARConfigCameraLensFacing)ret);

            ARDebug.LogInfo("UpdateUnityConfigWithNDKConfig GetARType size {0}", arconfig.GetARType());
            NDKAPI.HwArConfig_getLightingMode(m_ndkSession.SessionHandle, configHandle, ref ret);
            arconfig.SetLightingMode(ret);

            NDKAPI.HwArConfig_getPlaneFindingMode(m_ndkSession.SessionHandle, configHandle, ref ret);
            arconfig.SetPlaneFindingMode((ARConfigPlaneFindingMode)ret);

            NDKAPI.HwArConfig_getUpdateMode(m_ndkSession.SessionHandle, configHandle, ref ret);
            arconfig.SetUpdateMode((ARConfigUpdateMode)ret);

            NDKAPI.HwArConfig_getImageInputMode(m_ndkSession.SessionHandle, configHandle, ref ret);
            arconfig.SetImageInputMode(ret);

            //this interface only support by HUAWEI_AR_ENGINE

            NDKAPI.HwArConfig_getHandFindingMode(m_ndkSession.SessionHandle, configHandle, ref ret);
            arconfig.SetHandFindingMode((ARConfigHandFindingMode)ret);

            NDKAPI.HwArConfig_getPowerMode(m_ndkSession.SessionHandle, configHandle, ref ret);
            arconfig.SetPowerMode((ARConfigPowerMode)ret);

            NDKAPI.HwArConfig_getFocusMode(m_ndkSession.SessionHandle, configHandle, ref ret);
            arconfig.SetFocusMode((ARConfigFocusMode)ret);

            ulong enableItem = 0;
            NDKAPI.HwArConfig_getEnableItem(m_ndkSession.SessionHandle, configHandle, ref enableItem);
            ARConfigEnableItem arconfigEnableItem = (ARConfigEnableItem)enableItem;

            if (arconfig.EnableItem.HasFlag(ARConfigEnableItem.ENABLE_MESH))
            {
                if (!arconfigEnableItem.HasFlag(ARConfigEnableItem.ENABLE_MESH))
                {
                    throw new ARUnSupportedConfigurationException();
                }
            }
            arconfig.EnableItem = arconfigEnableItem;

            int enableSemanticMode = 0;
            NDKAPI.HwArConfig_getSemanticMode(m_ndkSession.SessionHandle, configHandle, ref enableSemanticMode);
            arconfig.SemanticMode = (ARConfigSemanticMode)enableSemanticMode;

        }

        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_create(IntPtr sessionHandle, ref IntPtr outConfigHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_destroy(IntPtr configHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_setArType(IntPtr sessionHandle,
                                       IntPtr configHandle, int arType);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_getArType(IntPtr sessionHandle,
                                           IntPtr configHandle, ref int arType);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_setCameraLensFacing(IntPtr sessionHandle,
                                       IntPtr configHandle, int lensFacing);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_getCameraLensFacing(IntPtr sessionHandle,
                                       IntPtr configHandle, ref int lensFacing);


            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_getLightingMode(IntPtr sessionHandle,
                                       IntPtr configHandle, ref int lightEstimationMode);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_setLightingMode(IntPtr sessionHandle,
                                       IntPtr configHande, int lightEstimationMode);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_getPlaneFindingMode(IntPtr sessionHandle, IntPtr configHandle,
                                    ref int planeFindingMode);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_setPlaneFindingMode(IntPtr sessionHandle, IntPtr configHandle,
                                    int planeFindingMode);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_getUpdateMode(IntPtr sessionHandle, IntPtr configHandle,
                              ref int updateMode);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_setUpdateMode(IntPtr sessionHandle, IntPtr configHandle,
                              int updateMode);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_getPowerMode(IntPtr sessionHandle, IntPtr configHandle,
                  ref int powerMode);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_setPowerMode(IntPtr sessionHandle, IntPtr configHandle,
                              int powerMode);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_getFocusMode(IntPtr sessionHandle, IntPtr configHandle,
                  ref int focusMode);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_setFocusMode(IntPtr sessionHandle, IntPtr configHandle,
                              int focusMode);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_getHandFindingMode(IntPtr sessionHandle, IntPtr configHandle,
                                     ref int mode);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_setHandFindingMode(IntPtr sessionHandle, IntPtr configHandle,
                                     int mode);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_getEnableItem(IntPtr sessionHandle, IntPtr configHandle,
                                     ref ulong item);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_setEnableItem(IntPtr sessionHandle, IntPtr configHandle,
                                     ulong item);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_getAugmentedImageDatabase(IntPtr sessionHandle, IntPtr configHandle,
                                     IntPtr AugImgDatabaseHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_setAugmentedImageDatabase(IntPtr sessionHandle, IntPtr configHandle,
                                     IntPtr AugImgDatabaseHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_setSemanticMode(IntPtr sessionHandle, IntPtr configHandle, int mode);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_getSemanticMode(IntPtr sessionHandle, IntPtr configHandle, ref int mode);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_setImageInputMode(IntPtr sessionHandle, IntPtr configHandle,
                                         int imageInputMode);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_getImageInputMode(IntPtr sessionHandle, IntPtr configHandle,
                                         ref int imageInputMode);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArConfig_setPreviewSize(IntPtr sessionHandle, IntPtr configHandle, int width, int height);
            //Select a supported priview size that is best to fit the target preview size.
            [DllImport(AdapterConstants.NDKAndroidHelper)]
            public static extern void CameraHelper_GetBestPreviewSize([MarshalAs(UnmanagedType.LPStruct)] Vector2Int targetSize, [MarshalAs(UnmanagedType.LPStruct)] ref Vector2Int outSize);
        }
    }
}
