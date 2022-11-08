namespace HuaweiARUnitySDK
{
    using System;
    using System.Runtime.Serialization;

    /**
     * \if english
     * @brief Thrown when cemera permission is missing.
     * \else
     * @brief 当缺少相机权限时抛出。
     * \endif
     */
    class ARCameraPermissionDeniedException : ApplicationException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARCameraPermissionDeniedException() : base() { }
        public ARCameraPermissionDeniedException(string message) : base(message) { }

        public ARCameraPermissionDeniedException(string message, Exception innerException) : base(message, innerException) { }
        protected ARCameraPermissionDeniedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
