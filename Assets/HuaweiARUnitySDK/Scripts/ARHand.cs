namespace HuaweiARUnitySDK
{
    using HuaweiARInternal;
    using System;
    using UnityEngine;
    using System.Collections.Generic;

    /**
     * \if english
     * @brief A hand in the real wolrd detected and tracked by HUAWEI AR Engine.
     * 
     * HUAWEI AR Engine always returns one ARHand object once the \link ARHandTrackingConfig \endlink is configurated
     * to ARSession by calling \link ARSession.Config(ARConfigBase) \endlink. Application should check the tracking 
     * state of one hand and adopt the data only when \link GetTrackingState()\endlink returns TRACKING. HUAWEI AR Engine
     * can always detect the guestures and the hand bound box. Currently, only when front camera is choosen by 
     * \link ARHandTrackingConfig.CameraLensFacing \endlink and \link ARHandTrackingConfig.EnableDepth\endlink is true,
     * it may distinguish hand type and skeletons according to devices'differences. The skeleton is defined as:
     * \htmlonly <style>div.image img[src="ARHand.png"]{width:400px;}</style> \endhtmlonly
     * @image html ARHand.png
     * In HUAWEI AR Engine, we use \link SkeletonPointName \endlink to represent the skeleton point. 
     * \else
     * @brief HUAWEI AR Engine识别并跟踪的真实世界中人手。
     * 
     * 如果\link ARSession.Config(ARConfigBase)\endlink 传入的是\link ARHandTrackingConfig\endlink 对象时，HUAWEI AR Engine将
     * 实时检测相机预览中的手。HUAWEI AR Engine将始终返回一个Hand对象，应用应该根据对象上的\link GetTrackingState()\endlink的
     * 返回值判断该对象是否有效。当且仅当GetTrackingState()的返回值为TRACKING时，数据才有效。HUAWEI AR Engine总是可以监测到手势
     * 以及手势周边的盒子。目前，只有当\link ARHandTrackingConfig.CameraLensFacing \endlink为前置相机且
     * \link ARHandTrackingConfig.EnableDepth\endlink开启的时候，才可能识别出手的类型和手骨数据（受手机类型限制）。骨骼点的定义如下：
     * \htmlonly <style>div.image img[src="ARHand.png"]{width:400px;}</style> \endhtmlonly
     * @image html ARHand.png
     * 在HUAWEI AR Engine使用\link SkeletonPointName \endlink来定义各个手骨骼点。
     * \endif
     */
    public class ARHand : ARTrackable
    {

        internal ARHand(IntPtr trackableHandle, NDKSession session) : base(trackableHandle, session)
        {

        }

        /**
         * \if engish
         * @brief Get gesture coordinate system type.
         * \else
         * @brief 获取手势的坐标系。
         * \endif
         */
        public ARCoordinateSystemType GetGestureCoordinateSystemType()
        {
            return m_ndkSession.HandAdapter.GetGestureCoordinateSystemType(m_trackableHandle);
        }

        /**
         * \if english
         * @brief Get hand type.
         * 
         * Currently, only when front camera is choosen by \link ARHandTrackingConfig.CameraLensFacing \endlink
         * and \link ARHandTrackingConfig.EnableDepth\endlink is true, the hand type may be distinguished accroding to devices'
         * differences.
         * \else
         * @brief 获取手势类型。
         * 
         * 目前，只有当\link ARHandTrackingConfig.CameraLensFacing \endlink为前置相机并且\link ARHandTrackingConfig.EnableDepth\endlink
         * 开启的时候，才可能识别出手势类型（受设备差异影响）。
         * \endif
         */
        public HandType GetHandType()
        {
            return m_ndkSession.HandAdapter.GetHandType(m_trackableHandle);
        }

        /**
         * \if english
         * @brief Get gesture type.
         * 
         * The returned integers are defined as follows:
         * \htmlonly <style>div.image img[src="ARHand_guesture_1-5.png"]{width:800px;}</style> \endhtmlonly
         * @image html ARHand_guesture_1-5.png
         * \htmlonly <style>div.image img[src="ARHand_guesture_6-10.png"]{width:800px;}</style> \endhtmlonly
         * @image html ARHand_guesture_6-10.png
         * For other gestures, the return value is -1.
         * 
         * <b>Note: when \link ARHandTrackingConfig.EnableDepth\endlink is false, gesture 2 & 8 are not supported;
         * when \link ARHandTrackingConfig.EnableDepth\endlink is true, gesture 1 & 10 are not supported.</b>
         * \else
         * @brief 获取手势类型。
         * 
         * 返回值定义如下：
         * \htmlonly <style>div.image img[src="ARHand_guesture_1-5.png"]{width:800px;}</style> \endhtmlonly
         * @image html ARHand_guesture_1-5.png
         * \htmlonly <style>div.image img[src="ARHand_guesture_6-10.png"]{width:800px;}</style> \endhtmlonly
         * @image html ARHand_guesture_6-10.png
         * 其他手势，返回值为-1。
         * 
         * <b>注意：当\link ARHandTrackingConfig.EnableDepth\endlink为false时，手势2&8不被识别；
         * 为true时，手势1&10不被识别。</b>
         * \endif
         */
        public int GetGestureType()
        {
            return m_ndkSession.HandAdapter.GetGustureType(m_trackableHandle);
        }

        /**
         * \if english
         * @brief Get the hand box.
         * 
         * The hand box is defined as the minimum rectangle covers the hand in the image. The returned value contains two Vector3.
         * The first one is the left top corner of the rectangle, and the second one is the right bottom corner. These two
         * values are in OpengGL NDC and the z components are 0.
         * \else
         * @brief 获取手势盒子。
         * 
         * 手势盒子被定义为图片中包裹手势的最小矩形框。返回值包含两个Vector3的值。第一个是手势盒子的左上角，第二个为右下角。
         * 两个值在OpengGL NDC空间，z分量始终为0.
         * \endif
         */
        public Vector3[] GetHandBox()
        {
            return m_ndkSession.HandAdapter.GetHandBox(m_trackableHandle);
        }

        /**
         * \if english
         * @brief Get the gesture center, named the center of hand box.
         * \else
         * @brief 获取手势中心，也就是手势盒子的中心。
         * \endif
         */
        public Vector3 GetGestureCenter()
        {
            return m_ndkSession.HandAdapter.GetGestureCenter(m_trackableHandle);
        }

        /**
         * \if english
         * @brief Get the gesture action.
         * @bug Not available.
         * \else
         * @brief 获取手势动作。
         * @bug 暂不可用。
         * \endif
         */
        public int[] GetGestureAction()
        {
            return m_ndkSession.HandAdapter.GetGestureAction(m_trackableHandle);
        }

        /**
         * \if english
         * @brief Get gesture orientation.
         * @bug Not available.
         * \else
         * @brief 获取手势朝向。
         * @bug 暂不可用。
         * \endif
         */
        public Vector4 GetGestureOrientation()
        {
            return m_ndkSession.HandAdapter.GetGestureOrientation(m_trackableHandle);
        }

        /**
         * \if english
         * @brief Get the coordinate system of skeleton data.
         * \else
         * @brief 获取手骨骼使用的系统坐标。
         * \endif
         */
        public ARCoordinateSystemType GetSkeletonCoordinateSystemType()
        {
            return m_ndkSession.HandAdapter.GetSkeletonCoordinateSystemType(m_trackableHandle);
        }

        /**
         * \if english
         * @brief Get the skeleton count of hand.
         * \else
         * @brief 获取手骨骼点的个数。
         * \endif
         */
        public int GetHandSkeletonCount()
        {
            return m_ndkSession.HandAdapter.GetHandSkeletonCount(m_trackableHandle);
        }

        /**
         * \if english
         * @brief Get the skeletons.
         * 
         * The out skeleton data is under the coordinate of \link GetSkeletonCoordinateSystemType()\endlink. Currently,
         * this method should only be called when \link ARHandTrackingConfig.CameraLensFacing \endlink is set FRONT and
         * \link ARHandTrackingConfig.EnableDepth \endlink is set true.
         * @param[out] outSkeleton The dictionary of skeletons.
         * \else
         * @brief 获取骨骼点。
         * 
         * 输出的骨骼点位于\link GetSkeletonCoordinateSystemType()\endlink的坐标系下。目前，仅当
         * \link ARHandTrackingConfig.CameraLensFacing \endlink 为前置，\link ARHandTrackingConfig.EnableDepth \endlink为
         * true时，该方法才返回有效数据。
         * @param[out] outSkeleton 骨骼点的字典。
         * \endif
         */
        public void GetSkeletons(Dictionary<SkeletonPointName, SkeletonPointEntry> outSkeleton)
        {
            if (null == outSkeleton)
            {
                throw new ArgumentNullException();
            }
            outSkeleton.Clear();

            int[] skeletonType = m_ndkSession.HandAdapter.GetHandSkeletonType(m_trackableHandle);

            Vector3[] points = m_ndkSession.HandAdapter.GetHandSkeletonData(m_trackableHandle);
            for (int i = 0; i < skeletonType.Length; i++)
            {
                if (!ValueLegalityChecker.CheckInt("GetSkeletons", skeletonType[i], 0,
                    (int)SkeletonPointName.SKELETON_LENGTH - 1))
                {
                    continue;
                }
                outSkeleton.Add((SkeletonPointName)skeletonType[i], new SkeletonPointEntry(points[i]));
            }
        }
        /**
         * \if english
         * @brief Get the skeleton connections.
         * @param[out] outConnections The connections of skeleton.
         * \else
         * @brief 获取骨骼点之间的连接关系。
         * @param[out] outConnections 骨骼点之间的连接。
         * \endif
         */
        public void GetSkeletonConnection(List<KeyValuePair<SkeletonPointName, SkeletonPointName>> outConnections)
        {
            if (null == outConnections)
            {
                throw new ArgumentNullException();
            }
            outConnections.Clear();
            int[] connections = m_ndkSession.HandAdapter.GetSkeletonConnection(m_trackableHandle);
            int[] skeletonType = m_ndkSession.HandAdapter.GetHandSkeletonType(m_trackableHandle);
            for (int i = 0; i < connections.Length/2; i++)
            {
                if (connections[2 * i] < 0 || connections[2 * i] >= skeletonType.Length
                    || connections[2 * i + 1] < 0 || connections[2 * i + 1] >= skeletonType.Length)
                {
                    continue;
                }
                if (!ValueLegalityChecker.CheckInt("GetSkeletonConnection", skeletonType[connections[2 * i]],
                    AdapterConstants.Enum_HandSkeletonPointName_MinIntValue, AdapterConstants.Enum_HandSkeletonPointName_MaxIntValue - 1)
                    || !ValueLegalityChecker.CheckInt("GetSkeletonConnection", skeletonType[connections[2 * i + 1]],
                    AdapterConstants.Enum_HandSkeletonPointName_MinIntValue, AdapterConstants.Enum_HandSkeletonPointName_MaxIntValue - 1))
                {
                    continue;
                }
                outConnections.Add(new KeyValuePair<SkeletonPointName, SkeletonPointName>(
                    (SkeletonPointName)skeletonType[connections[2 * i]],
                    (SkeletonPointName)skeletonType[connections[2 * i + 1]]));
            }
        }

        /**
         * \if english
         * @brief Struct of hand skeleton point.
         * \else
         * @brief 手骨骼点的结构。
         * \endif
         */
        public struct SkeletonPointEntry
        {
            internal SkeletonPointEntry(Vector3 vector)
            {
                Coordinate = vector;
            }
            /**
             * \if english
             * @brief Coordinate of hand skeleton.
             * \else
             * @brief 手骨骼点的坐标点
             * \endif
             */
            public Vector3 Coordinate { get; private set; }
        }

        /**
         * \if english
         * @brief Enumeration of hand type.
         * \else
         * @brief 手类型的枚举。
         * \endif
         */
        public enum HandType
        {
            UNKNOWN = -1,
            RIGHT = 0,
            LEFT = 1,
        }

        /**
         * \if english
         * @brief Enumeration of hand skeleton point name. Please refer to the definition of hand skeleton.
         * \else
         * @brief 手骨骼点名字的枚举。请参考骨骼点的定义图。
         * \endif
         */
        public enum SkeletonPointName
        {
            Root = 0,
            Pinky_1 = 1,
            Pinky_2 = 2,
            Pinky_3 = 3,
            Pinky_4 = 4,
            Ring_1 = 5,
            Ring_2 = 6,
            Ring_3 = 7,
            Ring_4 = 8,
            Middle_1 = 9,
            Middle_2 = 10,
            Middle_3 = 11,
            Middle_4 = 12,
            Index_1 = 13,
            Index_2 = 14,
            Index_3 = 15,
            Index_4 = 16,
            Thumb_1 = 17,
            Thumb_2 = 18,
            Thumb_3 = 19,
            Thumb_4 = 20,
            
            /**
             * \if english
             * Indicate the length of skeleton instead of a valid name.
             * \else
             * 非有效的骨骼点，用于表明骨骼点的个数。
             * \endif
             */
            SKELETON_LENGTH = 21
        };
    }
}
