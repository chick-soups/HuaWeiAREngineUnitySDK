namespace HuaweiARUnitySDK
{
    using HuaweiARInternal;
    using System;
    using UnityEngine;

    /**
     * \if english
     * @brief A configuration used to track hand.
     * \else
     * @brief 用于跟踪手的配置项。
     * \endif
     */
    [CreateAssetMenu(fileName = "HuaweiARConfig", menuName = "HuaweiARUnitySDK/HandTrackingConfig", order = 4)]
    public class ARHandTrackingConfig : ARConfigBase
    {
        private NDKARType arType = NDKARType.HAND_AR;

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

        /**
         * \if english
         * @deprecated use \link EnableDepth \endlink instead.
         * \else
         * @deprecated 请使用 \link EnableDepth\endlink。
         * \endif
         */
        [Obsolete]
        public ARConfigHandFindingMode HandFindingMode = ARConfigHandFindingMode.ENABLE_2D;

        internal override ARConfigCameraLensFacing GetCameraLensFacing() { return CameraLensFacing; }
        internal override void SetCameraLensFacing(ARConfigCameraLensFacing lensFacing)
        {
            CameraLensFacing = lensFacing;
        }
        internal override int GetARType(){return (int)arType;}
        [Obsolete]
        internal override ARConfigHandFindingMode GetHandFindingMode()
        {
            return HandFindingMode;
        }
        [Obsolete]
        internal override void SetHandFindingMode(ARConfigHandFindingMode findingMode)
        {
            HandFindingMode = findingMode;
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
                arType, CameraLensFacing, LightingMode, UpdateMode, PowerMode, FocusMode);
        }
        ///@endcond
    }
}
