namespace HuaweiARUnitySDK
{
    using System;
    using System.Runtime.Serialization;

    /**
     * \if english
     * @brief Thrown when current used sdk is too old to be compatiable with installed service .
     * \else
     * @brief 当前使用的sdk版本过旧，与安装的service不兼容。
     * \endif
     */
    public class ARUnavailableClientSdkTooOldException:ARUnavailableException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARUnavailableClientSdkTooOldException() : base() { }
        public ARUnavailableClientSdkTooOldException(string message) : base(message) { }

        public ARUnavailableClientSdkTooOldException(string message, Exception innerException) : base(message, innerException) { }
        protected ARUnavailableClientSdkTooOldException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
