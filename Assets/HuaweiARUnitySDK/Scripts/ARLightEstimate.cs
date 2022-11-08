namespace HuaweiARUnitySDK
{
    /**
     * \if english
     * @brief An estimation of lighting conditinos in the environment coorespoding to an \link ARFrame \endlink.
     * \else
     * @brief 与\link ARFrame\endlink相关的环境光估计。
     * \endif
     */
    public class ARLightEstimate
    {
        /**
         * \if english
         * @brief The intensity of environment light.
         * \else
         * @brief 环境光强。
         * \endif
         */
        public float PixelIntensity { get; private set; }

        /**
         * \if english
         * @brief Indicate whether this data is valid.
         * \else
         * @brief 数据是否有效。
         * \endif
         */
        public bool Valid { get; private set; }

        ///@cond EXCLUDE_DOXYGEN
        public ARLightEstimate(bool valid,float intensity)
        {
            Valid = valid;
            PixelIntensity = intensity;
        }
        ///@endcond
    }
}
