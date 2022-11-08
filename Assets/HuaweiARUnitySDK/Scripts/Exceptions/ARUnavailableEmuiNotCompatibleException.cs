
namespace HuaweiARUnitySDK
{
    using System;
    using System.Runtime.Serialization;
    /**
     * \if english
     * @brief HUAWEI AR Engine is not compatible with current OS.
     * \else
     * @brief 当前系统不支持HUAWEI AR Engine。
     * \endif
     */
    public class ARUnavailableEmuiNotCompatibleException:ARUnavailableException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARUnavailableEmuiNotCompatibleException() : base() { }
        public ARUnavailableEmuiNotCompatibleException(string message) : base(message) { }

        public ARUnavailableEmuiNotCompatibleException(string message, Exception innerException) : base(message, innerException) { }
        protected ARUnavailableEmuiNotCompatibleException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
