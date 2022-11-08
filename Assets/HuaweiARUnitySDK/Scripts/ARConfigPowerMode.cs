namespace HuaweiARUnitySDK
{
    /**
     * \if english
     * @brief Select the behavior of power saving.
     * 
     * Due to devices' limitation, HUAWEI AR Engine may have a high power consumption in some . 
     * Since real-time and accuracy are not important in some scenes, you can select a suboptimal solution by the behavior enumerated here.
     * Currently, this feature is only available in HandAR and BodyAR when depth is enabled by \link ARConfigBase.EnableDepth \endlink.
     * \else
     * @brief 选择低功耗的模式。
     * 
     * 受设备限制，HUAWEI AR Engine在某些场景下会有比较高的功耗。由于在一些场景对数据的实时性和精确度要求不是很高，开发者可以通过这个枚举
     * 来选择一个功耗较低的方案。目前这个特性只在HandAR和BodAR启动深度（\link ARConfigBase.EnableDepth \endlink）时有效。
     * \endif
     */
    public enum ARConfigPowerMode
    {
        /**
         * \if english
         * Normal Mode.
         * \else
         * 普通模式。
         * \endif
         */
        NORMAL=0,
        /**
         * \if english
         * Power saving mode. HUAWEI AR Engine will take some measures to descrease the consumption. This may have an effect on
         * the accuracy and real-time.
         * \else
         * 低功耗模式。HUAWEI AR Engine使用一些方法降低功耗，可能影响准确性和实时性。
         * \endif
         */
        POWER_SAVING = 1,
        /**
         * \if english
         * Ultra power saving mode. HUAWEI AR Engine will have a lowest consumption and only provide necessary data.
         * \else
         * 超级低功耗模式。HUAWEI AR Engine的功耗最低，只提供必要的数据。
         * \endif
         */
        ULTRA_POWER_SAVING = 2,
        /**
          * \if english
          * Performance first Mode.The HUAWEI AR Engine has high power consumption and priority is given to accuracy and real-time performance.
          * Currently, in addition to the environmental MESH feature, this mode is equivalent to the normal mode for other AR types.
          * \else
          * 性能模式。HUAWEI AR Engine的功耗较高，优先保证准确性和实时性。
          * 当前除了环境MESH特性之外，对其他AR类型，该模式等同于普通模式。
          * \endif
          */
        PERFORMANCE_FIRST = 3,
    }
}
