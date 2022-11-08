/*
 * Copyright 2018 Google Inc. All Rights Reserved.
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
namespace HuaweiARUnitySDK
{
	using HuaweiARInternal;
	using System;
	using UnityEngine;
	using System.Collections.Generic;

    /**
     * \if english 
     * @brief A CPU-accessible camera image with the format of YUV-420-888. 
     * \else
     * @brief 一个CPU可访问的YUV-420-888的图片。
     * \endif
     */
    public class ARCameraImageBytes
	{
		private IntPtr m_ImageHandle;
        private NDKSession m_ndkSession;

        internal ARCameraImageBytes(IntPtr imageHandle, NDKSession session)
		{
			m_ImageHandle = imageHandle;
            m_ndkSession = session;
            if (m_ImageHandle != IntPtr.Zero)
            {
                int width, height;
                IntPtr y, u, v;
                int yRowStride, uvPixelStride, uvRowStride;
                m_ndkSession.ImageAdapter.GetImageBuffer(m_ImageHandle, out width, out height,
                    out y, out u, out v, out yRowStride, out uvPixelStride, out uvRowStride);

                IsAvailable = true;
                Width = width;
                Height = height;
                Y = y;
                U = u;
                V = v;
                YRowStride = yRowStride;
                UVPixelStride = uvPixelStride;
                UVRowStride = uvRowStride;
            }
            else
            {
                IsAvailable = false;
                Width = Height = 0;
                Y = U = V = IntPtr.Zero;
                YRowStride = UVPixelStride = UVRowStride = 0;
            }

        }
        /**
         * \if english
         * @brief Gets a value indicating whether the image bytes are available. The struct should not 
         * be accessed if this value is \c false.
         * \else
         * @brief 获取当前结构体数据的可用性。
         * \endif
         */
        public bool IsAvailable { get; private set; }

        /**
         * \if english
         * @brief Gets the width of the image.
         * \else
         * @brief 获取图片的宽。
         * \endif
         */
        public int Width { get; private set; }


        /**
         * \if english
         * @brief Gets the height of the image.
         * \else
         * @brief 获取图片的高。
         * \endif
         */
        public int Height { get; private set; }

        /**
         * \if english
         * @brief Gets a pointer to the Y buffer with a pixel stride of 1 and a row stride of \link YRowStride \endlink.
         * \else
         * @brief 获取Y分量的指针，像素步长为1，行的步长为\link YRowStride \endlink。
         * \endif
         */
        public IntPtr Y { get; private set; }

        /**
         * \if english
         * @brief Gets a pointer to the U buffer with a pixel stride of \link UVPixelStride \endlink and a row stride of \link UVRowStride \endlink.
         * \else
         * @brief 获取U分量的指针，像素步长为\link UVPixelStride \endlink，行的步长为\link UVRowStride \endlink。
         * \endif
         */
        public IntPtr U { get; private set; }

        /**
         * \if english
         * @brief Gets a pointer to the V buffer with a pixel stride of \link UVPixelStride \endlink and a row stride of \link UVRowStride \endlink.
         * \else
         * @brief 获取V分量的指针，像素步长为\link UVPixelStride \endlink，行的步长为\link UVRowStride \endlink。
         * \endif
         */
        public IntPtr V { get; private set; }

        /**
         * \if english
         * @brief Gets the row stride of the Y plane.
         * \else
         * @brief 获取Y分量行步长。
         * \endif
         */
        public int YRowStride { get; private set; }

        /**
         * \if english
         * @brief Gets the pixel stride of the U and V planes.
         * \else
         * @brief 获取UV的像素步长。
         * \endif
         */
        public int UVPixelStride { get; private set; }

        /**
         * \if english
         * @brief Gets the row stride of the U and V planes.
         * \else
         * @brief 获取UV的行步长。
         * \endif
         */
        public int UVRowStride { get; private set; }

        /**
         * \if english
         * @brief Releases the camera image and associated resources, and signifies the developer will no longer access those resources.
         * \else
         * @brief 释放相机图片以及相关的资源，释放后资源不在可用。
         * \endif
         */
        public void Release()
		{
			if (m_ImageHandle != IntPtr.Zero)
			{
                m_ndkSession.ImageAdapter.Release(m_ImageHandle);
				m_ImageHandle = IntPtr.Zero;
			}
		}

        /**
         * \if english
         * @brief Calls release as part of IDisposable pattern supporting 'using' statements.
         * \else
         * @brief 释放相机图片。
         * \endif
         */
        public void Dispose()
		{
			Release();
		}
	}
}
