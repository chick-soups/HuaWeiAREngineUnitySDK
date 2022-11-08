 namespace HuaweiARInternal
{
    using HuaweiARUnitySDK;
    using UnityEngine;
    using System;
    using System.Collections.Generic;
    internal class ARSessionManager
    {
        private static ARSessionManager s_sessionManager;
        public NDKSession m_ndkSession;

        internal ARSessionStatus SessionStatus { get; private set; }
        internal bool DisplayGeometrySet = false;

        public Texture2D BackgroundTexture { get; private set; }

        private ARSessionManager() { }

        public static ARSessionManager Instance {
            get {
                if (s_sessionManager == null)
                {
                    s_sessionManager = new ARSessionManager();
                    s_sessionManager.m_ndkSession = null;
                    s_sessionManager.SessionStatus = ARSessionStatus.STOPPED;
                }
                return s_sessionManager;
            }
        }

        public bool IsCreated()
        {
            return m_ndkSession!= null && SessionStatus != ARSessionStatus.STOPPED;
        }

        public void CreateSession()
        {
            m_ndkSession = new NDKSession();
            SessionStatus = ARSessionStatus.INIT;
        }

        public Matrix4x4 GetProjectionMatrix(float nearClipPlane, float farClipPlane)
        {
            if (IntPtr.Zero == m_ndkSession.FrameHandle)
            {
                return Matrix4x4.identity;
            }
            IntPtr cameraPtr = m_ndkSession.FrameAdapter.AcquireCameraHandle();
            Matrix4x4 projectionMatrix = m_ndkSession.CameraAdapter.GetProjectionMatrix(cameraPtr, nearClipPlane,
                farClipPlane);
            m_ndkSession.CameraAdapter.Release(cameraPtr);
            return projectionMatrix;
        }

        public void SetDisplayGeometry(int width,int height)
        {
            if(ARSessionStatus.STOPPED == SessionStatus)
            {
                ARDebug.LogWarning("Session is stopped when SetDisplayGeometry, ignore it");
                return;
            }
            m_ndkSession.SessionAdapter.SetDisplayGeometry(Screen.orientation, width, height);
            DisplayGeometrySet = true;
        }

        public void SetDisplayGeometry(ScreenOrientation orientation,int width, int height)
        {
            if (ARSessionStatus.STOPPED == SessionStatus)
            {
                ARDebug.LogWarning("Session is stopped when SetDisplayGeometry, ignore it");
                return;
            }
            m_ndkSession.SessionAdapter.SetDisplayGeometry(orientation, width, height);
            DisplayGeometrySet = true;
        }

        public void Resume()
        {
            if (ARSessionStatus.STOPPED == SessionStatus)
            {
                ARDebug.LogWarning("Session is stopped when resume, ignore it");
                return;
            }
            if (!AndroidPermissionsRequest.IsPermissionGranted("android.permission.CAMERA"))
            {
                throw new ARCameraPermissionDeniedException();
            }
            m_ndkSession.SessionAdapter.Resume();
            SessionStatus = ARSessionStatus.RESUMED;
        }

        public void Pause()
        {
            if (ARSessionStatus.STOPPED == SessionStatus)
            {
                ARDebug.LogWarning("session is stopped when pause, ignore it");
                return;
            }
            m_ndkSession.SessionAdapter.Pause();
            SessionStatus = ARSessionStatus.PAUSED;
        }


        public void Stop()
        {
            if (ARSessionStatus.STOPPED == SessionStatus)
            {
                return;
            }
            m_ndkSession.SessionAdapter.Stop();
            s_sessionManager = null;
            SessionStatus = ARSessionStatus.STOPPED;
        }

        public void SetCameraTextureName()
        {
            int id = ARUnityHelper.Instance.GetTextureId();
            m_ndkSession.SessionAdapter.SetCameraTextureName(id);
            if (BackgroundTexture == null)
            {
                BackgroundTexture = Texture2D.CreateExternalTexture(0, 0, TextureFormat.ARGB32, false,
                    false, new IntPtr(id));
                return;
            }
            BackgroundTexture.UpdateExternalTexture(new IntPtr(id));
        }
        public void Update()
        {
            if(SessionStatus!=ARSessionStatus.RESUMED&&
                SessionStatus != ARSessionStatus.RUNNING)
            {
                ARDebug.LogWarning("update when state is not resumed or running, ignore it");
                return;
            }
            m_ndkSession.SessionAdapter.Update();
            SessionStatus = ARSessionStatus.RUNNING;
        }

        public  ARAnchor AddAnchor(Pose pose)
        {
            if (ARSessionStatus.RUNNING != Instance.SessionStatus)
            {
                ARDebug.LogWarning("add anchor when session is not running, ignore it");
                throw new ARNotYetAvailableException();
            }
            return  Instance.m_ndkSession.SessionAdapter.CreateAnchor(pose);
        }
        public void Config(ARConfigBase config)
        {
            if (ARSessionStatus.PAUSED != Instance.SessionStatus &&
                ARSessionStatus.INIT != Instance.SessionStatus)
            {
                ARDebug.LogWarning("config when session is not init or paused, ignore it");
            }
            Instance.m_ndkSession.SessionAdapter.Config(config);
        }
    }
}
