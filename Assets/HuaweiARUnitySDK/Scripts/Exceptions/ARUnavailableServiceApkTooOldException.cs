namespace HuaweiARUnitySDK
{
    using System;
    using System.Runtime.Serialization;

    /**
     * \if english
     * @brief Thrown when installed service is too old to be compatiable with current used sdk.
     * \else
     * @brief 当安装的引擎过旧，与当前使用的SDK不兼容时抛出该异常
     * \endif
     */
    class ARUnavailableServiceApkTooOldException:ARUnavailableException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARUnavailableServiceApkTooOldException() : base() { }
        public ARUnavailableServiceApkTooOldException(string message) : base(message) { }

        public ARUnavailableServiceApkTooOldException(string message, Exception innerException) : base(message, innerException) { }
        protected ARUnavailableServiceApkTooOldException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
