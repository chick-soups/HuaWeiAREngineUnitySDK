namespace HuaweiARUnitySDK
{
    using System;
    using System.Collections.Generic;
    internal class IntPtrComparer:IEqualityComparer<IntPtr>
    {
        public bool Equals(IntPtr intPtr1, IntPtr intPtr2)
        {
            return intPtr1 == intPtr2;
        }

        public int GetHashCode(IntPtr intPtr)
        {
            return intPtr.GetHashCode();
        }
    }
}
