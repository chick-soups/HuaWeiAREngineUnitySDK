///@cond ContainFaceAR
namespace HuaweiARUnitySDK
{
    using HuaweiARInternal;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    
    /**
     * \if english
     * @brief A face in the real world detected and tracked by HUAWEI AR Engine.
     * 
     * HUAWEI AR Engine always returns a ARFace object once the \link ARFaceTrackingConfig \endlink is configurated to
     * \link ARSession \endlink by calling \link ARSession.Config(ARConfigBase) \endlink. Application should check the 
     * tracking state of a body and adopt the data only when \link GetTrackingState() \endlink returns 
     * \link ARTrackable.TrackingState.TRACKING \endlink. 
     * 
     * HUAWEI AR Engine can detect the pose and blendshapes of face in the real time. Also, it can model a face and get a 3D mesh 
     * returned by \link GetFaceGeometry \endlink. This mesh is based on a face coordinate, whose origin is located 
     * in the middle of head. Application can obtain the pose of the origin by \link GetPose() \endlink.
     * \else
     * @brief HUAWEI AR Engine 识别并跟踪的真实世界中的人脸。
     * 
     * \link ARSession.Config(ARConfigBase) \endlink 传入的是 \link ARFaceTrackingConfig \endlink对象时，
     * HUAWEI AR Engine将实时检测相机预览中的人脸。HUAWEI AR Engine将始终返回一个body对象，应用应该根据对象上
     * 的 \link GetTrackingState() \endlink 的返回值判断该对象是否有效。当且仅当\link GetTrackingState() \endlink返回
     * 值为\link ARTrackable.TrackingState.TRACKING \endlink时，数据才有效。
     * 
     * HUAWEI AR Engine可以实时检测到人脸的姿态、微表情，同时还能对人脸建模：通过\link GetFaceGeometry() \endlink 返回
     * 人脸的3D Mesh。该Mesh基于一个人脸坐标系，该坐标系的原点位于人头模型的中央。可以通过\link GetPose()\endlink 获取到
     * 该原点在Unity相机坐标系下的位姿。
     * \endif
     */
    public class ARFace : ARTrackable
    {
        internal ARFace(IntPtr faceHandle, NDKSession session) : base(faceHandle, session)
        {
        }

        /**
         * \if english 
         * @brief Get the pose of face in unity camera coordinate.
         * @return Pose of face.
         * @bug Not available yet.
         * \else
         * @brief 获取人脸的位姿。
         * @return 人脸的位姿。
         * @bug 暂不可用。
         * \endif
         */
        public Pose GetPose()
        {
            return m_ndkSession.FaceAdapter.GetPose(m_trackableHandle);
        }

        /**
         * \if english 
         * @brief Get the geometry of face.
         * @return Geometry of face.
         * @bug Not available yet.
         * \else
         * @brief 获取人脸的几何形状。
         * @return 人脸的几何形状。
         * @bug 暂不可用。
         * \endif
         */
        public ARFaceGeometry GetFaceGeometry()
        {
            IntPtr faceGeometryHandle = m_ndkSession.FaceAdapter.AcquireGeometry(m_trackableHandle);
            return new ARFaceGeometry(faceGeometryHandle,m_ndkSession);
        }

        /**
         * \if english 
         * @brief Get the blend shape of face.
         * @return Dictionary of belnd shape and its value.
         * \else
         * @brief 获取人脸的微表情。
         * @return 包含人脸的微表情和值的字典。
         * \endif
         */
        public Dictionary<BlendShapeLocation, float> GetBlendShape()
        {
            IntPtr blendShapeHandle = m_ndkSession.FaceAdapter.AcquireBlendShape(m_trackableHandle);
            Dictionary<BlendShapeLocation, float> ret = m_ndkSession.FaceBlendShapeAdapter.GetBlendShapeData(blendShapeHandle);
            m_ndkSession.FaceBlendShapeAdapter.Release(blendShapeHandle);
            return ret;
        }

        /**
         * \if english 
         * @brief Get the blend shape of face.
         * @return Dictionary of belnd shape string and its value.
         * \else
         * @brief 获取人脸的微表情。
         * @return 包含人脸微表情字符串和值的字典。
         * \endif
         */
        public Dictionary<string, float> GetBlendShapeWithBlendName()
        {
            Dictionary<string, float> ret = new Dictionary<string, float>();
            Dictionary<BlendShapeLocation, float> tmp = GetBlendShape();
            foreach(KeyValuePair<BlendShapeLocation,float> item in tmp)
            {
                ret.Add(item.Key.ToString(), item.Value);
            }
            return ret;
        }

        /**
         * \if english 
         * @brief Enumeration of face blendshape.
         * \else
         * @brief 微表情的枚举。
         * \endif
         */
        public enum BlendShapeLocation
        {
            Animoji_Eye_Blink_Left = 0,
            Animoji_Eye_Look_Down_Left = 1,
            Animoji_Eye_Look_In_Left = 2,
            Animoji_Eye_Look_Out_Left = 3,
            Animoji_Eye_Look_Up_Left = 4,
            Animoji_Eye_Squint_Left = 5,
            Animoji_Eye_Wide_Left = 6,
            Animoji_Eye_Blink_Right = 7,
            Animoji_Eye_Look_Down_Right = 8,
            Animoji_Eye_Look_In_Right = 9,
            Animoji_Eye_Look_Out_Right = 10,
            Animoji_Eye_Look_Up_Right = 11,
            Animoji_Eye_Squint_Right = 12,
            Animoji_Eye_Wide_Right = 13,
            Animoji_Jaw_Forward = 14,
            Animoji_Jaw_Left = 15,
            Animoji_Jaw_Right = 16,
            Animoji_Jaw_Open = 17,
            Animoji_Mouth_Funnel = 18,
            Animoji_Mouth_Pucker = 19,
            Animoji_Mouth_Left = 20,
            Animoji_Mouth_Right = 21,
            Animoji_Mouth_Smile_Left = 22,
            Animoji_Mouth_Smile_Right = 23,
            Animoji_Mouth_Frown_Left = 24,
            Animoji_Mouth_Frown_Right = 25,
            Animoji_Mouth_Dimple_Left = 26,
            Animoji_Mouth_Dimple_Right = 27,
            Animoji_Mouth_Stretch_Left = 28,
            Animoji_Mouth_Stretch_Right = 29,
            Animoji_Mouth_Roll_Lower = 30,
            Animoji_Mouth_Roll_Upper = 31,
            Animoji_Mouth_Shrug_Lower = 32,
            Animoji_Mouth_Shrug_Upper = 33,
            Animoji_Mouth_Upper_Up = 34,
            Animoji_Mouth_Lower_Down = 35,
            Animoji_Mouth_Lower_Out = 36,
            Animoji_Brow_Down_Left = 37,
            Animoji_Brow_Down_Right = 38,
            Animoji_Brow_Inner_Up = 39,
            Animoji_Brow_Outter_Up_Left = 40,
            Animoji_Brow_Outter_Up_Right = 41,
            Animoji_Cheek_Puff = 42,
            Animoji_Cheek_Squint_Left = 43,
            Animoji_Cheek_Squint_Right = 44,
            Animoji_Frown_Nose_Mouth_Up = 45,
            Animoji_Tongue_In = 46,
            Animoji_Tongue_Out_Slight = 47,
            Animoji_Tongue_Left = 48,
            Animoji_Tongue_Right = 49,
            Animoji_Tongue_Up = 50,
            Animoji_Tongue_Down = 51,
            Animoji_Tongue_Left_Up = 52,
            Animoji_Tongue_Left_Down = 53,
            Animoji_Tongue_Right_Up = 54,
            Animoji_Tongue_Right_Down = 55,
            Animoji_Left_EyeBall_Left = 56,
            Animoji_Left_EyeBall_Right = 57,
            Animoji_Left_EyeBall_Up = 58,
            Animoji_Left_EyeBall_Down = 59,
            Animoji_Right_EyeBall_Left = 60,
            Animoji_Right_EyeBall_Right = 61,
            Animoji_Right_EyeBall_Up = 62,
            Animoji_Right_EyeBall_Down = 63,
            Animoji_BLENDSHAPE_LENGTH = 64,
        }
    }
}
///@endcond