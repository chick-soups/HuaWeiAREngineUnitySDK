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

        public ARConfigEnableItem EnableItem=ARConfigEnableItem.ENABLE_DEPTH;
        [Obsolete("Use EnableItem instead.")]
        public bool EnableDepth
        {
            get
            {
                return EnableItem.HasFlag(ARConfigEnableItem.ENABLE_DEPTH);
            }
        }
        [Obsolete("Use EnableItem instead.")]
        public bool EnableMask
        {
            get
            {
                return EnableItem.HasFlag(ARConfigEnableItem.ENABLE_MASK);
            }
        }
        [Obsolete("Use EnableItem instead.")]
        public bool EnableMesh
        {
            get
            {
                return EnableItem.HasFlag(ARConfigEnableItem.ENABLE_MESH);
            }
        }
        /**\if english
         * Enable semantic plane mode. Default value is \c false.
         * \else
         * 配置使能语义识别平面模式。默认关闭。
         * \endif
         */
        public ARConfigSemanticMode SemanticMode=ARConfigSemanticMode.NONE;
        public bool  SemanticPlaneMode{
            get{
                return SemanticMode.HasFlag(ARConfigSemanticMode.PLANE);
            }
        }

        /**\if english
         * The way the configuration item is opened by the camera. The camera is turned on internally by default.
         * \else
         * 配置项的相机打开方式。默认内部打开相机。
         * \endif
         */
        public int ImageInputMode = 0;
        public Vector2Int PreviewSize=new Vector2Int(1280,720);

        internal virtual ARAugmentedImageDatabase GetAugImgDataBaseHandle() { return null; }
        internal abstract int GetARType();
        internal virtual ARConfigPlaneFindingMode GetPlaneFindingMode() { return ARConfigPlaneFindingMode.DISABLE; }
        internal virtual ARConfigCameraLensFacing GetCameraLensFacing() { return ARConfigCameraLensFacing.REAR; }
        internal virtual ARConfigUpdateMode GetUpdateMode() { return UpdateMode; }
        internal virtual ARConfigPowerMode GetPowerMode() { return PowerMode; }
        internal abstract ARConfigFocusMode GetFocusMode();
        internal virtual int GetImageInputMode() { return ImageInputMode; }
        [Obsolete]
        internal virtual ARConfigHandFindingMode GetHandFindingMode() { return ARConfigHandFindingMode.DISABLED; }
        internal virtual void SetPlaneFindingMode(ARConfigPlaneFindingMode mode) { ; }
        internal virtual void SetCameraLensFacing(ARConfigCameraLensFacing lensFacing) { ; }
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
        internal const int ENABLE_CLOUD_ANCHOR = 1 << 4;
        internal const int ENABLE_CLOUD_AUGMENTED_IMAGE = 1 << 5;
        internal const int ENABLE_HEALTH_DEVICE = 1 << 6;
        internal const int ENABLE_FLASH_MODE_TORCH = 1 << 7;
        internal const int ENABLE_CLOUD_OBJECT_RECOGNITION = 1 << 10;
        internal virtual ulong GetConfigEnableItem()
        {
            return (ulong)EnableItem;
        }

        internal const int EnableSemanticModeNone = 0;
        internal const int EnableSemanticPlaneMode = 1 << 0;
        internal const int SEMANTIC_TARGET=1<<1;
        internal virtual int GetConfigSemanticMode()
        {
            return (int)SemanticMode;
        }

        internal const int LIGHT_MODE_NONE = 0;
        internal const int LIGHT_MODE_AMBIENT_INTENSITY = 0x0001;
        internal const int LIGHT_MODE_ENVIRONMENT_LIGHTING = 0x0002;
        internal const int LIGHT_MODE_ENVIRONMENT_TEXTURE = 0x0004;
        internal const int LIGHT_MODE_ALL = 0xFFFF;


        internal virtual int GetLightingMode()
        {
            if (LightingMode.HasFlag(ARConfigLightingMode.AMBIENT_INTENSITY)
            && LightingMode.HasFlag(ARConfigLightingMode.ENVIRONMENT_LIGHTING)
            && LightingMode.HasFlag(ARConfigLightingMode.ENVIRONMENT_TEXTURE))
            {
                return LIGHT_MODE_ALL;
            }
            else
            {
                return (int)LightingMode;
            }

        }
        internal virtual void SetLightingMode(int lightingMode)
        {
            if (lightingMode == LIGHT_MODE_ALL)
            {
                LightingMode = ARConfigLightingMode.AMBIENT_INTENSITY
                | ARConfigLightingMode.ENVIRONMENT_LIGHTING
                | ARConfigLightingMode.ENVIRONMENT_TEXTURE;
            }
            else
            {
                LightingMode = (ARConfigLightingMode)lightingMode;
            }

        }
        internal virtual Vector2Int GetPreviewSize(){
            return PreviewSize;
        }
        internal virtual void SetPreviewSize(Vector2Int previewSize){
            PreviewSize=previewSize;
        }
    }
}
