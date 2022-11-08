namespace HuaweiARInternal
{
    using HuaweiARUnitySDK;
    using System;
    internal class ARExceptionAdapter
    {
        public static void ExtractException(NDKARStatus status)
        {
            switch (status)
            {
                case NDKARStatus.HWAR_SUCCESS:
                    return;
                case NDKARStatus.HWAR_ERROR_INVALID_ARGUMENT:
                    throw new ArgumentException();
                case NDKARStatus.HWAR_ERROR_FATAL:
                    throw new SystemException();
                case NDKARStatus.HWAR_ERROR_SESSION_NOT_PAUSED:
                    throw new ARSessionNotPausedException();
                case NDKARStatus.HWAR_ERROR_SESSION_PAUSED:
                    throw new ARSessionPausedException();
                case NDKARStatus.HWAR_ERROR_TEXTURE_NOT_SET:
                    throw new ARTextureNotSetException();
                case NDKARStatus.HWAR_UNAVAILABLE_SDK_TOO_OLD:
                    throw new ARUnavailableClientSdkTooOldException();
                case NDKARStatus.HWAR_UNAVAILABLE_DEVICE_NOT_COMPATIBLE:
                    throw new ARUnavailableDeviceNotCompatibleException();
                case NDKARStatus.HWAR_UNAVAILABLE_EMUI_NOT_CAPABLE:
                    throw new ARUnavailableEmuiNotCompatibleException();
                case NDKARStatus.HWAR_UNAVAILABLE_APK_TOO_OLD:
                    throw new ARUnavailableServiceApkTooOldException();
                case NDKARStatus.HWAR_UNAVAILABLE_AREXECUTOR_NOT_INSTALLED:
                    throw new ARUnavailableServiceNotInstalledException();
                case NDKARStatus.HWAR_ERROR_UNSUPPORTED_CONFIGURATION:
                    throw new ARUnSupportedConfigurationException();
                case NDKARStatus.HWAR_UNAVAILABLE_USER_DECLINED_INSTALLATION:
                    throw new ARUnavailableUserDeclinedInstallationException();
                case NDKARStatus.HWAR_ERROR_CAMERA_PERMISSION_NOT_GRANTED:
                    throw new ARCameraPermissionDeniedException();
                case NDKARStatus.HWAR_ERROR_DEADLINE_EXCEEDED:
                    throw new ARDeadlineExceededException();
                case NDKARStatus.HWAR_ERROR_RESOURCE_EXHAUSTED:
                    throw new ARResourceExhaustedException();
                case NDKARStatus.HWAR_ERROR_NOT_YET_AVAILABLE:
                    throw new ARNotYetAvailableException();
                case NDKARStatus.HWAR_UNAVAILABLE_CONNECT_SERVER_TIME_OUT:
                    throw new ARUnavailableConnectServerTimeOutException();
                default:
                    throw new ApplicationException();
            }

        }
    }
}
