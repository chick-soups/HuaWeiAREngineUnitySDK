namespace HuaweiARInternal
{
    using System.Runtime.InteropServices;
    using UnityEngine;
    using System;
    internal class ARAugmentedImageDatabaseAdapter
    {
        private NDKSession m_ndkSession;

        public ARAugmentedImageDatabaseAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public IntPtr CreateAugImgDatabaseFromBytes(byte[] databaseBytes)
        {
            IntPtr databaseHandle = IntPtr.Zero;
            var bytesHandle = GCHandle.Alloc(databaseBytes, GCHandleType.Pinned);
            NDKARStatus status = NDKAPI.HwArAugmentedImageDatabase_deserialize(m_ndkSession.SessionHandle,
                bytesHandle.AddrOfPinnedObject(), databaseBytes.Length, ref databaseHandle);
            ARDebug.LogInfo("native AddImageWithPhysicalSize end with status={0}", status);
            ARExceptionAdapter.ExtractException(status);
            bytesHandle.Free();
            return databaseHandle;
        }

        public void Destroy(IntPtr dataBaseHandle)
        {
            ARDebug.LogInfo("native destroy session begin, handle =0x{0}", dataBaseHandle.ToString("x8"));
            NDKAPI.HwArAugmentedImageDatabase_destroy(dataBaseHandle);
            ARDebug.LogInfo("native destroy session end");
        }

        /*public IntPtr Create()
        {
            IntPtr databaseHandle = IntPtr.Zero;
            NDKAPI.HwArAugmentedImageDatabase_create(m_ndkSession.SessionHandle, ref databaseHandle);
            return databaseHandle;
        }

        public int AddImage(IntPtr dataBaseHandle, string name, string grayscale, int width, int height, int stride)
        {
            int outIndex = 0;
            NDKARStatus status = NDKAPI.HwArAugmentedImageDatabase_addImage(m_ndkSession.SessionHandle, dataBaseHandle,
                name, grayscale, width, height, stride, ref outIndex);
            ARDebug.LogInfo("native AddImage end with status={0}", status);
            ARExceptionAdapter.ExtractException(status);
            return outIndex;
        }

        public int AddImageWithPhysicalSize(IntPtr dataBaseHandle, string name, string grayscale, int width, int height, int stride, float meters)
        {
            int outIndex = 0;
            NDKARStatus status = NDKAPI.HwArAugmentedImageDatabase_addImageWithPhysicalSize(m_ndkSession.SessionHandle, dataBaseHandle,
                name, grayscale, width, height, stride, meters,ref outIndex);
            ARDebug.LogInfo("native AddImageWithPhysicalSize end with status={0}", status);
            ARExceptionAdapter.ExtractException(status);
            return outIndex;
        }

        public void Serialize(IntPtr dataBaseHandle, ref string rawByte, ref UInt64 outByteSize)
        {
            NDKAPI.HwArAugmentedImageDatabase_serialize(m_ndkSession.SessionHandle, dataBaseHandle, ref rawByte, ref outByteSize);
        }

        public int GetImagesNum(IntPtr dataBaseHandle)
        {
            int outImgNum = 0;
            NDKAPI.HwArAugmentedImageDatabase_getNumImages(m_ndkSession.SessionHandle, dataBaseHandle, ref outImgNum);
            return outImgNum;
        }*/

        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAugmentedImageDatabase_destroy(IntPtr databaseHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArAugmentedImageDatabase_deserialize(IntPtr sessionHandle, IntPtr rawBytes, Int64 rawBytesSize, ref IntPtr databaseHandle);
            /*[DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAugmentedImageDatabase_create(IntPtr sessionHandle, ref IntPtr databaseHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArAugmentedImageDatabase_addImage(IntPtr sessionHandle, IntPtr databaseHandle, string name, string grayscale,
                int width, int height, int stride, ref int outindex);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArAugmentedImageDatabase_addImageWithPhysicalSize(IntPtr sessionHandle, IntPtr databaseHandle, string name, string grayscale,
                int width, int height, int stride, float meters, ref int outindex);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAugmentedImageDatabase_serialize(IntPtr sessionHandle, IntPtr databaseHandle, ref string rawByte, ref UInt64 outByteSize);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAugmentedImageDatabase_getNumImages(IntPtr sessionHandle, IntPtr databaseHandle, ref int outImgNum);*/
        }
    }
}
