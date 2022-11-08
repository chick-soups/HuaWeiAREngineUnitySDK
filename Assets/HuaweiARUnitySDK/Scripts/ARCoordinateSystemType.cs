namespace HuaweiARUnitySDK
{
    /**
     * \if english
     * @brief Coordinate system used by HUAWEI AR Engine.
     * \else
     * @brief HUAWEI AR Engine所使用的坐标空间。
     * \endif
     */
    public enum ARCoordinateSystemType
    {
        /**
         * \if english
         * Coordinate system is unkown or unsupported.
         * \else
         * 未知或者不支持的坐标空间。
         * \endif
         */
        COORDINATE_SYSTEM_TYPE_UNKNOWN = -1,
        /**
         * \if english
         * World coordinate system.
         * \else
         * 世界坐标系。
         * \endif
         */
        COORDINATE_SYSTEM_TYPE_3D_WORLD = 0,
        /**
         * \if english
         * Model or self coordinate system.
         * \else
         * 模型或者物体自身坐标系。
         * \endif
         */
        COORDINATE_SYSTEM_TYPE_3D_SELF = 1,
        /**
         * \if english
         * OpenGL Normalized Device Coordinates (NDC) coordinate system.
         * \else
         * OpenGL NDC 坐标系。
         * \endif
         */
        COORDINATE_SYSTEM_TYPE_2D_IMAGE = 2,
        /**
         * \if english
         * Camera(eye) space.
         * \else
         * 相机坐标系。
         * \endif
         */
        COORDINATE_SYSTEM_TYPE_3D_CAMERA = 3,
    }
}
