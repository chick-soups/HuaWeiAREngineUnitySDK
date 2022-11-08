namespace Common
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using Common;
    using HuaweiARInternal;
    using System.Runtime.InteropServices;
    //using Unity.Collections;

    class BodyMaskHelper:MonoBehaviour
    {
        public static int UseMask = 0;
        public static IntPtr bodyMaskPtr=IntPtr.Zero;
        public static int width = 0;
        public static int height = 0;
        Texture2D texture = null;
        void Start()
        {
            texture = new Texture2D(width, height, TextureFormat.RFloat, false,false);
        }

        void Update()
        {
            Shader.SetGlobalFloat("_UseBodyMask", UseMask);
            if (UseMask > 0)
            {

                texture.Resize(width, height);
                texture.LoadRawTextureData(bodyMaskPtr, width * height * sizeof(float));
                texture.Apply();
                Shader.SetGlobalTexture("_BodyMaskTex", texture);
            }
            UseMask = 0;
        }
    }
}
