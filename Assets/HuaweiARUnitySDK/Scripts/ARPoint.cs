namespace HuaweiARUnitySDK
{
    using System;
    using HuaweiARInternal;
    using UnityEngine;
    using System.Collections.Generic;

    /**
     * \if english
     * @brief A point in the real world tracked by HUAWEI AR Engine.
     * 
     * These objects are created as a side-effect of \link ARSession.AddAnchor(Pose)\endlink or when 
     * \link ARFrame.HitTest()\endlink returns results against a point cloud.
     * \else
     * 
     * @brief HUAWEI AR Engine跟踪的真实世界中的点。
     * 
     * 该对象通常是由\link ARSession.AddAnchor(Pose)\endlink 或者\link ARFrame.HitTest()\endlink返回的与点云的碰撞结果产生。
     * \endif
     */
    public class ARPoint: ARTrackable
    {
        /**
         * \if english
         * @brief Enumeration of point orientation mode.
         * \else
         * @brief 点的朝向模式的枚举。
         * \endif
         */
        public enum OrientationMode
        {
            /**
             * \if english
             * The point orientation mode is initialized to identity but may adjust slightly over time.
             * \else
             * 点的朝向模式是单位，与世界坐标系一致，但可能会随着时间调整。
             * \endif
             */
            INITIALIZED_TO_IDENTITY =0,

            /**
             * \if english
             * The point orientation mode is set as the normal of estimated surface nearby.
             * \else
             * 点的朝向模式与附近估计的平面法向量一致。
             * \endif
             */
            ESTIMATED_SURFACE_NORMAL = 1,
        }


        internal ARPoint(IntPtr trackableHandle, NDKSession session) : base(trackableHandle, session) { }

        /**
         * \if english
         * @brief Get the pose of this point in unity world coordinate.
         * \else
         * @brief 获取当前点在世界坐标系中的位姿。
         * \endif
         */
        public Pose GetPose()
        {
            return m_ndkSession.PointAdapter.GetPose(m_trackableHandle);
        }
        
        /**
         * \if english
         * @brief Get the orientation mode of this point.
         * \else
         * @brief 获取当前点的朝向模式。
         * \endif
         */
        public OrientationMode GetOrientationMode()
        {
            return m_ndkSession.PointAdapter.GetOrientationMode(m_trackableHandle);
        }
    }
}
