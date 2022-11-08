namespace HuaweiARUnitySDK
{
    using HuaweiARInternal;
    using System;
    using UnityEngine;

    /**
     * \if english
     * @brief A configuration used to track the world.
     * \else
     * @brief 用于跟踪世界的配置项。
     * \endif
     */
    [CreateAssetMenu(fileName = "HuaweiARConfig", menuName = "HuaweiARUnitySDK/WorldTrackingConfig", order = 1)]
    public class ARWorldTrackingConfig : ARConfigBase
    {
        private NDKARType arType = NDKARType.WORLD_AR;

        /**
         * \if english
         * Focus mode of this configuratioin. Default is FIXED_FOCUS.
         * \else
         * 对焦模式，默认是锁定对焦到无穷远。
         * \endif
         */
        public ARConfigFocusMode FocusMode = ARConfigFocusMode.FIXED_FOCUS;

        /**
         * \if english
         * Select the behavior of the plane detection subsystem. Default is Enable.
         * \else
         * 设置平面检测的行为，默认使能。
         * \endif
         */
        public ARConfigPlaneFindingMode PlaneFindingMode =ARConfigPlaneFindingMode.ENABLE;

        ///@cond ContainImageAR
        /**
         * \if english
         * Set the database of image tracking. Default is null.
         * \else
         * 设置图像跟踪的数据库。
         * \endif
         */
        public ARAugmentedImageDatabase AugmentedImageDatabase = null;
        ///endcond

        internal override ARAugmentedImageDatabase GetAugImgDataBaseHandle() { return AugmentedImageDatabase; }
        internal override ARConfigPlaneFindingMode GetPlaneFindingMode() { return PlaneFindingMode; }
        internal override void SetPlaneFindingMode(ARConfigPlaneFindingMode mode)
        {
            PlaneFindingMode = mode;
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
            return string.Format("Config Type:{0}, PlaneFindingMode:{1}, LightingMode:{2}, UpdateMode:{3}, PowerMode:{4},FocusMode:{5}",
                arType, PlaneFindingMode, LightingMode, UpdateMode, PowerMode, FocusMode);
        }
        ///@endcond
    }
}
