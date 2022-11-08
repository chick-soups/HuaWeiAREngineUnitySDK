///@cond ContainImageAR
namespace HuaweiARUnitySDK
{
    using HuaweiARInternal;
    using System;
    using UnityEngine;

    /**
     * \if english
     * @brief A configuration used to track 2D images.
     * \else
     * @brief 用于跟踪图片的配置项。
     * \endif
     */
    [CreateAssetMenu(fileName = "HuaweiARConfig", menuName = "HuaweiARUnitySDK/ImageTrackingConfig", order = 1)]
    public class ARImageTrackingConfig : ARConfigBase
    {
        private NDKARType arType = NDKARType.IMAGE_AR;

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
         * The database of tracking images.
         * \else
         * 要跟踪的图片的数据库。
         * \endif
         */
        public ARAugmentedImageDatabase AugmentedImageDatabase = null;

        internal override ARAugmentedImageDatabase GetAugImgDataBaseHandle() { return AugmentedImageDatabase; }
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
            return string.Format("Config Type:{0}, LightingMode:{1} FocusMode:{2}", arType, LightingMode, FocusMode);
        }
        ///@endcond
    }
}
///@endcond