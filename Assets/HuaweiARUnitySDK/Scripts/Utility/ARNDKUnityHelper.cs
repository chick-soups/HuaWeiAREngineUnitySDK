namespace HuaweiARInternal
{
    using System;
    using UnityEngine;
    using System.Runtime.InteropServices;
    using HuaweiARUnitySDK;
    internal class ARUnityHelper
    {
        private IntPtr m_activityHandle;
        private int m_textureId = -1;
        private static ARUnityHelper s_unityHelper;

        private  ARUnityHelper()
        {
            m_activityHandle = IntPtr.Zero;
        }
        
        public static ARUnityHelper Instance
        {
            get
            {
                if (null == s_unityHelper)
                {
                    s_unityHelper= new ARUnityHelper();
                }
                return s_unityHelper;
            }
        }


        public IntPtr GetJEnv()
        {
            return NDKAPI.GetJEnv();
        }

        public IntPtr GetActivityHandle()
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            m_activityHandle = activity.GetRawObject();
            return m_activityHandle;
        }

        public int GetTextureId()
        {
            if (m_textureId == -1)
            {
                m_textureId = NDKAPI.getTextureId();
            }
            return m_textureId;
        }

        private struct NDKAPI
        {
            
           [DllImport(AdapterConstants.UnityPluginApi)]
            public static extern int getTextureId();

            [DllImport(AdapterConstants.UnityPluginApi)]
            public static extern IntPtr GetJEnv();
        }
    }
}
