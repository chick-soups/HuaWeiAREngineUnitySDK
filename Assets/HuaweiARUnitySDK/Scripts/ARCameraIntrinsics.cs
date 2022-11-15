namespace HuaweiARUnitySDK
{
    using System;
    using HuaweiARInternal;
    using UnityEngine;
    using System.Collections.Generic;

    public class ARCameraIntrinsics
    {
        internal IntPtr m_CameraIntrinsicsHandle = IntPtr.Zero;
        internal ARCameraIntrinsicsAdapter m_Adapter;

        internal ARCameraIntrinsics()
        {
        }
        internal ARCameraIntrinsics(IntPtr cameraIntrinsicsHandle,ARCameraIntrinsicsAdapter adapter)
        {
            m_CameraIntrinsicsHandle = cameraIntrinsicsHandle;
            m_Adapter=adapter;
        }
        ~ARCameraIntrinsics()
        {
            destory();
        }

        private void create()
        {
            m_CameraIntrinsicsHandle = m_Adapter.Create();
        }

        private void destory()
        {
            m_Adapter.Destory(m_CameraIntrinsicsHandle);
            m_CameraIntrinsicsHandle = IntPtr.Zero;
            m_Adapter=null;
        }


        public Vector2Int GetImageDimensions()
        {
            return m_Adapter.GetImageDimensions(m_CameraIntrinsicsHandle);
        }

        public Vector2 GetTextureDimensions()
        {
            return m_Adapter.GetPrincipalPoint(m_CameraIntrinsicsHandle);
        }

        public Vector2 GetFocalLength()
        {
            return m_Adapter.GeFocalLength(m_CameraIntrinsicsHandle);
        }

    }
}

