namespace HuaweiARUnitySDK
{
    public enum FaceHealthCheckState
    {
        DETECT_FAILED = -1,
        DETECT_SUCCESS = 0,
        NO_AVAILABLE_HEALTH_DATA = 1,
        FACE_WITH_EXPRESSION = 10,
        IMAGE_SIZE_WRONG = 20,
        FACE_NOT_IN_ELLIPSE = 21,
        FACE_MOTION_TOO_MUCH = 22,
        EFFECTIVE_PIXEEL_TOO_LOW = 23,
        LIGHT_TOO_DARK = 24,
        LIGHT_NOT_UNIFORM = 25,
        POSE_TOO_LARGE = 26,
        SIGNAL_CAPTURE_FAILED = 27,
        SIGNAL_NAN = 28,
        FINGER_OUTSIDE_CAMERA = 29,
        FINGER_SIGNAL_UNAVAILABLE = 30,
        FRAUD_FACE = 31,

    }
}

