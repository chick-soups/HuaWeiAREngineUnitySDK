namespace HuaweiARUnitySDK
{
    using HuaweiARInternal;
    using System;
    using UnityEngine;
    using System.Collections.Generic;

    /**
     * \if english
     * @brief Defines an intersection between a ray and estimated real-world geometry.
     * \else
     * @brief 定义用户\link ARFrame.HitTest()\endlink和真实世界中结构的交点。
     * \endif
     */
    public class ARHitResult
    {
        private NDKSession m_ndkSession;
        internal IntPtr m_hitResultHandle;

        internal ARHitResult(IntPtr hitResultHandle,NDKSession session)
        {
            m_hitResultHandle = hitResultHandle;
            m_ndkSession = session;
        }

        ~ARHitResult()
        {
            if (m_hitResultHandle != IntPtr.Zero)
            {
                m_ndkSession.HitResultAdapter.Destroy(m_hitResultHandle);
            }
        }
        
        /**
         * \if english
         * @brief Gets the pose where the raycast hit the object in unity wolrd coordinates.
         * \else
         * @brief 碰撞交点的在Unity世界坐标系中的位姿。
         * \endif
         */
        public Pose HitPose
        {
            get
            {
               return  m_ndkSession.HitResultAdapter.GetHitPose(m_hitResultHandle);
            }
        }

       /**
        * \if english
        * @brief Gets the distance from the origin of the ray to the intersection.
        * \else
        * @brief 获取碰撞点和设备之间的距离。
        * \endif
        */
        public float Distance
        {
            get
            {
                return m_ndkSession.HitResultAdapter.GetDistance(m_hitResultHandle);
            }
        }

        /**
         * \if english
         * @brief Gets the trackable where the intersection is located.
         * \else 
         * @brief 获取当前交点所处的Trackable。
         * \endif
         */
        public ARTrackable GetTrackable()
        {
            return m_ndkSession.HitResultAdapter.AcquireTrackable(m_hitResultHandle);
        }

        /**
         * \if english
         * @brief Create an anchor at the intersection. This anchor will bind to the Trackable where the 
         * intersection located.
         * \else
         * @brief 在交点上创建锚点，该锚点将自动和交点所处的Trackable绑定。
         * \endif
         */
        public ARAnchor CreateAnchor()
        {
            return m_ndkSession.HitResultAdapter.AcquireNewAnchor(m_hitResultHandle);
        }

        /**
         * \if english
         * @deprecated use \link HitPose\endlink instead.
         * \else
         * @deprecated 请使用\link HitPose\endlink。
         * \endif
         */
        [Obsolete]
        public Pose PoseInUnity { get { return HitPose; } }
    }
}
