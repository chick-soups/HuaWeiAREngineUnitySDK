namespace HuaweiARUnitySDK
{
    using HuaweiARInternal;
    using System;
    using UnityEngine;

    /**
     * \if english 
     * @brief A configuration used to track bodies.
     * \else
     * @brief 用于跟踪人体的一个配置项。
     * \endif
     */

    [CreateAssetMenu(fileName = "HuaweiARConfig", menuName = "HuaweiARUnitySDK/BodyTrackingConfig", order = 2)]
    public class ARBodyTrackingConfig: ARConfigBase
    {
        private NDKARType arType = NDKARType.BODY_AR;
        /**
         * \if english
         * Focus mode of this configuratioin. Default is AUTO_FOCUS.
         * \else
         * 对焦模式，默认是自动对焦。
         * \endif
         */
        public ARConfigFocusMode FocusMode = ARConfigFocusMode.AUTO_FOCUS;

        /**
         * \if english
         * The choosen camera. Default is REAR camera.
         * \else
         * 启动的相机，默认启动后置相机。
         * \endif
         */
        public ARConfigCameraLensFacing CameraLensFacing = ARConfigCameraLensFacing.REAR;

        internal override ARConfigCameraLensFacing GetCameraLensFacing() { return CameraLensFacing; }
        internal override int GetARType() { return (int)arType; }

        internal override void SetCameraLensFacing(ARConfigCameraLensFacing lensFacing)
        {
            CameraLensFacing = lensFacing;
        }

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
            return string.Format("Config Type:{0}, CameraLensFacing:{1}, LightingMode:{2}, UpdateMode:{3}, PowerMode:{4} FocusMode:{5}",
                arType,CameraLensFacing,LightingMode,UpdateMode,PowerMode, FocusMode);
        }
        ///@endcond
    }
}
