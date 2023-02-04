namespace HuaweiARUnitySDK
{
    using System;
    using HuaweiARInternal;
    using UnityEngine;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    /**
     * \if english
     * @brief Manages AR system state and handles the session lifecycle.
     * 
     * Through this class, application can create a session, configure it, start/pause or stop it. Most importantly, 
     * application can receive the frames which provide the camera image and AR system data.
     * 
     * <b>Note:</b>
     * 
     * Before creating a session, application must firstly confirm that HUAWEI AR Engine is installed and compatiable on 
     * current device. The installation and compatibility can be verified by:
     * \arg Checking \link AREnginesApk.CheckAvailability()\endlink returns SUPPORTED_INSTALLED.
     * \arg Calling \link AREnginesApk.RequestInstall()\endlink returns INSTALLED.
     * \else
     * @brief 管理AR系统的状态和会话的生命周期。
     * 
     * 通过该类，应用可以创建并配置会话，可以启动，暂停和关闭会话。此外，最重要的一点：应用可以通过该类获取到每一帧的相机
     * 预览和AR系统的数据。
     * 
     * <b>注意：</b>
     * 
     * 在创建会话前，应用应该首先确认HUAWEI AR Engine在当前手机上已经安装，并且和应用兼容。安装和兼容性可以通过如下方法确认：
     * \arg \link AREnginesApk.CheckAvailability()\endlink 返回SUPPORTED_INSTALLED。
     * \arg \link AREnginesApk.RequestInstall()\endlink 返回INSTALLED。
     * \endif
     */
    public class ARSession
    {
        /**
         * \if english
         * @brief Create a new ARSession.
         * 
         * Before calling this method, application must firstly confirm that HUAWEI AR Engine is installed 
         * and compatiable on current device. Otherwise, exceptions may throwed.
         * \else
         * @brief 创建一个新的会话。
         * 
         * 调用该方法前，应用应该首先保证引擎已经安装并且兼容，否则，将抛出异常。
         * \endif
         * @exception ARUnavailableServiceNotInstalledException \copybrief ARUnavailableServiceNotInstalledException
         * @exception ARUnavailableServiceApkTooOldException \copybrief ARUnavailableServiceApkTooOldException
         * @exception ARUnavailableDeviceNotCompatibleException \copybrief ARUnavailableDeviceNotCompatibleException
         * @exception ARUnavailableEmuiNotCompatibleException \copybrief ARUnavailableEmuiNotCompatibleException
         */
        public static void CreateSession()
        {
            ARSessionManager.Instance.CreateSession();
        }

        /**
         * \if english
         * @brief Updates the state of AR system.
         * 
         * Applications should call this method on their own initiative to update the state of AR system. This includes
         * receiving a new camera preview, updating the pose of device, etc.
         * 
         * If the \link ARConfigBase.UpdateMode\endlink is set as BLOCKING, this funciton will block the caller thread until a 
         * new camera frame is available or timeout(66ms) is reached. If the \link ARConfigBase.UpdateMode\endlink is set as
         * LATEST_CAMERA_IMAGE, this function will return immediately. And the ARFrame is set a new frame if available
         * or the last frame otherwise.
         * \else
         * @brief 更新AR系统的状态。
         * 
         * 应用应该主动调用该函数，以便刷新AR系统的状态。包括获取新的预览流，更新设备的位姿等。
         * 
         * 如果\link ARConfigBase.UpdateMode\endlink被设置为BLOCKING，该函数将阻塞调用者直至有新的预览可用，或者超时(66ms)。
         * 如果\link ARConfigBase.UpdateMode\endlink被设置为LATEST_CAMERA_IMAGE，该函数将会立刻返回。如果有新的数据可用，ARFrame
         * 将会被刷新，否则，保持为上一帧的状态。
         * \endif
         * @exception ARSessionPausedException \copybrief ARSessionPausedException
         * @exception ARTextureNotSetException \copybrief ARTextureNotSetException
         * @exception ARMissingGlContextException \copybrief ARMissingGlContextException
         */
        public static void Update()
        {
            ARSessionManager.Instance.Update();
        }

        /**
         * \if english
         * @brief Resume the session according to the configuration in \link ARSession.Config(ARConfigBase)\endlink.
         * 
         * This function is used to start a session or resume a paused session.<b>Note: after calling
         * \link ARSession.Stop()\endlink, this session cannot be resumed.</b>
         * \else
         * @brief 根据配置启动会话。
         * 
         * 该函数可以用于启动或者恢复会话。<b>注意：当\link ARSession.Stop()\endlink 被调用之后，会话将不可恢复。</b>
         * \endif
         * @exception ARCameraPermissionDeniedException \copybrief ARCameraPermissionDeniedException
         */
        public static void Resume()
        {
            ARSessionManager.Instance.Resume();
        }

        /**
         * \if english
         * @brief Pause the session.
         * 
         * This method will stop the camera feed and release camera. The resource of AR system will not be released, so that
         * it cloud be restarted again by calling \link Resume()\endlink.
         * \else
         * @brief 暂停会话。
         * 
         * 该方法将停止预览流，并关闭相机。AR系统的资源将不会释放，因此可以通过\link Resume()\endlink恢复。
         * \endif
         */
        public static void Pause()
        {
            ARSessionManager.Instance.Pause();
        }

        /**
         * \if english
         * @brief Stop the session.
         * 
         * This method will stop the camera and release camera. The resource of AR system will be released, so that 
         * it can't be resumed.To restart, you need a new session.
         * \else
         * @brief 停止会话。
         * 
         * 该方法将停止预览流，并关闭相机。AR系统的资源将被释放，并且该会话不可恢复。如果要重启，需要新建会话。 
         * \endif
         */
        public static void Stop()
        {
            ARSessionManager.Instance.Stop();
        }

        /**
         * \if english
         * @brief Set the camera texture name automatically.
         * 
         * This method generates an external texture, which is used to fill with camera preview, and sets it to HUAWEI AR Engine.
         * Application can access the preview through \link ARFrame.CameraTexture\endlink.
         * \else
         * @brief 自动设置纹理。
         * 
         * 该方法将自动产生一个用于填充相机预览的外部纹理，并将该纹理提供给AR引擎。应用可以通过\link ARFrame.CameraTexture\endlink
         * 访问预览流。
         * \endif
         */
        public static void SetCameraTextureNameAuto()
        {
            ARSessionManager.Instance.SetCameraTextureName();
        }

        /**
         * \if english
         * @brief Set the display geometry.
         * @param width Width of display geometry in pixel.
         * @param height Height of display geometry in pixel.
         * \else
         * @brief 设置显示的宽高。
         * @param width 显示的宽（以像素为单位）。
         * @param height 显示的高（以像素为单位）。
         * \endif
         */
        public static void SetDisplayGeometry(int width, int height)
        {
            ARSessionManager.Instance.SetDisplayGeometry(width, height);
        }

        /**
         * \if english
         * @brief Set the display geometry.
	     * @param orientation orientation of display geometry.
         * @param width Width of display geometry in pixel.
         * @param height Height of display geometry in pixel.
         * \else
         * @brief 设置显示的旋转和宽高。
	     * @param width 显示的旋转角度。
         * @param width 显示的宽（以像素为单位）。
         * @param height 显示的高（以像素为单位）。
         * \endif
         */
        public static void SetDisplayGeometryForVideoMode(ScreenOrientation orientation,int width, int height)
        {
            ARSessionManager.Instance.SetDisplayGeometry(orientation,width, height);
        }

        /**
         * \if english
         * @brief Add tracking anchor.
         * @param pose Application specified pose. 
         * @exception ARResourceExhaustedException if too many anchors exist.
         * \else
         * @brief 添加跟踪的Anchor。
         * @param pose 用户指定的位置。
         * @exception ARResourceExhaustedException 如果添加的锚点过多。
         * \endif
         */
        public static ARAnchor AddAnchor(Pose pose)
        {
            ARAnchor anchor = ARSessionManager.Instance.AddAnchor(pose);
            return anchor;
        }

        /**
         * \if english
         * @brief Configures the session.
         * 
         * The default configuration of session is \link ARWorldTrackingConfig \endlink. Application can change this
         * as their need and call Resume() after configuring.
         * 
         * <b>Note: </b>
         * \arg The enable item, such as \link ARConfigBase.EnableDepth \endlink and 
         * \link ARConfigBase.EnableMask \endlink, may be changed after calling this function. Application should check 
         * these items to find which item is actually enabled.
         * \arg Currently, HUAWEI AR Engine does not support to change the configuration once Resume() is called. 
         * Alternatively, application can stop the old session, create a new one and config it with other configurations. 
         * @param config Application specified configuration.
         * \else
         * @brief 配置会话。
         * 
         * 默认的配置是\link ARWorldTrackingConfig \endlink。应用可以根据自己的需求选择。
         * 
         * <b>注意：</b>
         * \arg 调用该函数后，配置中的使能项，例如\link ARConfigBase.EnableDepth \endlink和
         * \link ARConfigBase.EnableMask \endlink 可能会被改变。应用应该使用后，检查关心的选项以便确认真正的使能项。
         * \arg 目前，HUAWEI AR Engine不支持在 Resume()后动态改变使用的配置。应用可以通过关闭之前的会话，开启新的会话后重新
         * 配置。
         * @param config 应用指定的配置。
         * \endif
         * @exception ARUnSupportedConfigurationException \copybrief ARUnSupportedConfigurationException
         */
        public static void Config(ARConfigBase config)
        {
            ARSessionManager.Instance.Config(config);
        }

        /**
         * \if english
         * @deprecated This method always returns true.
         * \else
         * @deprecated 该方法将始终返回true。
         * \endif
         */
        [Obsolete]
        public static bool IsSupported(ARConfigBase config)
        {
            return true;
        }

        /**
         * \if english
         * @deprecated Use Config(ARConfigBase) and Resume() instead.
         * \else
         * @deprecated 请使用Config(ARConfigBase) 和 Resume()。
         * \endif
         */
        [Obsolete]
        public static void Resume(ARConfigBase config)
        {
            Config(config);
            Resume();
        }

        /**
         * \if english
         * @deprecated Use \link ARAnchor.Detach()\endlink instead.
         * \else
         * @deprecated 请使用\link ARAnchor.Detach()\endlink。
         * \endif
         */
        [Obsolete]
        public static void RemoveAnchors(List<ARAnchor> anchors)
        {
            if (anchors == null)
            {
                throw new ArgumentNullException();
            }
            foreach(ARAnchor anchor in anchors)
            {
                anchor.Detach();
            }
        }

        /**
         * \if english
         * @deprecated Use \link ARFrame.GetAnchors() \endlink.
         * \else
         * @deprecated 请使用\link ARFrame.GetAnchors()\endlink。
         * \endif
         */
        [Obsolete]
        public static List<ARAnchor> GetAllAnchors()
        {
            return ARFrame.GetAnchors(ARTrackableQueryFilter.ALL);
        }

        /**
         * \if english
         * @brief Get the projection matrix.
         * @param nearClipPlane Near clip plane of the projection matrix.
         * @param farClipPlane Far clip plane of the projection matrix.
         * \else
         * @brief 获取投影矩阵。
         * @param nearClipPlane 近裁剪平面。
         * @param farClipPlane 远裁剪平面。
         * \endif
         */
        public static Matrix4x4 GetProjectionMatrix(float nearClipPlane, float farClipPlane)
        {
            return ARSessionManager.Instance.GetProjectionMatrix(nearClipPlane, farClipPlane); ;
        }

        ///@cond EXCLUDE_DOXYGEN
        ///@deprecated Use ARFrame.GetPlanes() instead.
        [Obsolete]
        public static List<ARPlane> GetAllPlanes()
        {
            return ARFrame.GetPlanes(ARTrackableQueryFilter.ALL); ;
        }
        ///@endcond

        //uncomment the interface manual if it's ready.
        ///@cond ContainShareAR
        /***
         * \if english
         * @brief save the whole share data.
         * @bug Not available yet.
         * \else
         * @brief 获取当前的共享数据。
         * @bug 目前不可用。
         * \endif
         */
        public static ARSharedData SaveSharedData()
        {
            return ARSessionManager.Instance.m_ndkSession.SessionAdapter.SaveSharedData();
        }

        public static void LoadSharedData(ARSharedData sharedData)
        {
            if (null == sharedData || sharedData.RawData.DataSize < 0)
            {
                throw new ArgumentException();
            }
            ARSessionManager.Instance.m_ndkSession.SessionAdapter.LoadSharedData(sharedData);
        }

        public static ARSharedData SerializeAnchors(List<ARAnchor> anchors, bool isNeedAlign)
        {
            return ARSessionManager.Instance.m_ndkSession.SessionAdapter.SerializeAnchors(anchors, isNeedAlign);
        }

        public static void DeSerializeAnchors(ARSharedData sharedData, List<ARAnchor> anchors)
        {
            if (null == sharedData || sharedData.RawData.DataSize < 0 || null == anchors)
            {
                throw new ArgumentException();
            }
            anchors.Clear();
            ARSessionManager.Instance.m_ndkSession.SessionAdapter.DeSerializeAnchors(sharedData, anchors);
        }
        ///@endcond
        /**
         * \if english
         * @brief Get camera configuration.
         * 
         * Currently, HUAWEI AR Engine does not support receiving specified camera configuration.
         * \else
         * @brief 获取当前的相机配置。
         * 
         * 目前，HUAWEI AR Engine不支持修改相机配置。
         * \endif
         */
        public static ARCameraConfig GetCameraConfig()
        {
            return ARSessionManager.Instance.m_ndkSession.SessionAdapter.GetCameraConfig();
        }

        /**
         * \if english
         * @brief Get supported semantic mode.
         * \else
         * @brief 获取当前支持的语义识别类型。。
         * \endif
         */
        public static int GetSupportedSemanticMode()
        {
            return ARSessionManager.Instance.m_ndkSession.SessionAdapter.GetSupportedSemanticMode();
        }

        public static void AddServiceListener(MonitorServiceCallback callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                ARSessionManager.Instance.m_ndkSession.SessionAdapter.SetNotifyDataCallback(callback);
            }
        }

        public static void SetCloudServiceAuthInfo(AuthInfo authInfo)
        {
            if (authInfo == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                string info = JsonUtility.ToJson(authInfo);
                ARSessionManager.Instance.m_ndkSession.SessionAdapter.SetCloudServiceAuthInfo(info);
            }
        }
    }
}
