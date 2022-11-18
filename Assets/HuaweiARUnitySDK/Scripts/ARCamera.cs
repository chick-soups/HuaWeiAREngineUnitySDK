using System;
using HuaweiARInternal;
using UnityEngine;
namespace HuaweiARUnitySDK
{
    public class ARCamera : IDisposable
    {
        private IntPtr m_Handle;
        private ARCameraAdapter m_Adapter;
        private bool m_Disposed;
        internal ARCamera(IntPtr arCameraHandle, ARCameraAdapter adapter)
        {
            m_Handle = arCameraHandle;
            m_Adapter = adapter;
        }
        ~ARCamera()
        {
            Dispose(false);
        }

        public ARTrackable.TrackingState GetTrackingState()
        {
            return m_Adapter.GetTrackingState(m_Handle);
        }

        public Pose GetPose()
        {
            return m_Adapter.GetPose(m_Handle);
        }

        public Matrix4x4 GetProjectionMatrix(float near, float far)
        {
            return m_Adapter.GetProjectionMatrix(m_Handle, near, far);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        public ARCameraIntrinsics GetImageIntrinsics()
        {
            return m_Adapter.GetImageIntrinsics(m_Handle);

        }

        private void Dispose(bool disposing)
        {
            if (!m_Disposed)
            {
              
                m_Adapter.Release(m_Handle);
                m_Handle = IntPtr.Zero;
                if (disposing)
                {
                    m_Adapter = null;
                }
                m_Disposed=true;
            }

        }


    }

}
