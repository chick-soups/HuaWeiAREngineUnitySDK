namespace HuaweiARInternal
{
    using System.Runtime.InteropServices;
    using System;
    using HuaweiARUnitySDK;
    using System.Collections.Generic;

    internal class ARFaceBlendShapeAdapter
    {
        private NDKSession m_ndkSession;
        public ARFaceBlendShapeAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }
        public void Release(IntPtr blendShapesHandle)
        {
            NDKAPI.HwArFaceBlendShapes_release(blendShapesHandle);
        }
        public Dictionary<ARFace.BlendShapeLocation, float> GetBlendShapeData(IntPtr blendShapesHandle)
        {
            IntPtr dataHandle = IntPtr.Zero;
            IntPtr shapeTypeHandle = IntPtr.Zero;
            int dataSize = 0;
            Dictionary<ARFace.BlendShapeLocation, float> ret = new Dictionary<ARFace.BlendShapeLocation, float>();

            NDKAPI.HwArFaceBlendShapes_getCount(m_ndkSession.SessionHandle, blendShapesHandle, ref dataSize);

            if (dataSize < 0 || dataSize > AdapterConstants.Enum_FaceBlendShapeLocation_MaxIntValue)
            {
                ARDebug.LogWarning("HwArFaceBlendShapes_getCount return value:{0}, while the legal max value is {1}",
                        dataSize, AdapterConstants.Enum_FaceBlendShapeLocation_MaxIntValue);
                return ret;
            }

            NDKAPI.HwArFaceBlendShapes_acquireTypes(m_ndkSession.SessionHandle, blendShapesHandle, ref shapeTypeHandle);
            NDKAPI.HwArFaceBlendShapes_acquireData(m_ndkSession.SessionHandle, blendShapesHandle, ref dataHandle);

            for (int i = 0; i < dataSize; i++)
            {
                int location = MarshalingHelper.GetValueOfUnmanagedArrayElement<int>(shapeTypeHandle, i);
                if (!ValueLegalityChecker.CheckInt("GetBlendShapeData", location,
                    AdapterConstants.Enum_FaceBlendShapeLocation_MinIntValue,
                    AdapterConstants.Enum_FaceBlendShapeLocation_MaxIntValue - 1))
                {
                    continue;
                }

                float val = MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(dataHandle, i);

                ret.Add((ARFace.BlendShapeLocation)location, val);
            }
            return ret;
        }


        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFaceBlendShapes_release(IntPtr blendshapesHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFaceBlendShapes_acquireData(IntPtr sessionHandle,
                                     IntPtr blendshapesHandle, ref IntPtr dataHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFaceBlendShapes_getCount(IntPtr sessionHandle,
                                  IntPtr blendshapesHandle, ref int count);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFaceBlendShapes_acquireTypes(IntPtr sessionHandle,
                                      IntPtr blendshapesHandle, ref IntPtr typesHandle);
        }
    }
}
