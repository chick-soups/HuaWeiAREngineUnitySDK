namespace HuaweiARUnitySDK
{
    using System;
    using HuaweiARInternal;
    using UnityEngine;
    using System.Collections.Generic;

    public class ARCameraIntrinsics : IDisposable
    {
        internal IntPtr m_CameraIntrinsicsHandle = IntPtr.Zero;
        internal ARCameraIntrinsicsAdapter m_Adapter;
        private bool m_Disposed;

        internal ARCameraIntrinsics(IntPtr cameraIntrinsicsHandle, ARCameraIntrinsicsAdapter adapter)
        {
            m_CameraIntrinsicsHandle = cameraIntrinsicsHandle;
            m_Adapter = adapter;
        }
        ~ARCameraIntrinsics()
        {
            Dispose(false);
        }

        public Vector2Int GetImageDimensions()
        {
            return m_Adapter.GetImageDimensions(m_CameraIntrinsicsHandle);
        }

        public Vector2 GetPrincipalPoint()
        {
            return m_Adapter.GetPrincipalPoint(m_CameraIntrinsicsHandle);
        }

        public Vector2 GetFocalLength()
        {
            return m_Adapter.GeFocalLength(m_CameraIntrinsicsHandle);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!m_Disposed)
            {
                m_Adapter.Destory(m_CameraIntrinsicsHandle);
                m_CameraIntrinsicsHandle = IntPtr.Zero;
                if (disposing)
                {
                    m_Adapter = null;
                }
                m_Disposed = true;
            }
        }

    }
}

