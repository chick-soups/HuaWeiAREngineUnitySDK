namespace HuaweiARUnitySDK
{
    using System;
    using UnityEngine;
    using HuaweiARInternal;
    using System.Collections.Generic;

    /**
     * \if english
     * @brief An abstract class used to represent all kinds of trackable detected and tracked by HUAWEI AR Engine.
     * \else
     * @brief 抽象类，代表了HUAWEI AR Engine的识别跟踪的trackable。
     * \endif
     */
    public abstract class ARTrackable
    {
        /**
         * \if english
         * @brief Possible tracking state for HUAWEI AR Engine.
         * \else
         * @brief HUAWEI AR Engine可能的跟踪状态。
         * \endif
         */
        public enum TrackingState
        {
            /**
             * \if english
             * TRACKING means the object is being tracked and its state is valid.
             * \else
             * TRACKING 表明正在跟踪，相关的数据是有效的
             * \endif
             */
            TRACKING = 0,
            /**
             * \if english
             * PAUSED indicates that HUAWEI AR Engine has paused tracking, 
             * and the related data is not accurate.  
             * \else
             * PAUSED 表明暂停跟踪，相关的数据是不准确的，不应该被使用
             * \endif
             */
            PAUSED = 1,
            /**
             * \if english
             * STOPPED means that HUAWEI AR Engine has stopped tracking, and will never resume tracking. 
             * \else
             * STOPPED 表明停止跟踪，相关的数据是无效的，可以被删除
             * \endif
             */
            STOPPED = 2
        }

        internal IntPtr m_trackableHandle = IntPtr.Zero;
        internal NDKSession m_ndkSession;

        internal ARTrackable()
        {

        }
        internal ARTrackable(IntPtr trackableHandle,NDKSession session)
        {
            m_trackableHandle = trackableHandle;
            m_ndkSession = session;
        }
        ~ARTrackable()
        {
            m_ndkSession.TrackableAdapter.Release(m_trackableHandle);
        }

        /**
         * \if english
         * Get the tracking state of current trackable.
         * \else
         * 获取当前的跟踪状态。
         * \endif
         */
        public virtual TrackingState GetTrackingState()
        {
            return m_ndkSession.TrackableAdapter.GetTrackingState(m_trackableHandle);
        }

        /**
         * \if english
         * Creates an anchor attached to current trackable at given pose.<b>Note: if the trackable doest not support 
         * attaching anchors, null will be returned.</b>
         * \else
         * 使用应用给定的位姿创建与trackable绑定的锚点。<b>注意：如果当期trackable不支持绑定锚点，将会返回null。</b>
         * \endif
         */
        public virtual ARAnchor CreateAnchor(Pose pose)
        {
            IntPtr anchorHandle = IntPtr.Zero;

            if(!m_ndkSession.TrackableAdapter.AcquireNewAnchor(m_trackableHandle,pose,out anchorHandle))
            {
                ARDebug.LogError("failed to create anchor on trackbale");
                return null;
            }
            return m_ndkSession.AnchorManager.ARAnchorFactory(anchorHandle, true);
        }

        /**
         * \if english
         * Get all anchors attached to current trackable.
         * @param[out] anchors A list to be filled with anchors.
         * @exception ARResourceExhaustedException if too many anchors exist.
         * \else
         * 获取与当前trackable绑定的所有锚点。
         * @param[out] anchors 锚点集合。
         * @exception ARResourceExhaustedException 如果存在的锚点过多。
         * \endif
         */
        public virtual void GetAllAnchors(List<ARAnchor> anchors)
        {
            if (anchors == null)
            {
                throw new ArgumentNullException();
            }
            m_ndkSession.TrackableAdapter.GetAnchors(m_trackableHandle, anchors);
        }

        /**
         * \if english 
         * @brief Indicates whether some other object is an ARTrackable referencing the same logical anchor as this one.
         * @param obj An object in C\#.
         * @return \c true if \c obj references to the same logical trackable as this one. Otherwise, \c false.
         * \else
         * @brief 两个ARTrackable对象可能对应同一个真实世界的物体，使用该方法可以比较是否对应同一个物体。
         * @param obj C\#中的对象。
         * @return 若\c obj 与当前trackable表示的是同一个真实世界中的物体时，返回\c true。否则，返回\c false。
         * \endif
         */
        public override bool Equals(object obj)
        {
            if (obj != null && obj is ARTrackable)
            {
                ARTrackable other = (ARTrackable)obj;
                if (other.m_trackableHandle == m_trackableHandle)
                {
                    return true;
                }
            }
            return false;
        }

        /**
         * \if english 
         * @brief Get the hashcode of this trackable.
         * @return Hashcode of this trackable.
         * \else
         * @brief 获取当前trackable的hashcode.
         * @return 当前trackable的hashcode.
         * \endif
         */
        public override int GetHashCode()
        {
            return m_trackableHandle.ToInt32();
        }
    }
}
