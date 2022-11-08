namespace HuaweiARUnitySDK
{
    using HuaweiARInternal;
    using System;
    using UnityEngine;

    /**
     * \if english
     * @brief This abstract class is the base of all configuration used in \link ARSession.Config(ARConfigBase) \endlink.
     * \else
     * @brief 该抽象类是所有配置类的基类。
     * \endif
     */
    public abstract class ARConfigBase: ScriptableObject
    {
        /**\if english
         * Lighting mode of an configuration object. Default value is AMBIENT_INTENSITY。
         * \else
         * 配置项的光照模式。默认启动环境光。
         * \endif
         */
        public ARConfigLightingMode LightingMode = ARConfigLightingMode.AMBIENT_INTENSITY;

        /**\if english
         * Update mode of an configuration object. Default value is BLOCKING。
         * \else
         * 配置项的更新模式。默认阻塞模式。
         * \endif
         */
        public ARConfigUpdateMode UpdateMode = ARConfigUpdateMode.BLOCKING;

        /**\if english
         * Power mode of an configuration object. Default value is NORMAL。
         * \else
         * 配置项的功耗模式。默认不使用低功耗。
         * \endif
         */
        public ARConfigPowerMode PowerMode = ARConfigPowerMode.NORMAL;

        /**\if english
         * Item of depth switch of an configuration object. Default value is \c true。
         * \else
         * 配置项的深度开关。默认打开深度流。
         * \endif
         */
        public bool EnableDepth = true;
        /**\if english
         * Item of mask switch an configuration object. Default value is \c false.
         * \else
         * 配置项的遮罩开关。默认关闭遮罩功能。
         * \endif
         */
        public bool EnableMask = false;
		/**\if english
         * Item of scenemesh switch an configuration object. Default value is \c false.
         * \else
         * 配置项的环境Mesh开关。默认关闭环境Mesh。
         * \endif
         */
        public bool EnableMesh = false;

        /**\if english
         * Enable semantic plane mode. Default value is \c false.
         * \else
         * 配置使能语义识别平面模式。默认关闭。
         * \endif
         */
        public bool SemanticPlaneMode = false;

        /**\if english
         * The way the configuration item is opened by the camera. The camera is turned on internally by default.
         * \else
         * 配置项的相机打开方式。默认内部打开相机。
         * \endif
         */
        public int ImageInputMode = 0;

        internal virtual ARAugmentedImageDatabase GetAugImgDataBaseHandle() { return null; }
        internal abstract int GetARType();
        internal virtual ARConfigPlaneFindingMode GetPlaneFindingMode() { return ARConfigPlaneFindingMode.DISABLE; }
        internal virtual ARConfigCameraLensFacing GetCameraLensFacing() { return ARConfigCameraLensFacing.REAR; }
        internal virtual ARConfigLightingMode GetLightingMode() { return LightingMode; }
        internal virtual ARConfigUpdateMode GetUpdateMode() { return UpdateMode; }
        internal virtual ARConfigPowerMode GetPowerMode() { return PowerMode; }
        internal abstract ARConfigFocusMode GetFocusMode();
        internal virtual int GetImageInputMode() { return ImageInputMode; }
        [Obsolete]
        internal virtual ARConfigHandFindingMode GetHandFindingMode() { return ARConfigHandFindingMode.DISABLED; }
        internal virtual void SetPlaneFindingMode(ARConfigPlaneFindingMode mode) { ; }
        internal virtual void SetCameraLensFacing(ARConfigCameraLensFacing lensFacing) { ; }
        internal virtual void SetLightingMode(ARConfigLightingMode lightingMode) { LightingMode=lightingMode; }
        internal virtual void SetUpdateMode(ARConfigUpdateMode updateMode) { UpdateMode = updateMode; }
        internal virtual void SetPowerMode(ARConfigPowerMode powerMode) { PowerMode = powerMode; }
        internal virtual void SetImageInputMode(int imageInputMode) { ImageInputMode = imageInputMode; }
        internal abstract void SetFocusMode(ARConfigFocusMode focusMode);
        [Obsolete]
        internal virtual void SetHandFindingMode(ARConfigHandFindingMode mode) { ; }

        internal const int EnableItem_None = 0;
        internal const int EnableItem_Depth = 1 << 0;
        internal const int EnableItem_Mask = 1 << 1;
        internal const int EnableItem_Mesh = 1 << 2;
        internal virtual ulong GetConfigEnableItem()
        {
            ulong ret = EnableItem_None;
            ret = EnableDepth ? ret | EnableItem_Depth : ret;
            ret = EnableMask ? ret | EnableItem_Mask : ret;
            ret = EnableMesh ? ret | EnableItem_Mesh : ret;
            return ret;
        }

        internal const int EnableSemanticModeNone = 0;
        internal const int EnableSemanticPlaneMode = 1 << 0;

        internal virtual int GetConfigSemanticMode()
        {
            int ret = EnableSemanticModeNone;
            ret = SemanticPlaneMode ? ret | EnableSemanticPlaneMode : ret;
            return ret;
        }
    }
}
