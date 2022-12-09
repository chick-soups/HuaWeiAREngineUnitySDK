using System;
namespace HuaweiARUnitySDK
{
    [Flags]
    public enum ARConfigEnableItem
    {
        ENABLE_NULL = 0,
        ENABLE_DEPTH = 1 << 0,
        ENABLE_MASK = 1 << 1,
        ENABLE_MESH = 1 << 2,
        ENABLE_CLOUD_ANCHOR = 1 << 4,
        ENABLE_CLOUD_AUGMENTED_IMAGE = 1 << 5,
        ENABLE_HEALTH_DEVICE = 1 << 6,
        ENABLE_FLASH_MODE_TORCH = 1 << 7,
        ENABLE_CLOUD_OBJECT_RECOGNITION = 1 << 10
    }
}
