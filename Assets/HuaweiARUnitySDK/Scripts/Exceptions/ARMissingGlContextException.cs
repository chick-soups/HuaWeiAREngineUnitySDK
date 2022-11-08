namespace HuaweiARUnitySDK
{
    using System;
    using System.Runtime.Serialization;
    /**
     * \if english
     * @brief Thrown when an operation requires a gl context.
     * \else
     * @brief 当调用缺少gl上下文时抛出。
     * \endif
     */
    public class ARMissingGlContextException:ApplicationException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARMissingGlContextException() : base() { }
        public ARMissingGlContextException(string message) : base(message) { }

        public ARMissingGlContextException(string message, Exception innerException) : base(message, innerException) { }
        protected ARMissingGlContextException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
