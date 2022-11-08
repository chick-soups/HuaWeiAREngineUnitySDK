///@cond ContainShareAR
namespace HuaweiARUnitySDK
{
    using System;
    using System.Runtime.InteropServices;

    /**
     * \if english
     * @brief The shared data that can be used in persistence AR and multi-user AR.
     * \else
     * @brief 共享数据，可以用于存储实现持久化AR或者多个机器之间共享实现多人AR。
     * \endif
     */
    public class ARSharedData
    {
        /**
         * \if english
         * Raw data.
         * \else
         * 原始数据。
         * \endif
         */
        public ARRawData RawData { get; private set; }

        internal ARSharedData(ARRawData rawData)
        {
            RawData = rawData;
        }

        /**
         * \if english
         * @brief Raw data of shared data.
         * \else
         * @brief 共享数据的原始数据。
         * \endif
         */
        public class ARRawData
        {
            /**
             * \if english
             * @brief The used size of raw data.
             * \else
             * @brief 原始数据的真实长度。
             * \endif
             */
            public long DataSize;

            /**
             * \if english
             * @brief The capacity of raw data.
             * \else
             * @brief 原始数据的缓冲容量。
             * \endif
             */
            public long DataCapacity { get; private set; }
            
            /**
             * \if english
             * @brief The address of raw data.
             * \else
             * @brief 原始数据的数组。
             * \endif
             */
            public byte[] Data { get; private set; }
            private GCHandle m_gcHandle;
            internal IntPtr m_pinAddr { get; private set; }

            /**
             * \if english
             * @brief Constructor of raw data.
             * @param capacity Capacity of raw data cache.
             * \else
             * @brief 原始数据的构造函数。
             * @param capacity 原始数据缓冲区容量。
             * \endif
             */
            public ARRawData(long capacity)
            {
                DataCapacity = capacity;
                Data = new byte[DataCapacity];
                m_gcHandle = GCHandle.Alloc(Data, GCHandleType.Pinned);
                m_pinAddr = m_gcHandle.AddrOfPinnedObject();
            }

            ~ARRawData()
            {
                m_gcHandle.Free();
            }
        }
    }
}
///@endcond