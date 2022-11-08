namespace HuaweiARUnitySDK
{
    using HuaweiARInternal;
    using System;
    using UnityEngine;

    /**
     * \if engish
     * @brief A configuration used to tracking world and body simultaneously.
     * \else
     * @brief 同时跟踪世界和人体的配置项。
     * \endif
     */
    [CreateAssetMenu(fileName = "HuaweiARConfig", menuName = "HuaweiARUnitySDK/WorldBodyTrackingConfig", order = 5)]
    public class ARWorldBodyTrackingConfig:ARConfigBase
    {
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
        public ARConfigPlaneFindingMode PlaneFindingMode = ARConfigPlaneFindingMode.ENABLE;

        ///@cond ContainImageAR
        /**
         * \if english
         * Set the database of image tracking. Default is null.
         * \else
         * 设置图像跟踪的数据库。
         * \endif
         */
        public ARAugmentedImageDatabase AugmentedImageDatabase = null;
        ///@endcond

        internal override ARAugmentedImageDatabase GetAugImgDataBaseHandle() { return AugmentedImageDatabase; }

        internal override ARConfigPlaneFindingMode GetPlaneFindingMode() { return PlaneFindingMode; }
        internal override void SetPlaneFindingMode(ARConfigPlaneFindingMode mode)
        {
            PlaneFindingMode = mode;
        }
        internal override int GetARType() { return (int)NDKARType.BODY_AR|(int)NDKARType.WORLD_AR; }
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
            return string.Format("Config Type:{0}, LightingMode:{1}, UpdateMode:{2}, PlanFindingMode:{3}, PowerMode:{4} FocusMode:{5}",
                "BodyWordTracking",  LightingMode, UpdateMode, PlaneFindingMode, PowerMode, FocusMode);
        }
        ///@endcond
    }
}
