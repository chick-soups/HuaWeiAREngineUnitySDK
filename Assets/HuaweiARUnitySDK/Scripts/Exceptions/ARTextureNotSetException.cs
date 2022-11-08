
namespace HuaweiARUnitySDK
{
    using System;
    using System.Runtime.Serialization;
    /**
     * \if english
     * @brief Thrown if \link ARSession.SetCameraTextureNameAuto()\endlink is not called before 
     * \link ARSession.Update()\endlink.
     * \else
     * @brief 调用\link ARSession.Update()\endlink前没有调用\link ARSession.SetCameraTextureNameAuto()\endlink。
     * \endif
     */
    class ARTextureNotSetException :ApplicationException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARTextureNotSetException() : base() { }
        public ARTextureNotSetException(string message) : base(message) { }

        public ARTextureNotSetException(string message, Exception innerException) : base(message, innerException) { }
        protected ARTextureNotSetException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
