namespace HuaweiARUnitySDK
{
    using System;
    using System.Runtime.Serialization;
    /**
     * \if english
     * @brief Thrown when an operation requires trackable to be tracking, but it's actually not.
     * \else
     * @brief 当调用要求trackable处于跟踪状态，但实际未跟踪时抛出。
     * \endif
     */
    class ARNotTrackingException :ApplicationException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARNotTrackingException() : base() { }
        public ARNotTrackingException(string message) : base(message) { }

        public ARNotTrackingException(string message, Exception innerException) : base(message, innerException) { }
        protected ARNotTrackingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
