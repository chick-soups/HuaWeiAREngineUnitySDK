namespace HuaweiARUnitySDK
{
    /**
     * \if english
     * @brief Enumeration of query filter.
     * \else
     * @brief 查询过滤器的枚举。
     * \endif
     */
    public enum ARTrackableQueryFilter
    {
        /**
         * \if english 
         * Require all items. 
         * \else
         * 需要所有的项。
         * \endif
         */
        ALL,
        
        /**
         * \if english
         * Only new items in current frame is required.
         * \else
         * 仅需要当前帧新出现的项。
         * \endif
         */
        NEW,

        /**
         * \if english
         * Only updated items in current frame is required.
         * \else
         * 仅需要当前帧变化过的项。
         * \endif
         */
        UPDATED,
    }
}
