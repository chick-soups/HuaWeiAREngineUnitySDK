namespace HuaweiARUnitySDK
{
    using System;
    using System.Runtime.Serialization;

    /**
     * \if english
     * @brief HUAWEI AR Engine is not compatible with current device.
     * \else
     * @brief 当前硬件设备不支持HUAWEI AR Engine。
     * \endif
     */
    public class ARUnavailableDeviceNotCompatibleException: ARUnavailableException
    {
        ///@cond EXCLUDE_DOXYGEN
		public ARUnavailableDeviceNotCompatibleException ():base(){	}
        public ARUnavailableDeviceNotCompatibleException(string message):base(message) { }

        public ARUnavailableDeviceNotCompatibleException(string message, Exception innerException) : base(message, innerException) { }
        protected ARUnavailableDeviceNotCompatibleException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}

