namespace HuaweiARUnitySDK
{
    using System;
    using HuaweiARInternal;
    using UnityEngine;
    using System.Collections.Generic;

    /**\if english
     * @brief  A body in the real world detected and tracked by HUAWEI AR Engine.
     * 
     * HUAWEI AR Engine always returns two ARBody obejcts once the \link ARBodyTrackingConfig \endlink or
     * \link ARWorldBodyTrackingConfig\endlink is configurated to \link ARSession\endlink by calling 
     * \link ARSession.Config(ARConfigBase)\endlink. Application should check the tracking state of a body and adopt the data only when
     * \link GetTrackingState() \endlink returns \link ARTrackable.TrackingState.TRACKING \endlink. The body is defined as 
     * \htmlonly <style>div.image img[src="ARBody.png"]{width:200px;}</style> \endhtmlonly 
     * @image html ARBody.png 
     * In HUAWEI AR Engine, we use \link SkeletonPointName \endlink to represent the skeleton point.
     * \else
     * @brief HUAWEI AR Engine识别并跟踪的真实世界中的人体。
     * 
     * 如果\link ARSession.Config(ARConfigBase) \endlink 传入的是 \link ARBodyTrackingConfig \endlink 或者是
     * \link ARWorldBodyTrackingConfig\endlink 的对象时， HUAWEI AR Engine将实时检测相机预览中的人体。HUAWEI AR Engine将始终返回
     * 两个body对象，应用应该根据对象上的 \link GetTrackingState() \endlink 的返回值判断该对象是否有效。当且仅当\link GetTrackingState() \endlink返回
     * 值为  \link ARTrackable.TrackingState.TRACKING \endlink时，数据才有效。人体的骨骼定义如下图：
     * \htmlonly <style>div.image img[src="ARBody.png"]{width:200px;}</style> \endhtmlonly 
     * @image html ARBody.png 
     * 在HUAWEI AR Engine中使用 \link SkeletonPointName \endlink 来定义各个骨骼点。
     * \endif
     */
    public class ARBody:ARTrackable
    {

        internal ARBody(IntPtr trackableHandle, NDKSession session) : base(trackableHandle, session)
        {
            
        }

        /**
         * \if english
         * @brief Get the number of skeleton point of this body.
         * @return The count of skeleton point.
         * \else
         * @brief 获取当前body对象上的骨骼点个数。
         * @return 骨骼点的个数。
         * \endif
         */

        public int GetSkeletonPointCount()
        {
            return m_ndkSession.BodyAdapter.GetSkeletonPointCount(m_trackableHandle);
        }

        /**
         * \if english
         * @brief Get the skeletons of this Body.
         * @param[out] outDic The Dictionary of the body skeletons. The item in \c outDic is indexed by 
         * \link SkeletonPointName \endlink and the second value of this item is a \link SkeletonPointEntry \endlink.
         * \else
         * @brief 获取当前body对象的骨骼点。
         * @param[out] outDic 当前骨骼点的字典。outDic的项是通过\link SkeletonPointName \endlink索引，索引的对象是
         * \link SkeletonPointEntry \endlink。
         * \endif
         */
        public void GetSkeletons(Dictionary<SkeletonPointName, SkeletonPointEntry> outDic)
        {
            if (null == outDic)
            {
                throw new ArgumentNullException();
            }
            outDic.Clear();

            bool[] is2DValid = m_ndkSession.BodyAdapter.GetSkeletonPointIsExist_2D(m_trackableHandle);
            Vector3[] coord2D = m_ndkSession.BodyAdapter.GetSkeletonPoint2D(m_trackableHandle);
            bool[] is3DValid = m_ndkSession.BodyAdapter.GetSkeletonPointIsExist_3D(m_trackableHandle);
            Vector3[] coord3D = m_ndkSession.BodyAdapter.GetSkeletonPoint3D(m_trackableHandle);
            int[] skeletonType = m_ndkSession.BodyAdapter.GetSkeletonType(m_trackableHandle);
            float[] confidence = m_ndkSession.BodyAdapter.GetSkeletonConfidence(m_trackableHandle);
            int sCnt = GetSkeletonPointCount();
            for(int i = 0; i < sCnt; i++)
            {
                SkeletonPointEntry spe = new SkeletonPointEntry(is2DValid[i],coord2D[i],
                    is3DValid[i],coord3D[i], confidence[i]);
                if(!ValueLegalityChecker.CheckInt("GetSkeletons", skeletonType[i],
                    0, (int)SkeletonPointName.SKELETON_LENGTH-1)){
                    continue;
                }
                outDic.Add((SkeletonPointName)skeletonType[i], spe);
            }
        }

        /**
         * \if english
         * @brief Get the connections relationship of the body skeleton.
         * @param[out] outConnections The connections relationship of the body skeleton.
         * \else
         * @brief 获取骨骼点之间的连接关系。
         * @param[out] outConnections 骨骼点之间的连接关系。
         * \endif
         */
        public void GetSkeletonConnection(List<KeyValuePair<SkeletonPointName, SkeletonPointName>> outConnections)
        {
            if (null == outConnections)
            {
                throw new ArgumentNullException();
            }
            outConnections.Clear();

            int[] connections = m_ndkSession.BodyAdapter.GetSkeletonConnection(m_trackableHandle);
            int[] skeletonType = m_ndkSession.BodyAdapter.GetSkeletonType(m_trackableHandle);
            for (int i=0;i<connections.Length/2;i++)
            {
                if (connections[2*i]<0||connections[2*i]>=skeletonType.Length
                    ||connections[2*i+1]<0||connections[2*i+1]>=skeletonType.Length)
                {
                    continue;
                }
                if(!ValueLegalityChecker.CheckInt("GetSkeletonConnection",skeletonType[connections[2*i]],
                    AdapterConstants.Enum_BodySkeletonPointName_MinIntValue,AdapterConstants.Enum_BodySkeletonPointName_MaxIntValue-1)
                    || !ValueLegalityChecker.CheckInt("GetSkeletonConnection", skeletonType[connections[2 * i+1]],
                    AdapterConstants.Enum_BodySkeletonPointName_MinIntValue, AdapterConstants.Enum_BodySkeletonPointName_MaxIntValue - 1))
                {
                    continue;
                }
                outConnections.Add(new KeyValuePair<SkeletonPointName, SkeletonPointName>(
                    (SkeletonPointName)skeletonType[connections[2 * i]],
                    (SkeletonPointName)skeletonType[connections[2 * i+1]]));
            }
        }

        /**
         * \if english
         * @brief Get the confidence of the body skeletons.
         * @deprecated Use \link SkeletonPointEntry.Confidence \endlink.
         * @return The array of the confidence of the body skeletons. Use each \link SkeletonPointName \endlink as the index 
         * of this array. Each confidence rangs in [0,1].
         * <b>Note: \link SkeletonPointName.SKELETON_LENGTH \endlink can not be used as an index.</b>
         * \else
         * @brief 获取骨骼点的置信度。
         * @deprecated 请使用 \link SkeletonPointEntry.Confidence \endlink。
         * @return 骨骼点的置信度数组。使用\link SkeletonPointName \endlink作为数组索引以取得对应的置信度值。每个置信度的取值范围为[0,1]。
         * <b>注意：\link SkeletonPointName.SKELETON_LENGTH \endlink值不可用做索引。</b>
         * \endif
         */
        [Obsolete]
        public float[] GetSkeletonConfidence()
        {
            return m_ndkSession.BodyAdapter.GetSkeletonConfidence(m_trackableHandle);
        }
        /**
         * \if english
         * @brief Get the current body action.
         * @return The body action value. The value if defined as
         * \htmlonly 
         *      <style>div.image 
         *                  img[src="ARBody_actions.png"]{width:1400px;}
         *      </style> \endhtmlonly 
         * @image html ARBody_actions.png
         * Other actions will return 0.
         * \else
         * @brief 获取当前人体的动作.
         * @return 返回值定义如下：
         * \htmlonly 
         *      <style>div.image 
         *                  img[src="ARBody_actions.png"]{width:1400px;}
         *      </style> \endhtmlonly 
         * @image html ARBody_actions.png
         * 其他未定义的动作返回值为0.
         * \endif
         */
        public int GetBodyAction()
        {
            return m_ndkSession.BodyAdapter.GetBodyAction(m_trackableHandle);
        }

        /**
         * \if english
         * @brief Get the confidence of body mask.
         * 
         * This confidence means whether there exists body in on pixel. If current pixel is very likely belongs part of a body,
         * then the confidence is large.
         * @return \c IntPtr of unmanaged memory. This memory stores a row major confidence image, which contains only 
         * one float channel. The width, height and roation is the same as the camera preview(GPU), which can be obtained from 
         * \link ARSession.GetCameraConfig() \endlink. 
         *  You can use <a href="https://docs.unity3d.com/ScriptReference/Texture2D.LoadRawTextureData.html">Texture2D.LoadRawTextureData()</a> 
         *  to generate a Texture2D with
         *  <a href ="https://docs.unity3d.com/ScriptReference/TextureFormat.RFloat.html">TextureFormat.RFloat</a> 
         *  according to the returned value.<b>Note: works when \link ARConfigBase.EnableMask \endlink is true.</b>
         * \else
         * @brief 获取人体遮罩的置信度。
         * 
         * 一个人体遮罩的置信度意味着在某个像素是否属于人体的一部分。如果置信度高，则意味着这个像素很可能属于人体的一部分。
         * @return 非托管内存的指针。这块内存中存储着一副行排序、单通道、浮点的置信度图。该图的宽、高和方向与预览流(GPU)的一致。宽和高
         * 可以通过 \link ARSession.GetCameraConfig() \endlink获取。应用可以使用返回值，通过
         * <a href="https://docs.unity3d.com/ScriptReference/Texture2D.LoadRawTextureData.html">Texture2D.LoadRawTextureData()</a>
         * 生成一个
         * <a href ="https://docs.unity3d.com/ScriptReference/TextureFormat.RFloat.html">TextureFormat.RFloat</a>格式的Texture2D。
         * <b>注意：仅当\link ARConfigBase.EnableMask \endlink 为true时，才返回有效数据。</b>
         * \endif
         */
        public IntPtr GetMaskConfidence()
        {
            return m_ndkSession.BodyAdapter.GetMaskConfidence(m_trackableHandle);
        }

        /**
         * \if english
         * @brief Get the depth of the body mask.
         * 
         * The Format of the depth is 
         * <a href="https://developer.android.com/reference/android/graphics/ImageFormat#DEPTH16">DEPTH16</a>.
         * @return \c IntPtr of unmanaged memory. This memory stores a row major depth image, which contains only 
         * one channel. The width, height and roation is the same as the camera preview(GPU), which can be obtained from 
         * \link ARSession.GetCameraConfig() \endlink. 
         * <b>Note: 1. works when \link ARConfigBase.EnableMask \endlink is true; 2. only the depth of pixels which are very likely part of a body is non-zero.</b>
         * \else
         * @brief 获取人体遮罩的深度值。
         * 
         * 深度值的格式为<a href="https://developer.android.com/reference/android/graphics/ImageFormat#DEPTH16">DEPTH16</a>。
         * @return 非托管内存的指针。这块内存中存储了一个行排序的、单通道的深度图。深度图的宽、高和方向与预览流(GPU)一致。宽和高
         * 可以通过 \link ARSession.GetCameraConfig() \endlink获取。
         * <b>注意：1. 仅当\link ARConfigBase.EnableMask \endlink 为true时，才返回有效数据；2. 只有在有人体的位置，深度值才不为0。</b>
         * \endif
         */
        public IntPtr GetMaskDepth()
        {
            return m_ndkSession.BodyAdapter.GetMaskDepth(m_trackableHandle);
        }

        /**
         * \if english
         * @brief Get the coordinate system of this body.
         * 
         * Different configurations of ARSession may start different algorithms, so as the different coordinate systems.
         * @return The coordinate system of this body.
         * \else
         * @brief 获取当前人体骨骼点所使用的坐标空间。
         * 
         * 不同的ARSession配置可能会启动不同的算法，而不同的算法返回骨骼点的坐标在不同的坐标空间。
         * @return 当前body骨骼点的坐标空间。
         * \endif
         */
        public ARCoordinateSystemType GetCoordinateSystemType()
        {
            return m_ndkSession.BodyAdapter.GetCoordinateSystemType(m_trackableHandle);
        }

        /**
         * \if english
         * @brief Enumeration of skeleton point name. 
         * \else
         * @brief 骨骼点名称的枚举。
         * \endif
         */
        public enum SkeletonPointName
        {
            /**
             * \if english
             *  Head Top
             * \else
             *  头
             * \endif
             */
            Head_Top = 0,
            /**
             * \if english
             *  Neck
             * \else
             *  颈部
             * \endif
             */
            Neck = 1,
            /**
             * \if english
             *  Right shoulder
             * \else
             *  右肩
             * \endif
             */
            Right_Shoulder = 2,
            /**
             * \if english
             *  Right elbow
             * \else
             *  右肘
             * \endif
             */
            Right_Elbow = 3,
            /**
             * \if english
             *  Right wrist
             * \else
             *  右手腕
             * \endif
             */
            Right_Wrist = 4,
            /**
             * \if english
             *  Left shoulder
             * \else
             *  左肩
             * \endif
             */
            Left_Shoulder = 5,
            /**
             * \if english
             *  Left elbow
             * \else
             *  左肘
             * \endif
             */
            Left_Elbow = 6,
            /**
             * \if english
             *  Left wrist
             * \else
             *  左手腕
             * \endif
             */
            Left_Wrist = 7,
            /**
             * \if english
             *  Right Hip
             * \else
             *  右髋关节
             * \endif
             */
            Right_Hip = 8,
            /**
             * \if english
             *  Right ankle
             * \else
             *  右膝
             * \endif
             */
            Right_Knee = 9,
            /**
             * \if english
             *  Right ankle
             * \else
             *  右脚腕
             * \endif
             */
            Right_Ankle = 10,
            /**
             * \if english
             *  Left Hip
             * \else
             *  左髋关节
             * \endif
             */
            Left_Hip = 11,
            /**
             * \if english
             *  Left knee
             * \else
             *  左膝盖
             * \endif
             */
            Left_Knee = 12,
            /**
             * \if english
             *  Left Ankle
             * \else
             *  左脚腕
             * \endif
             */
            Left_Ankle = 13,
            /**
             * \if english
             *  Body Center
             * \else
             *  身体中心点
             * \endif
             */
            Body_Center = 14,
            /**
             * \if english
             *  The length of skeleton. It's not a name.
             * \else
             *  骨骼点的个数。该枚举不是一个名字。
             * \endif
             */
            SKELETON_LENGTH = 15,
        }

        /**
         * \if english
         * @brief The struct of the skeleton point.
         * \else
         * @brief 骨骼点的结构体。
         * \endif
         */
        public struct SkeletonPointEntry
        {
            internal SkeletonPointEntry(bool valid2d,Vector2 coord2d,bool valid3d,
                Vector3 coord3d,float confidence)
            {
                Is2DValid = valid2d;
                Coordinate2D = coord2d;
                Is3DValid = valid3d;
                Coordinate3D = coord3d;
                Confidence = confidence;
            }

            /**
             * \if english
             * Whether the \link Coordinate2D \endlink is valid.
             * \else
             * \link Coordinate2D \endlink是否有效。
             * \endif
             */
            public bool Is2DValid { get; private set; }

            /**
             * \if english
             * 2D coordinate in OpenGL NDC. \c z is always 0.
             * \else
             * 2D图像坐标，在OpenGL NDC空间。 \c z 始终为0。
             * \endif
             */
            public Vector3 Coordinate2D { get; private set; }

            /**
             * \if english
             * Whether the \link Coordinate3D \endlink is valid.
             * \else
             * \link Coordinate3D \endlink是否有效。
             * \endif
             */
            public bool Is3DValid { get; private set; }

            /**
             * \if english
             *3D coordinate in \link GetCoordinateSystemType() \endlink system.
             * \else
             * 在\link GetCoordinateSystemType() \endlink空间的3D坐标。
             * \endif
             */
            public Vector3 Coordinate3D { get; private set; }
            /**
             * \if english
             * Confidence of skeleton point.
             * \else
             * 骨骼点置信度
             * \endif
             */
            public float Confidence { get; private set; }
        }
    }
}
