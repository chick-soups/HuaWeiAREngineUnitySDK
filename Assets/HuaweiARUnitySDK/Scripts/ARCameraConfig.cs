namespace HuaweiARUnitySDK
{
    using System;
    using HuaweiARInternal;
    using UnityEngine;
    using System.Collections.Generic;

    /** 
     * \if english
     * @brief Provides details of a camera configuration such as size of the CPU image and GPU texture.
     * \else 
     * @brief 提供相机的配置，例如，CPU上图片的宽高，GPU上贴图（预览纹理）的宽高。
     * \endif
     */
    public class ARCameraConfig
    {
        internal IntPtr m_cameraConfigHandle = IntPtr.Zero;
        internal NDKSession m_ndkSession;

        internal ARCameraConfig()
        {
        }
        internal ARCameraConfig(IntPtr cameraConfigHandle, NDKSession session)
        {
            m_cameraConfigHandle = cameraConfigHandle;
            m_ndkSession = session;
        }
        ~ARCameraConfig()
        {
            destory();
        }

        private void create()
        {
            m_cameraConfigHandle = m_ndkSession.CameraConfigAdapter.Create();
        }

        private void destory()
        {
            m_ndkSession.CameraConfigAdapter.Destory(m_cameraConfigHandle);
            m_cameraConfigHandle = IntPtr.Zero;
        }

        /**
         * \if english
         * @brief Get the dimensions of the CPU-accessible image byte (\link ARCameraImageBytes \endlink)for the camera configuration.
         * @return The dimensions of the image. \c x is the width. \c y is the height.
         * \else
         * @brief 获取CPU上可访问的图片（\link ARCameraImageBytes \endlink）的宽高。
         * @return 图片的宽高。\c x 分量是宽，\c y 分量是高。
         * \endif
         */
        public Vector2Int GetImageDimensions()
        {
            return m_ndkSession.CameraConfigAdapter.GetImageDimensions(m_cameraConfigHandle);
        }

        /**
         * \if english
         * @brief Get the dimensions of the GPU-accessible external texture (namely the camera preview \link ARFrame.CameraTexture \endlink)
         * for the camera configuration.
         * @return The dimensions of the external texture. \c x is the width. \c y is the height.
         * \else
         * @brief 获取GPU上可访问的外部纹理（也就是相机预览\link ARFrame.CameraTexture \endlink）的宽高。
         * @return 纹理的宽高。\c x 分量是宽，\c y 分量是高。
         * \endif
         */
        public Vector2Int GetTextureDimensions()
        {
            return m_ndkSession.CameraConfigAdapter.GetTextureDimensions(m_cameraConfigHandle);
        }

    }
}
