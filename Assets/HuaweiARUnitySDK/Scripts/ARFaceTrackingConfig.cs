///@cond ContainFaceAR
namespace HuaweiARUnitySDK
{
    using HuaweiARInternal;
    using System;
    using UnityEngine;

    /**
     * \if english
     * @brief A configuration used to track a face.
     * \else
     * @brief 用于跟踪人脸的一个配置项。
     * \endif
     */
    [CreateAssetMenu(fileName = "HuaweiARConfig", menuName = "HuaweiARUnitySDK/FaceTrackingConfig", order = 3)]
    public class ARFaceTrackingConfig : ARConfigBase
    {
        private NDKARType arType = NDKARType.FACE_AR;

        /**
         * \if english
         * Focus mode of this configuratioin. Default is AUTO_FOCUS.
         * \else 
         * 对焦模式，默认是自动对焦。
         * \endif
         */
        public ARConfigFocusMode FocusMode = ARConfigFocusMode.AUTO_FOCUS;
        private ARConfigCameraLensFacing CameraLensFacing = ARConfigCameraLensFacing.FRONT;

        internal override ARConfigCameraLensFacing GetCameraLensFacing() { return CameraLensFacing; }
        internal override void SetCameraLensFacing(ARConfigCameraLensFacing lensFacing)
        {
            CameraLensFacing = lensFacing;
        }
        internal override int GetARType() { return (int)arType; }

        internal override ARConfigFocusMode GetFocusMode()
        {
            return FocusMode;
        }

        internal override void SetFocusMode(ARConfigFocusMode focusMode)
        {
            FocusMode = focusMode;
        }
        ///@cond EXCLUDE_DOXYGEN
        public override string ToString()
        {
            return string.Format("Config Type:{0}, CameraLensFacing:{1}, LightingMode:{2} ,UpdateMode:{3}, PowerMode:{4} FocusMode:{5}",
                arType, CameraLensFacing, LightingMode, UpdateMode, PowerMode, FocusMode);
        }
        ///@endcond
    }
}
///@endcond