namespace HuaweiARUnitySDK
{
    using System;
    using HuaweiARInternal;
    using UnityEngine;
    /**
     * \if english
     * @brief ARAnchor describes a fixed location and orientation in the real world.
     * 
     * To stay at a fixed location in physical space, the numerical description of 
     * this position will update as HUAWEI AREngine's understanding of the space improves.
     * \else 
     * @brief ARAnchor是真实世界中的一个固定的位置和方向。
     * 
     * 为了保持这个物理空间的固定位置，HUAWEI AR Engine会根据自己对于空间的理解，更新它的数值。
     * \endif
     */
    public class ARAnchor
    {
        internal  IntPtr m_anchorHandle = IntPtr.Zero;
        private NDKSession m_ndkSession;
        //this method must be called in ARAnchorMaager
        internal ARAnchor(IntPtr anchorHandle,NDKSession session)
        {
            m_anchorHandle = anchorHandle;
            m_ndkSession = session;
        }
        /**
         * \if english
         * @brief Gets the \c Pose of the anchor in the unity coordinate space.
         * 
         * This \c Pose may change each time \link ARSession.Update() \endlink is called.
         * @return The \c Pose value of this anchor. And it should only be used for rendering if 
         * \link GetTrackingState() \endlink returns \link ARTrackable.TrackingState.TRACKING \endlink.
         * \else 
         * @brief 获取锚点在Unity世界坐标系的位姿。
         * 
         * 该\c Pose在 ARSession.Update()  时可能会被更新。
         * @return 当前Anchor的位姿。当且仅当 GetTrackingState() 返回值为
         *  ARTrackable.TrackingState.TRACKING 时，应用才应该使用该位姿。
         * \endif
         */
        public Pose GetPose()
        {
            return m_ndkSession.AnchorAdapter.GetPose(m_anchorHandle);
        }


        ///@cond EXCLUDE_FROM_DOXYGEN
        [Obsolete]
        public Pose GetPoseInUnity()
        {
            return GetPose();
        }
        ///@endcond 

        /**
         * \if english
         * @brief Get the trackingstate of this anchor.
         * @return Tracking state of this anchor.
         * \else
         * @brief 获取anchor的跟踪状态。
         * @return anchor的跟踪状态
         * \endif
         */
        public ARTrackable.TrackingState GetTrackingState()
        {
            return m_ndkSession.AnchorAdapter.GetTrackingState(m_anchorHandle);
        }
        /**
         * \if english 
         * @brief Detaches this Anchor from its \link ARTrackable\endlink and removes it from the \link ARSession \endlink.
         * 
         * After calling this method, \link ARAnchor.GetTrackingState() \endlink will return 
         * \link ARTrackable.TrackingState.STOPPED \endlink.
         * \else
         * @brief 通知引擎停止跟踪当前锚点。
         * 
         * 调用该方法后，调用\link ARAnchor.GetTrackingState() \endlink 将会返回
         * \link ARTrackable.TrackingState.STOPPED \endlink 。
         * \endif
         */
        public void Detach()
        {
            m_ndkSession.AnchorManager.RemoveAnchor(this);
            m_ndkSession.AnchorAdapter.Detach(m_anchorHandle);
        }
        /**
         * \if english 
         * @brief Indicates whether some other object is an ARAnchor referencing the same logical anchor as this one.
         * @param obj An object in C\#.
         * @return \c true if \c obj references to the same logical anchor as this one. Otherwise, \c false.
         * \else
         * @brief 两个ARAnchor对象可能对应同一个真实世界的锚点，使用该方法可以比较是否对应同一个锚点。
         * @param obj C\#中的对象。
         * @return 若\c obj 与当前anchor表示的是同一个真实世界中的锚点时，返回\c true。否则，返回\c false。
         * \endif
         */
        public override bool Equals(object obj)
        {
            if (obj != null && obj is ARAnchor)
            {
                ARAnchor other = (ARAnchor)obj;
                return m_anchorHandle==other.m_anchorHandle;
            }
            else
            {
                return false;
            }
        }
        /**
         * \if english 
         * @brief Get the hashcode of this anchor.
         * @return Hashcode of this anchor.
         * \else
         * @brief 获取当前anchor的hashcode.
         * @return 当前anchor的hashcode.
         * \endif
         */
        public override int GetHashCode()
        {
            return m_anchorHandle.ToInt32();
        }

        ~ARAnchor()
        {
            //release resouce here
            m_ndkSession.AnchorAdapter.Release(m_anchorHandle);
        }
    }
}
