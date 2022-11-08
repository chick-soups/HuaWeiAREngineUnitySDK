namespace HuaweiARUnitySDK
{
    using System;
    using System.Runtime.Serialization;

    /**
     * \if english
     * @brief Thrown when input configuration is not supported in \link ARSession.Config(ARConfigBase)\endlink.
     * \else
     * @brief \link ARSession.Config(ARConfigBase)\endlink的配置项不被支持时抛出。
     * \endif
     */
    class ARUnSupportedConfigurationException :ApplicationException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARUnSupportedConfigurationException() : base() { }
        public ARUnSupportedConfigurationException(string message) : base(message) { }

        public ARUnSupportedConfigurationException(string message, Exception innerException) : base(message, innerException) { }
        protected ARUnSupportedConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
