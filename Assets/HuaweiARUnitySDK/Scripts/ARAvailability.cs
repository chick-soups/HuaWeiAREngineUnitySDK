namespace HuaweiARUnitySDK
{
    /**
     * \if english
     * @brief An Enumration of device availability.
     * 
     * The value of enumration is usually returned by \link AREnginesApk.CheckAvailability() \endlink to identify
     * whether this device or OS supports for HUAWEI AR Engine.
     * \else
     * @brief 设备能力的枚举值。
     * 
     * 该值通常由\link AREnginesApk.CheckAvailability()\endlink 返回，用来表明当前设备或者OS是否支持HUAWEI AR Engine。
     * \endif
     */
    public enum ARAvailability
    {
        /**
         * \if english
         * Unknown error.
         * \else
         * 未知错误。
         * \endif
         */
        UNKNOWN_ERROR = 0,
        /**
         * \if english
         * Device availability is unknown, and the check is still in processing.
         * \else
         * 设备能力未知，仍在检查中。
         * \endif
         */
        UNKNOWN_CHECKING = 1,
        /**
         * \if english
         * Device availability is unknown, and the check is time out.
         * \else
         * 设备能力未知，检查超时。
         * \endif
         */
        UNKNOWN_TIMED_OUT = 2,
        /**
         * \if english
         * HUAWEI AR Engine is not supported by this device.
         * \else
         * 当前设备不支持HUAWEI AR Engine。
         * \endif
         */
        UNSUPPORTED_DEVICE_NOT_CAPABLE = 100,
        /**
         * \if english
         * HUAWEI AR Engine is not supported by the EMUI on this device.
         * \else
         * 当前设备上的EMUI不支持HUAWEI AR Engine。
         * \endif
         */
        UNSUPPORTED_EMUI_NOT_CAPABLE = 5000,
        /**
         * \if english
         * HUAWEI AR Engine is supported by this device and EMUI, but it's not installed.
         * \else
         * 当前设备和EMUI支持HUAWEI AR Engine，但未安装。
         * \endif
         */
        SUPPORTED_NOT_INSTALLED = 201,
        /**
         * \if english
         * HUAWEI AR Engine is supported by this device and EMUI, but the installed version is too old.
         * \else
         * 当前设备和EMUI支持HUAWEI AR Engine，但是安装的版本过旧。
         * \endif
         */
        SUPPORTED_APK_TOO_OLD = 202,
        /**
         * \if english
         * HUAWEI AR Engine is supported by this device and EMUI. It's already installed and compatiable.
         * \else
         * 当前设备和EMUI支持HUAWEI AR Engine，并且安装的版本正常。
         * \endif
         */
        SUPPORTED_INSTALLED = 203
    }
}
