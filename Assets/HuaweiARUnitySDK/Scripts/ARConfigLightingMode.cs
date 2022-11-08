namespace HuaweiARUnitySDK
{
    /**
     * \if english
     * @brief Selects the behavior of the lighting estimation subsystem.
     * \else
     * @brief 指定光照估计的行为。
     * \endif
     */
    public enum ARConfigLightingMode
    {
        /**
         * \if english
         * @brief  LightEstimate is disabled.
         * \else
         * @brief 关闭光线估计。
         * \endif
         */
        DISABLED=0,
        /**
         * \if english
         * @brief  LightEstimate is enabled.
         * \else
         * @brief 启动光线估计。
         * \endif
         */
        AMBIENT_INTENSITY = 1,
    }
}
