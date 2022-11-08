
namespace HuaweiARUnitySDK
{
    using System;
    using System.Runtime.Serialization;
    /**
     * \if english
     * @brief HUAWEI AR Engine is unavailable.
     * \else
     * @brief HUAWEI AR Engine不可用。
     * \endif
     */
    public class ARUnavailableException:ApplicationException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARUnavailableException() : base() { }
        public ARUnavailableException(string message) : base(message) { }

        public ARUnavailableException(string message, Exception innerException) : base(message, innerException) { }
        protected ARUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
