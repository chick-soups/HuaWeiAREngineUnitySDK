
namespace HuaweiARInternal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal class MarshalingHelper
    {
        
        public static T[] GetArrayOfUnmanagedArrayElement<T>(IntPtr arrayPtr, int arrayLength) where T: struct
        {
            if (IntPtr.Zero == arrayPtr || arrayLength == 0)
            {
                return new T[0];
            }
            T[] ret = new T[arrayLength];
            for(int i = 0; i < arrayLength; i++)
            {
                ret[i] = GetValueOfUnmanagedArrayElement<T>(arrayPtr, i);
            }
            return ret;
        }

        public static void AppendUnManagedArray2List<T>(IntPtr arrayPtr, int arrayLength, List<T> list) where T:struct
        {
            if(IntPtr.Zero==arrayPtr || null == list)
            {
                return; 
            }
            for(int i = 0; i < arrayLength; i++)
            {
                IntPtr ptr = new IntPtr(arrayPtr.ToInt64() + (Marshal.SizeOf(typeof(T)) * i));
                list.Add((T)Marshal.PtrToStructure(ptr, typeof(T)));
            }
        }
        public static T GetValueOfUnmanagedArrayElement<T>(IntPtr arrayPtr, int arrayIndex) where T : struct
        {
            IntPtr ptr = new IntPtr(arrayPtr.ToInt64() + (Marshal.SizeOf(typeof(T)) * arrayIndex));
            T value = (T)Marshal.PtrToStructure(ptr, typeof(T));
            return value;
        }
    }
}
