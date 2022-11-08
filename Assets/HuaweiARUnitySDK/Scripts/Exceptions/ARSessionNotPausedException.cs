namespace HuaweiARUnitySDK
{
    using System;
    using System.Runtime.Serialization;

    /**
     * \if english
     * @brief Thrown when an operation requires HUAWEI AR Engine to be paused, but it's actually not.
     * \else
     * @brief 当一个调用要求引擎处于暂时状态，但是实际上处于其他状态时，抛出。
     * \endif
     */
    class ARSessionNotPausedException : ApplicationException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARSessionNotPausedException() : base() { }
        public ARSessionNotPausedException(string message) : base(message) { }

        public ARSessionNotPausedException(string message, Exception innerException) : base(message, innerException) { }
        protected ARSessionNotPausedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
