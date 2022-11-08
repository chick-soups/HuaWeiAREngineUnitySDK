namespace HuaweiARUnitySDK
{
    using HuaweiARInternal;
    using System;
    using UnityEngine;
    using System.Collections.Generic;

    /**
     * \if english
     * @brief Camera metadata of a \link ARFrame \endlink
     * \else
     * @brief \link ARFrame \endlink 相关的相机元数据。
     * \endif
     */
    public class ARCameraMetadata
    {
        private IntPtr m_metadataPtr;
        private NDKSession m_ndkSession;

        internal ARCameraMetadata(IntPtr metadataPtr,NDKSession session)
        {
            m_metadataPtr = metadataPtr;
            m_ndkSession = session;
        }
        /**
         * \if english
         * @brief Get all camera metadata tags.
         * @return A list of camera metadata tags.
         * \else
         * @brief 获取所有的元数据的tag。
         * @return 元数据tag的列表。
         * \endif
         */
        public List<ARCameraMetadataTag> GetAllCameraMetadataTags()
        {
            List<ARCameraMetadataTag> metadataTags = new List<ARCameraMetadataTag>();
            m_ndkSession.CameraMetadataAdapter.GetAllCameraMetadataTags(m_metadataPtr, metadataTags);
            return metadataTags;
        }

        /**
         * \if english
         * @brief Get a camera metadata of input tag.
         * @param cameraMetadataTag A camera metadata tag.
         * @return A list of camera metadata values.
         * \else
         * @brief 根据元数据的tag获取元数据值。
         * @param cameraMetadataTag 元数据tag。
         * @return 元数据tag对应的值。
         * \endif
         */
        public List<ARCameraMetadataValue> GetValue(ARCameraMetadataTag cameraMetadataTag)
        {
            List<ARCameraMetadataValue> metadataValues = new List<ARCameraMetadataValue>();
            m_ndkSession.CameraMetadataAdapter.GetValues(m_metadataPtr, cameraMetadataTag, metadataValues);
            return metadataValues;
        }

        ~ARCameraMetadata()
        {
            m_ndkSession.CameraMetadataAdapter.Release(m_metadataPtr);
        }
    }
}
