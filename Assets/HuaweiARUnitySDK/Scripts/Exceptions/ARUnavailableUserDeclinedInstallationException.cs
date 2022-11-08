namespace HuaweiARUnitySDK
{
    using System;
    using System.Runtime.Serialization;

    /**
     * \if english
     * @brief User declined installation.
     * \else
     * @brief 用户取消了安装。
     * \endif
     */
    class ARUnavailableUserDeclinedInstallationException :ARUnavailableException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARUnavailableUserDeclinedInstallationException() : base() { }
        public ARUnavailableUserDeclinedInstallationException(string message) : base(message) { }

        public ARUnavailableUserDeclinedInstallationException(string message, Exception innerException) : base(message, innerException) { }
        protected ARUnavailableUserDeclinedInstallationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
