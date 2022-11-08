///@cond ContainVideoBodyAR
namespace HuaweiARUnitySDK
{
    using System;
    using HuaweiARInternal;
    using UnityEngine;
    using System.Collections.Generic;

    public class ARVideoSource
    {
        private NDKSession m_ndkSession;

        private String m_videoPath;

        private IntPtr m_configHandle;
        internal ARVideoSource(String videoPath,NDKSession session)
        {
            m_ndkSession = session;
            m_configHandle = session.ConfigHandle;
            m_videoPath = videoPath;
            m_ndkSession.VideoSourceAdapter.Constructor(m_ndkSession.SessionHandle, m_configHandle, m_videoPath);
        }

        /**
         * \if english
         * @brief Initialize the player.
         * @return Vertex coordinate Vector array.
         * \else
         * @brief 初始化播放器。
         * \endif
         */
        public void InitVideoPlayer()
        {
            m_ndkSession.VideoSourceAdapter.InitPlayer();
        }
        /**
          * \if english
          * @brief Get the width of the video.
          * \else
          * @brief 获取视频宽度。
          * \endif
          */
        public int GetVideoWidth()
        {
            return m_ndkSession.VideoSourceAdapter.GetVideoWidth();
        }
        /**
          * \if english
          * @brief Get the height of the video.
          * \else
          * @brief 获取视频高度。
          * \endif
          */
        public int GetVideoHeight()
        {
            return m_ndkSession.VideoSourceAdapter.GetVideoHeight();
        }

        /**
          * \if english
          * @brief Start to play the video.
          * \else
          * @brief 开始播放视频。
          * \endif
          */
        public void StartPlayVideo()
        {
            m_ndkSession.VideoSourceAdapter.StartVideoPlay();
        }

        /**
          * \if english
          * @brief Stop to play the video.
          * \else
          * @brief 结束播放视频。
          * \endif
          */
        public void StopVideoPlay()
        {
            m_ndkSession.VideoSourceAdapter.StopVideoPlay();
        }
        /**
          * \if english
          * @brief  Pause video.
          * \else
          * @brief 暂停播放视频。
          * \endif
          */
        public void PauseVideoPlay()
        {
            m_ndkSession.VideoSourceAdapter.PauseVideoPlay();
        }
        /**
          * \if english
          * @brief  Resume playing video.
          * \else
          * @brief 暂停播放视频。
          * \endif
          */
        public void ResumeVideoPlay()
        {
            m_ndkSession.VideoSourceAdapter.ResumeVideoPlay(m_ndkSession.SessionHandle, m_configHandle);
        }
        /**
          * \if english
          * @brief  Set the video path.
          * \else
          * @brief 设置视频路径。
          * \endif
          */
        public void SetVideoPath(String videoPath)
        {
            m_ndkSession.VideoSourceAdapter.SetVideoPath(videoPath);
            m_videoPath = videoPath;
        }
        /**
          * \if english
          * @brief  Release player resources.
          * \else
          * @brief 释放播放器资源。
          * \endif
          */
        public void VideoPlayerRelease()
        {
            m_ndkSession.VideoSourceAdapter.VideoPlayerRelease();
        }

    }
}
///@endcond