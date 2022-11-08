namespace HuaweiARUnitySDK
{
    using HuaweiARInternal;

    /**
     * \if english
     * @brief A selector used to change used engine. 
     * 
     * Through this class, you can choose HUAWEI AR Engine as the running engine. 
     * If you do not want switch engines, just skip this class. And the default engine is HUAWEI AR Engine. 
     * <b>Note: Only worldAR can use both engines. Other scences such as handAR, bodyAR and faceAR is only supported by 
     * HUAWEI AR Engine.</b>
     * \else
     * @brief 用于切换底层的引擎。
     * 
     * 通过该类，应用可以选择HUAWEI AR Engine作为底层运行的引擎。如果应用不希望切换引擎，可以跳过该类。
     * 默认使用的引擎是HUAWEI AR Engine。
     * <b>注意：只有worldAR的功能在两个引擎中可以互通。其他的功能，例如，handAR，bodyAR和faceAR仅在HUAWEI AR Engine中可用。</b>
     * \endif
     */
    public class AREnginesSelector
    {

        private AREnginesSelectorAdapter m_adapter;

        private static AREnginesSelector s_executorSelector;
        private AREnginesSelector()
        {
            m_adapter = new AREnginesSelectorAdapter();
        }

        /**
         * \if english
         * @brief The instance of this AREnginesSelector.
         * \else
         * @brief AREnginesSelector的实例。
         * \endif
         */
        public static AREnginesSelector Instance
        {
            get
            {
                if (s_executorSelector == null)
                {
                    s_executorSelector = new AREnginesSelector();
                }
                return s_executorSelector;
            }
        }

        /**
         * \if english
         * @brief Check the device ability for all engines synchronously.
         * @return Which engines are supported by this device.
         * \else
         * @brief 同步地检查对多引擎的支持情况。
         * @return 设备支持的多引擎。
         * \endif
         */
        public AREnginesAvaliblity CheckDeviceExecuteAbility()
        {
            return m_adapter.CheckDeviceExecuteAbility();
        }

        /**
         * \if english
         * @brief Set the used engine.
         * 
         * Due to difference of devices, the engine actually used may be different from user's designation. 
         * Application should firstly call \link CheckDeviceExecuteAbility \endlink to find a supported engine.
         * @param executor The engine that user want to adopt.
         * @return The engine actually used.
         * \else
         * @brief 设定使用的引擎。
         * 
         * 由于设备的差异性，实际使用的引擎可能与用户指定的引擎不同。应用应该首先调用\link CheckDeviceExecuteAbility \endlink
         * 以便查找当前设备的支持的引擎。
         * @param executor 用户希望使用的引擎。
         * @return 实际使用的引擎。
         * \endif
         */
        public AREnginesType SetAREngine(AREnginesType executor)
        {
            return m_adapter.SetAREngine(executor);
        }

        /**
         * \if english
         * @brief Get currently used engine.
         * @return The engine used currently.
         * \else
         * @brief 获取当前使用的引擎。
         * @return 当前使用的引擎。
         * \endif
         */
        public AREnginesType GetCreatedEngine()
        {
            return m_adapter.GetCreatedEngine();
        }
    }

    /**
     * \if english
     * @brief Engine type
     * \else
     * @brief 引擎的类型。
     * \endif
     */
    public enum AREnginesType
    {
        /**
         * \if english
         * Invalid type.
         * \else
         * 无效的类型。
         * \endif
         */
        NONE = 0,
        /**
         * \if english
         * HUAWEI AR ENGINE.
         * \else
         * HUAWEI AR ENGINE。
         * \endif
         */
        HUAWEI_AR_ENGINE = 1,
    }

    /**
     * \if english
     * @brief Engine avaliblity
     * \else
     * @brief 设备对引擎支持性。
     * \endif
     */
    public enum AREnginesAvaliblity
    {
        /**
         * \if english
         * @brief Support no engines.
         * \else
         * @brief 不支持任何引擎。
         * \endif
         */
        NONE_SUPPORTED = 0,
        /**
         * \if english
         * @brief Support HUAWEI AR ENGINE.
         * \else
         * @brief 支持HUAWEI AR Engine。
         * \endif
         */
        HUAWEI_AR_ENGINE = 1<<0,
    }
}
