namespace HuaweiARUnitySDK
{
    using System;
    using System.Runtime.Serialization;

    /**
     * \if english
     * @brief Connect to server time out.
     * \else
     * @brief 连接网络服务超时。
     * \endif
     */
    public class ARUnavailableConnectServerTimeOutException: ARUnavailableException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARUnavailableConnectServerTimeOutException() : base() { }
        public ARUnavailableConnectServerTimeOutException(string message) : base(message) { }

        public ARUnavailableConnectServerTimeOutException(string message, Exception innerException) : base(message, innerException) { }
        protected ARUnavailableConnectServerTimeOutException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
