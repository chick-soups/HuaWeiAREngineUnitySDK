
namespace HuaweiARUnitySDK
{
    using System;
    using System.Runtime.Serialization;
    
    /**
     * \if english
     * @brief Thrown when HUAWEI AR Engine is not installed.
     * \else
     * @brief 当引擎没有安装时抛出。
     * \endif
     */
    public class ARUnavailableServiceNotInstalledException:ARUnavailableException
	{
        ///@cond EXCLUDE_DOXYGEN
        public ARUnavailableServiceNotInstalledException() : base() { }
        public ARUnavailableServiceNotInstalledException(string message) : base(message) { }

        public ARUnavailableServiceNotInstalledException(string message, Exception innerException) : base(message, innerException) { }
        protected ARUnavailableServiceNotInstalledException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}

