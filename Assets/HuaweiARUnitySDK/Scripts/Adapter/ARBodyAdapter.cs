namespace HuaweiARInternal
{
    using System;
    using System.Runtime.InteropServices;
    using HuaweiARUnitySDK;
    using UnityEngine;

    internal class ARBodyAdapter
    {
        private NDKSession m_ndkSession;

        public ARBodyAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public int GetSkeletonPointCount(IntPtr bodyHandle)
        {
            int pointCount = 0;
            NDKAPI.HwArBody_getSkeletonPointCount(m_ndkSession.SessionHandle, bodyHandle, ref pointCount);
            if (!ValueLegalityChecker.CheckInt("GetSkeletonPointCount", pointCount, 0))
            {
                return 0;
            }
            return pointCount;
        }

        public ARCoordinateSystemType GetCoordinateSystemType(IntPtr bodyHandle)
        {
            int type = (int)ARCoordinateSystemType.COORDINATE_SYSTEM_TYPE_UNKNOWN;
            NDKAPI.HwArBody_getCoordinateSystemType(m_ndkSession.SessionHandle, bodyHandle, ref type);
            if (!ValueLegalityChecker.CheckInt("GetCoordinateSystemType", type,
                AdapterConstants.Enum_CoordSystem_MinIntValue, AdapterConstants.Enum_CoordSystem_MaxIntValue))
            {
                return ARCoordinateSystemType.COORDINATE_SYSTEM_TYPE_2D_IMAGE;
            }
            return (ARCoordinateSystemType)type;
        }

        public int[] GetSkeletonType(IntPtr bodyHandle)
        {
            int skeletonCnt = GetSkeletonPointCount(bodyHandle);
            IntPtr typeHandle = IntPtr.Zero;
            NDKAPI.HwArBody_getSkeletonTypes(m_ndkSession.SessionHandle, bodyHandle, ref typeHandle);
            return MarshalingHelper.GetArrayOfUnmanagedArrayElement<int>(typeHandle, skeletonCnt);
        }


        public bool[] GetSkeletonPointIsExist_2D(IntPtr bodyHandle)
        {
            int skeletonCnt = GetSkeletonPointCount(bodyHandle);
            IntPtr existHandle = IntPtr.Zero;
            bool[] ret = new bool[skeletonCnt];

            NDKAPI.HwArBody_getSkeletonPointIsExist2D(m_ndkSession.SessionHandle, bodyHandle,
                ref existHandle);
            for (int i = 0; i < skeletonCnt; i++)
            {
                ret[i] = MarshalingHelper.GetValueOfUnmanagedArrayElement<int>(existHandle, i) == 1 ? true : false;
            }
            return ret;
        }

        public bool[] GetSkeletonPointIsExist_3D(IntPtr bodyHandle)
        {
            int skeletonCnt = GetSkeletonPointCount(bodyHandle);
            IntPtr existHandle = IntPtr.Zero;
            bool[] ret = new bool[skeletonCnt];
            NDKAPI.HwArBody_getSkeletonPointIsExist3D(m_ndkSession.SessionHandle, bodyHandle,
                ref existHandle);
            for (int i = 0; i < skeletonCnt; i++)
            {
                ret[i] = MarshalingHelper.GetValueOfUnmanagedArrayElement<int>(existHandle, i) == 1 ? true : false;
            }
            return ret;
        }

        public float[] GetSkeletonConfidence(IntPtr bodyHandle)
        {
            int skeletonCnt = GetSkeletonPointCount(bodyHandle);
            IntPtr skeletonConfidenceHandle = IntPtr.Zero;
            NDKAPI.HwArBody_getSkeletonConfidence(m_ndkSession.SessionHandle, bodyHandle, ref skeletonConfidenceHandle);
            return MarshalingHelper.GetArrayOfUnmanagedArrayElement<float>(skeletonConfidenceHandle, skeletonCnt);
        }

        public Vector3[] GetSkeletonPoint2D(IntPtr bodyHandle)
        {
            int skeletonCnt = GetSkeletonPointCount(bodyHandle);
            IntPtr skeleton2DHandle = IntPtr.Zero;
            NDKAPI.HwArBody_getSkeletonPoint2D(m_ndkSession.SessionHandle, bodyHandle,
                ref skeleton2DHandle);
            return MarshalingHelper.GetArrayOfUnmanagedArrayElement<Vector3>(skeleton2DHandle, skeletonCnt);
        }

        public Vector3[] GetSkeletonPoint3D(IntPtr bodyHandle)
        {
            int skeletonCnt = GetSkeletonPointCount(bodyHandle);
            IntPtr skeleton3DHandle = IntPtr.Zero;
            NDKAPI.HwArBody_getSkeletonPoint3D(m_ndkSession.SessionHandle, bodyHandle,
                ref skeleton3DHandle);
            //if native returned camera coordinate, do not negative z,
            //since the camera coordinate in opengl and unity are the same, which is right hand
            if (ARCoordinateSystemType.COORDINATE_SYSTEM_TYPE_3D_CAMERA ==
                GetCoordinateSystemType(bodyHandle))
            {
                return MarshalingHelper.GetArrayOfUnmanagedArrayElement<Vector3>(skeleton3DHandle, skeletonCnt);
            }
            //otherwise negative z,
            //since the world and model coordinate in opengl and unity are converse
            //and z value in image coordinate is useless
            else
            {
                Vector3[] ret = new Vector3[skeletonCnt];
                for (int i = 0; i < skeletonCnt; i++)
                {
                    Vector3 vector = new Vector3();
                    vector.x = MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(skeleton3DHandle, 3 * i);
                    vector.y = MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(skeleton3DHandle, 3 * i + 1);
                    vector.z = -MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(skeleton3DHandle, 3 * i + 2);
                    ret[i] = vector;
                }
                return ret;
            }
            
        }


        public int[] GetSkeletonConnection(IntPtr bodyHandle)
        {
            int connectionArraySize = 0;
            NDKAPI.HwArBody_getSkeletonConnectionSize(m_ndkSession.SessionHandle, bodyHandle, ref connectionArraySize);
            if (!ValueLegalityChecker.CheckInt("GetSkeletonConnection", connectionArraySize, 0))
            {
                return new int[0];
            }
            IntPtr connectionHandle = IntPtr.Zero;
            NDKAPI.HwArBody_getSkeletonConnection(m_ndkSession.SessionHandle, bodyHandle,
                ref connectionHandle);
            return MarshalingHelper.GetArrayOfUnmanagedArrayElement<int>(connectionHandle, connectionArraySize);
        }

        public int GetBodyAction(IntPtr bodyHandle)
        {
            int action = 0;
            NDKAPI.HwArBody_getBodyAction(m_ndkSession.SessionHandle, bodyHandle, ref action);
            return action;
        }

        public IntPtr GetMaskConfidence(IntPtr bodyHandle)
        {
            IntPtr confidence = IntPtr.Zero;
            NDKAPI.HwArBody_getMaskConfidence(m_ndkSession.SessionHandle, bodyHandle, ref confidence);
            return confidence;
        }

        public IntPtr GetMaskDepth(IntPtr bodyHandle)
        {
            IntPtr depth = IntPtr.Zero;
            NDKAPI.HwArBody_getMaskDepth(m_ndkSession.SessionHandle, bodyHandle, ref depth);
            return depth;
        }

        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArBody_getSkeletonPointCount(IntPtr sessionHandle, IntPtr bodyHandle,
                ref int pointCount);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArBody_getCoordinateSystemType(IntPtr sessionHandle, IntPtr bodyHandle,
                ref int systemType);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArBody_getSkeletonTypes(IntPtr sessionHandle, IntPtr bodyHandle,
                ref IntPtr skeletonType);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArBody_getSkeletonPointIsExist2D(IntPtr sessionHandle, IntPtr bodyHandle,
                ref IntPtr outSkeletonPintIsExistHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArBody_getSkeletonPointIsExist3D(IntPtr sessionHandle, IntPtr bodyHandle,
                ref IntPtr outSkeletonPintIsExistHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArBody_getSkeletonPoint2D(IntPtr sessionHandle, IntPtr bodyHandle,
                ref IntPtr outPointHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArBody_getSkeletonPoint3D(IntPtr sessionHandle, IntPtr bodyHandle,
                ref IntPtr outPointHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArBody_getSkeletonConfidence(IntPtr sessionHandle, IntPtr bodyHandle,
                ref IntPtr outSkeletonConfidence);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArBody_getSkeletonConnectionSize(IntPtr sessionHandle, IntPtr bodyHandle,
                ref int connectionCnt);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArBody_getSkeletonConnection(IntPtr sessionHandle, IntPtr bodyHandle,
                ref IntPtr outConnectionHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArBody_getBodyAction(IntPtr sessionHandle, IntPtr bodyHandle,
                ref int bodyAction);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArBody_getMaskConfidence(IntPtr sessionHandle, IntPtr bodyHandle,
                ref IntPtr outMaskConfidenceHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArBody_getMaskDepth(IntPtr sessionHandle, IntPtr bodyHandle,
                ref IntPtr outMaskDepthHandle);

        }

    }
}
