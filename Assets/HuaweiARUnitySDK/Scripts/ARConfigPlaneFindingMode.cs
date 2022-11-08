namespace HuaweiARUnitySDK
{
    /**
     * \if english
     * @brief Selects the behavior of the plane detection subsystem.
     * \else
     * @brief 指定平面检测的行为。
     * \endif
     */
    public enum ARConfigPlaneFindingMode
    {
        /**
         * \if english
         * @brief Disable the plane detection subsystem.
         * \else
         * @brief 关闭平面检测。
         * \endif
         */
        DISABLE = 0,
        /**
         * \if english
         * @brief Detect only horizontal planes.
         * \else
         * @brief 仅识别水平面。
         * \endif
         */
        HORIZONTAL_ONLY = 1,
        /**
         * \if english
         * @brief Detect only vertical planes.
         * \else
         * @brief 仅识别垂直面。
         * \endif
         */
        VERTICAL_ONLY = 2,
        /**
         * \if english
         * @brief Detect all types of planes.
         * \else
         * @brief 识别所有类型平面。
         * \endif
         */
        ENABLE = 3,
    }
}
