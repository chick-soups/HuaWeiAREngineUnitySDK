namespace HuaweiARUnitySDK
{
    using HuaweiARInternal;
    using System;
    using UnityEngine;
    using System.Collections.Generic;

    /**
     * \if english
     * @brief Provides a snapshot of HUAWEI AREngine at \link GetTimestampNs() \endlink associated with current frame.
     * 
     * ARFrame holds information about HUAWEI AREngine's tracking state, device's pose in the world coordinate,
     * ligh estimation, trackables, anchors, etc..
     * \else
     * @brief  HUAWEI AREngine当前帧的快照。
     * 
     * ARFrame持有HUAWEI AREngine的跟踪状态，设备位置，光照估计参数，trackables，锚点以及其他信息。
     * \endif
     */
    public class ARFrame
    {
        /**
         * \if english
         * @brief Check whether the texture of camera preview is available.
         * \else
         * @brief 检查预览流的贴图是否可用。
         * \endif
         */
        public static bool TextureIsAvailable()
        {
            return ARSessionManager.Instance.SessionStatus == ARSessionStatus.RUNNING;
        }

        /**
         * \if english
         * @brief Texture of camera preview.
         * \else
         * @brief 预览流的贴图。
         * \endif
         */
        public static Texture2D CameraTexture
        {
            get
            {
                return ARSessionManager.Instance.BackgroundTexture;
            }
        }
        /**
         * \if english
         * @brief Get the transform display uv coordinate.
         * 
         * Since the size of camera preview may be not compatible with screen, application should adopt
         * this method to get the transform display uv coordinate in order to avoid stretch or shrink the 
         * original preview.
         * 
         * @param inUVCoords The uv coordinates to transform. This coordinate array's length must be 8, with 
         * the format of [x0,y0,x1,y1,...]。
         * @return Transformed uv coordinates.
         * \else
         * @brief 获取显示的uv贴图坐标。
         * 
         * 由于预览流的比例可能与屏幕不一致，应用应该使用该方法获取显示的uv贴图坐标以防止显示的拉伸。
         * 
         * @param inUVCoords 原始的贴图坐标。该数组长度需要为8，格式为[x0,y0,x1,y1,...]。
         * @return 转换后的贴图坐标。
         * \endif
         * @exception ArgumentException
         * @exception ARNotYetAvailableException \copybrief ARNotYetAvailableException
         */
        public static float[] GetTransformDisplayUvCoords(float[] inUVCoords)
        {
            if (null == inUVCoords || inUVCoords.Length != 8)
            {
                ARDebug.LogError("wrong inUVCoords");
                throw new ArgumentException("inUVCoords is wrong");
            }
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
                    ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                throw new ARNotYetAvailableException();
            }

            float[] outUv = ARSessionManager.Instance.m_ndkSession.FrameAdapter.TransformDisplayUvCoords(inUVCoords);
            return outUv;
        }

        /**
         * \if english
         * @brief Get the pose of device in unity world coordinate.
         * @return Pose of device.
         * \else
         * @brief 获取设备在unity世界坐标系的位姿。
         * @return 设备的位姿。
         * \endif
         */
        public static Pose GetPose()
        {
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
                    ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                return Pose.identity;
            }

            IntPtr cameraPtr = ARSessionManager.Instance.m_ndkSession.FrameAdapter.AcquireCameraHandle();
            Pose p = ARSessionManager.Instance.m_ndkSession.CameraAdapter.GetPose(cameraPtr);
            ARSessionManager.Instance.m_ndkSession.CameraAdapter.Release(cameraPtr);
            return p;
        }

        /**
         * \if english
         * @brief Get the light estimation of current frame.
         * @return Light estimation of current frame.
         * \else
         * @brief 获取当前帧的光照估计。
         * @return 当前帧的光照估计。
         * \endif
         */
        public static ARLightEstimate GetLightEstimate()
        {
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
                    ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                return new ARLightEstimate(false, 1.0f);
            }
            ARLightEstimate lightEstimate = ARSessionManager.Instance.m_ndkSession.FrameAdapter.GetLightEstimate();
            return lightEstimate;
        }

        /**
         * \if english
         * @brief Get the point cloud of current frame.
         * @return Point cloud of current frame.
         * \else
         * @brief 获取当前帧的点云。
         * @return 当前帧的点云。
         * \endif
         * @exception ARNotYetAvailableException \copybrief ARNotYetAvailableException
         * @exception ARResourceExhaustedException \copybrief ARResourceExhaustedException
         */
        public static ARPointCloud AcquirePointCloud()
        {
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
                    ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                throw new ARNotYetAvailableException();
            }

            IntPtr pointcloudHandle = ARSessionManager.Instance.m_ndkSession.FrameAdapter.AcquirePointCloudHandle();
            ARPointCloud pointCloud = new ARPointCloud(pointcloudHandle, ARSessionManager.Instance.m_ndkSession);
            return pointCloud;
        }

        /**
         * \if english
         * @brief Get timestamp of current frame in nanosecond.
         * @return Timestamp of current frame.
         * \else
         * @brief 获取当前帧的时戳（纳秒级）。
         * @return 当前帧的时戳。
         * \endif
         */
        public static long GetTimestampNs()
        {
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
                    ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                return 0;
            }

            long timestamp = ARSessionManager.Instance.m_ndkSession.FrameAdapter.GetTimestamp();
            return timestamp;
        }

        /**
         * \if english
         * @brief Get tracking state of current frame.
         * @return Tracking state of current frame.
         * \else
         * @brief 获取当前帧的跟踪状态。
         * @return 当前帧的跟踪状态。
         * \endif
         */
        public static ARTrackable.TrackingState GetTrackingState()
        {
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
                ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                return ARTrackable.TrackingState.STOPPED;
            }
            IntPtr cameraPtr = ARSessionManager.Instance.m_ndkSession.FrameAdapter.AcquireCameraHandle();
            ARTrackable.TrackingState state = ARSessionManager.Instance.m_ndkSession.CameraAdapter.GetTrackingState(cameraPtr);
            ARSessionManager.Instance.m_ndkSession.CameraAdapter.Release(cameraPtr);
            return state;
        }

        /**
         * \if english
         * @brief Get the list of anchors with specified filter.
         * @param filter Query filter.
         * @return The list of anchor.
         * \else
         * @brief 根据指定的参数，获取锚点。
         * @param filter 查询过滤器。
         * @return 锚点的集合。
         * \endif
         */
        public static List<ARAnchor> GetAnchors(ARTrackableQueryFilter filter)
        {
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
                    ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                return new List<ARAnchor>();
            }

            switch (filter)
            {
                case ARTrackableQueryFilter.ALL:
                    List<ARAnchor> anchors = new List<ARAnchor>();
                    ARSessionManager.Instance.m_ndkSession.AnchorManager.GetAllAnchor(anchors);
                    return anchors;
                case ARTrackableQueryFilter.UPDATED:
                    List<ARAnchor> anchorList = new List<ARAnchor>();
                    ARSessionManager.Instance.m_ndkSession.FrameAdapter.GetUpdatedAnchors(anchorList);
                    return anchorList;
                case ARTrackableQueryFilter.NEW:
                default:
                    return new List<ARAnchor>();
            }
        }

        /**
         * \if english
         * @brief Get the list of trackables with specified filter.
         * @param[out] trackableList A list where the returned trackable stored. The previous values will be cleared.
         * @param filter Query filter.
         * \else
         * @brief 根据过滤器获取ARTrackable.
         * @param[out] trackableList 获取到的ARTrackable集合。该集合之前存储的值将被清除。
         * @param filter 查询过滤器。
         * \endif
         */
        public static void GetTrackables<T>(List<T> trackableList, ARTrackableQueryFilter filter) where T : ARTrackable
        {
            if (trackableList == null)
            {
                throw new ArgumentException();
            }
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
                    ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                trackableList.Clear();
                return;
            }
            ARSessionManager.Instance.m_ndkSession.TrackableManager.GetTrackables<T>(trackableList, filter);
        }

        /**
         * \if english
         * @brief  Same as \link HitTest(float,float)\endlink.
         * @param touch Unity touch on screen.
         * @return A list of hit result.
         * \else
         * @brief 与 \link HitTest(float,float)\endlink一致。
         * @param touch unity中的屏幕点击。
         * @return 碰撞结果列表。
         * \endif
         */
        public static List<ARHitResult> HitTest(Touch touch)
        {
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
                    ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                return new List<ARHitResult>();
            }

            List<ARHitResult> res = HitTest(touch.position.x, touch.position.y);
            return res;
        }

        /**
         * \if english
         * @brief Performs a ray cast from the user's device in the direction of the given location in the camera view.
         * 
         * Intersections with detected scene geometry are returned, sorted by distance from the device; 
         * the nearest intersection is returned first. <b>Note: This methods only works in worldAR.</b>
         * @param xPx x coordinate in pixels.
         * @param yPx y coordinate in pixels.
         * @return A list of hit result.
         * \else
         * @brief 从手机发射一条射线，指向用户在屏幕中选择的预览的方向。
         * 
         * 返回与场景中识别的物体的交点。该返回值按照距离排序，距离最近的点最靠前。<b>注意：该方法仅在worldAR中有效</b>。
         * @param xPx 像素点的x坐标。
         * @param yPx 像素点的y坐标。
         * @return 碰撞结果列表。
         * \endif
         */
        public static List<ARHitResult> HitTest(float xPx, float yPx)
        {
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
                ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                return new List<ARHitResult>();
            }
            List<ARHitResult> results = new List<ARHitResult>();
            ARSessionManager.Instance.m_ndkSession.FrameAdapter.HitTest(xPx, Screen.height - yPx, results);
            return results;
        }

        /**
         * \if english
         * @brief Get the hit results by multiple points，in order to improve accuracy.
         * @param input2DPoints the coordinates of the input 2D points.Format is(x0,y0,x1,y1...)
         * @return The hits result of input points, correspond to the input points one by one, and the hit result of each point is an ARPoint.
         * \else
         * @brief 获取多个点的碰撞结果，用于提高准确性。
         * @param input2DPoints 输入的2D点的坐标，格式为(x0,y0,x1,y1...)。
         * @return 碰撞结果列表，与输入点一一对应，每个点的碰撞结果都是一个ARPoint。
         * \endif
         */
        public static List<ARHitResult> HitTestArea(float[] input2DPoints)
        {
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
                ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                return new List<ARHitResult>();
            }
            List<ARHitResult> results = new List<ARHitResult>();
            ARSessionManager.Instance.m_ndkSession.FrameAdapter.HitTestArea(input2DPoints, results);
            return results;
        }

        /**
         * \if english
         * @brief Checks whether the display geomtery is changed since last frame.
         * 
         * This method will return true if the width or height of the display view changes when 
         * \link ARSession.SetDisplayGeometry() \endlink is called. If this method returns true, application
         * should call \link GetTransformDisplayUvCoords()\endlink to get the new uv coordinates.
         * \else
         * @brief 检查与上一帧相比，显示的几何结构是否发生了变化。
         * 
         * 如果使用\link ARSession.SetDisplayGeometry() \endlink调整显示，该方法将会返回true。此时，应用应该使用
         * \link GetTransformDisplayUvCoords()\endlink重新获取贴图坐标。
         * \endif
         */
        public static bool IsDisplayGeometryChanged()
        {
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
                ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                return false;
            }
            if (ARSessionManager.Instance.DisplayGeometrySet)
            {
                ARSessionManager.Instance.DisplayGeometrySet = false;
                return true;
            }
            bool res = ARSessionManager.Instance.m_ndkSession.FrameAdapter.GetDisplayGeometryChanged();
            return res;
        }

        /**
         * \if english
         * @brief Get camera metadata.
         * @exception ARNotYetAvailableException \copybrief ARNotYetAvailableException
         * \else
         * @brief 获取相机元数据。
         * @exception ARNotYetAvailableException \copybrief ARNotYetAvailableException
         * \endif
         */
        public static ARCameraMetadata GetCameraMetadata()
        {
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
                ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                throw new ARNotYetAvailableException();
            }
            IntPtr metadataPtr = ARSessionManager.Instance.m_ndkSession.FrameAdapter.AcquireImageMetadata();
            return new ARCameraMetadata(metadataPtr, ARSessionManager.Instance.m_ndkSession);
        }

        /**
         * \if english
         * @brief Acquire camera image bytes.
         * 
         * This method provides a way for application to access the image on CPU. The format of image is 
         * YUV-420-888.
         * \else
         * @brief 获取相机图片。
         * 
         * 该方法为应用提供了一种访问相机的CPU图像的途径。图像的格式是YUV-420-888。
         * \endif
         * @exception ARNotYetAvailableException \copybrief ARNotYetAvailableException
         */
        public static ARCameraImageBytes AcquireCameraImageBytes()
        {
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
                ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                throw new ARNotYetAvailableException();
            }
            IntPtr imagePtr = ARSessionManager.Instance.m_ndkSession.FrameAdapter.AcquireCameraImage();
            return new ARCameraImageBytes(imagePtr, ARSessionManager.Instance.m_ndkSession);
        }
        /**
         * \if english
         * @brief Get the depth image of the current frame.
         * @return the object of the current frame depth image.
         * \else
         * @brief 获取当前帧的深度图。
         * @return 当前帧深度图的对象。
         * \endif
         * @exception ARNotYetAvailableException \copybrief ARNotYetAvailableException
         */
        public static ARCameraImageBytes AcquireDepthImageBytes()
        {
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
                ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                throw new ARNotYetAvailableException();
            }
            IntPtr imagePtr = ARSessionManager.Instance.m_ndkSession.FrameAdapter.AcquireDepthImage();
            return new ARCameraImageBytes(imagePtr, ARSessionManager.Instance.m_ndkSession);
        }

        ///@cond ContainShareAR
        /**
         * \if english
         * @brief Get the world mapping status of current frame.
         * 
         * See \link ShareMap \endlink for more details.
         * \else
         * @brief 获取当前帧的共享地图状态。
         * 
         * 请参考\link 共享地图\endlink。
         * \endif
         */
        public static ARWorldMappingState GetWorldMappingStatus()
        {
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
                ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                return ARWorldMappingState.NOT_AVAILABLE;
            }
            return ARSessionManager.Instance.m_ndkSession.FrameAdapter.GetMappingState();
        }
        ///@endcond

        ///@cond ContainShareAR
        /**
         * \if english
         * @brief Get the align state of world map in current frame.
         * 
         * See \link Share Map \endlink for more details.
         * \else
         * @brief 获取共享地图在当前帧的对齐状态。
         * 
         * 请参考\link 共享地图\endlink 功能。
         * \endif
         */
        public static ARAlignState GetAlignState()
        {
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
                ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                return ARAlignState.NONE;
            }
            return ARSessionManager.Instance.m_ndkSession.FrameAdapter.GetAlignState();
        }

        ///@endcond

        ///@cond ContainSecneMesh
        /**
         * \if english
         * @brief Get the mesh of scene in current frame.
         * 
         * 
         * \else
         * @brief 获取当前帧的场景mesh。
         * 
         * 
         * \endif
         */
        public static ARSceneMesh AcquireSceneMesh()
        {
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
                ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                throw new ARNotYetAvailableException();
            }
            IntPtr sceneMeshHandle = ARSessionManager.Instance.m_ndkSession.FrameAdapter.AcquireSceneMesh();
            ARSceneMesh arSceneMesh = new ARSceneMesh(sceneMeshHandle, ARSessionManager.Instance.m_ndkSession);
            return arSceneMesh;
        }
        ///@endcond
        /**
         * \if english 
         * @deprecated use \link GetPose()\endlink instead.
         * \else
         * @deprecated 请使用\link GetPose()\endlink。
         * \endif
         */
        [Obsolete]
        public static Pose GetPoseInUnity()
        {
            return GetPose();
        }
        /**
         * \if english 
         * @deprecated use \link GetTrackables<T>()\endlink instead.
         * \else
         * @deprecated 请使用\link GetTrackables<T>()\endlink。
         * \endif
         */
        [Obsolete]
        public static List<ARPlane> GetPlanes(ARTrackableQueryFilter filter)
        {
            List<ARPlane> planeList = new List<ARPlane>();
            GetTrackables<ARPlane>(planeList, filter);
            return planeList;
        }
        /**
         * \if english 
         * @deprecated use \link GetTrackables<T>()\endlink instead.
         * \else
         * @deprecated 请使用\link GetTrackables<T>()\endlink。
         * \endif
         */
        [Obsolete]
        public static List<ARPlane> GetUpdatedPlanes()
        {
            return GetPlanes(ARTrackableQueryFilter.UPDATED);
        }
        /**
         * \if english 
         * @deprecated use \link GetAnchors()\endlink instead.
         * \else
         * @deprecated 请使用\link GetAnchors()\endlink。
         * \endif
         */
        [Obsolete]
        public static List<ARAnchor> GetUpdatedAnchors()
        {
            return GetAnchors(ARTrackableQueryFilter.UPDATED);
        }

        /**
         * \if english 
         * @deprecated use \link GetPose()\endlink instead.
         * \else
         * @deprecated 请使用\link GetPose()\endlink。
         * \endif
         */
        [Obsolete]
        public static Matrix4x4 GetViewMatrix()
        {
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
                ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                return Matrix4x4.identity;
            }

            Pose p = GetPose();
            return Matrix4x4.TRS(p.position, p.rotation, Vector3.one).inverse;
        }

        /**
         * \if english 
         * @deprecated use \link IsDisplayGeometryChanged()\endlink instead.
         * \else
         * @deprecated 请使用\link IsDisplayGeometryChanged()\endlink。
         * \endif
         */
        [Obsolete]
        public static bool IsDisplayRotationChanged()
        {
            return IsDisplayGeometryChanged();
        }

        /**
         * \if english 
         * @deprecated use \link AcquirePointCloud()\endlink instead.
         * \else
         * @deprecated 请使用\link AcquirePointCloud()\endlink。
         * \endif
         */
        [Obsolete]
        public static ARPointCloud GetPointCloud()
        {
            return AcquirePointCloud();
        }

        public static ARCamera GetCamera()
        {
            if (ARSessionManager.Instance.SessionStatus != ARSessionStatus.RUNNING &&
              ARSessionManager.Instance.SessionStatus != ARSessionStatus.PAUSED)
            {
                throw new ARNotYetAvailableException();
            }
            IntPtr cameraPtr = ARSessionManager.Instance.m_ndkSession.FrameAdapter.AcquireCameraHandle();
            return new ARCamera(cameraPtr, ARSessionManager.Instance.m_ndkSession.CameraAdapter);
        }
    }
}
