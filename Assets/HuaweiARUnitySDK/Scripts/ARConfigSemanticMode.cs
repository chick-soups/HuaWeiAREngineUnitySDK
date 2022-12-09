using System;
namespace HuaweiARUnitySDK
{
   [Flags]
    public enum ARConfigSemanticMode
    {
        NONE = 0,
        PLANE = 1 << 0,
        TAEGET = 1 << 1
    }
}