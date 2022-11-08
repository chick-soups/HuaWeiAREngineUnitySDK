namespace HuaweiARInternal
{
    using System.Runtime.InteropServices;
    using UnityEngine;
    using System;
    using HuaweiARUnitySDK;

    internal class ARHandAdapter
    {
        private NDKSession m_ndkSession;

        public ARHandAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public ARCoordinateSystemType GetGestureCoordinateSystemType(IntPtr handHandle)
        {
            int ret = 0;
            NDKAPI.HwArHand_getGestureCoordinateSystem(m_ndkSession.SessionHandle, handHandle, ref ret);
            if (!ValueLegalityChecker.CheckInt("GetGestureCoordinateSystemType", ret, 
                AdapterConstants.Enum_CoordSystem_MinIntValue, AdapterConstants.Enum_CoordSystem_MaxIntValue))
            {
                return ARCoordinateSystemType.COORDINATE_SYSTEM_TYPE_2D_IMAGE;
            }
            return (ARCoordinateSystemType)ret;
        }

        public int GetGustureType(IntPtr handHandle)
        {
            int ret = 0;
            NDKAPI.HwArHand_getGestureType(m_ndkSession.SessionHandle, handHandle, ref ret);
            //-1 is the min value of gusture type, and it means unknown type
            if (!ValueLegalityChecker.CheckInt("getGestureType", ret, -1))
            {
                return -1;
            }
            return ret;
        }

        public ARHand.HandType GetHandType(IntPtr handHandle)
        {
            int ret = 0;
            NDKAPI.HwArHand_getHandType(m_ndkSession.SessionHandle, handHandle, ref ret);
            if (!ValueLegalityChecker.CheckInt("GetHandType", ret, 
                AdapterConstants.Enum_HandType_MinIntValue, AdapterConstants.Enum_HandType_MaxIntValue))
            {
                return ARHand.HandType.UNKNOWN;
            }
            return (ARHand.HandType)ret;
        }

        public Vector3[] GetHandBox(IntPtr handHandle)
        {
            int count = 2;
            IntPtr data = IntPtr.Zero;
            NDKAPI.HwArHand_getGestureHandBox(m_ndkSession.SessionHandle, handHandle, ref data);
            Vector3[] ret = new Vector3[count];
            for(int i = 0; i < count; i++)
            {
                Vector3 vector = new Vector3();
                vector.x = MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(data, 3*i);
                vector.y = MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(data, 3*i+1);
                vector.z = -MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(data, 3*i+2);
                ret[i] = vector;
            }
            return ret;
        }

        public Vector3 GetGestureCenter(IntPtr handHandle)
        {
            IntPtr data = IntPtr.Zero;
            NDKAPI.HwArHand_getGestureCenter(m_ndkSession.SessionHandle, handHandle, ref data);
            return MarshalingHelper.GetValueOfUnmanagedArrayElement<Vector3>(data, 0);
        }

        public int[] GetGestureAction(IntPtr handHandle)
        {
            int actionCount = 0;
            NDKAPI.HwArHand_getGestureActionSize(m_ndkSession.SessionHandle, handHandle, ref actionCount);
            IntPtr actionDataHandle = IntPtr.Zero;
            NDKAPI.HwArHand_getGestureAction(m_ndkSession.SessionHandle, handHandle, ref actionDataHandle);
            return MarshalingHelper.GetArrayOfUnmanagedArrayElement<int>(actionDataHandle, actionCount);
        }

        public Vector4 GetGestureOrientation(IntPtr handHandle)
        {
            
            IntPtr data = IntPtr.Zero;
            NDKAPI.HwArHand_getGestureOrientation(m_ndkSession.SessionHandle, handHandle, ref data);
            return MarshalingHelper.GetValueOfUnmanagedArrayElement<Vector4>(data, 0);
        }


        public ARCoordinateSystemType GetSkeletonCoordinateSystemType(IntPtr handHandle)
        {
            int type = 0;
            NDKAPI.HwArHand_getSkeletonCoordinateSystem(m_ndkSession.SessionHandle, handHandle, ref type);
            if (!ValueLegalityChecker.CheckInt("getSkeletonCoordinateSystem", type, 
                AdapterConstants.Enum_CoordSystem_MinIntValue, AdapterConstants.Enum_CoordSystem_MaxIntValue))
            {
                return ARCoordinateSystemType.COORDINATE_SYSTEM_TYPE_3D_CAMERA;
            }
            return (ARCoordinateSystemType)type;
        }

        public int GetHandSkeletonCount(IntPtr handHandle)
        {
            int count=0;
            NDKAPI.HwArHand_getHandSkeletonCount(m_ndkSession.SessionHandle, handHandle, ref count);
            if (!ValueLegalityChecker.CheckInt("getHandSkeletonCount", count, 0))
            {
                return 0;
            }
            return count;
        }

        public Vector3[] GetHandSkeletonData(IntPtr handHandle)
        {
            int skeletonCount = GetHandSkeletonCount(handHandle);
            IntPtr skeletonHandle = IntPtr.Zero;
            NDKAPI.HwArHand_getHandSkeletonArray(m_ndkSession.SessionHandle, handHandle, ref skeletonHandle);
            //if native returned camera coordinate, do not negative z,
            //since the camera coordinate in opengl and unity are the same, which is right hand
            if (ARCoordinateSystemType.COORDINATE_SYSTEM_TYPE_3D_CAMERA==
                GetSkeletonCoordinateSystemType(handHandle) )
            {
                return MarshalingHelper.GetArrayOfUnmanagedArrayElement<Vector3>(skeletonHandle, skeletonCount);
            }
            //otherwise negative z,
            //since the world and model coordinate in opengl and unity are converse
            //and z value in image coordinate is useless
            else
            {
                Vector3[] ret = new Vector3[skeletonCount];
                for (int i = 0; i < skeletonCount; i++)
                {
                    Vector3 vector = new Vector3();
                    vector.x = MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(skeletonHandle, 3 * i);
                    vector.y = MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(skeletonHandle, 3 * i + 1);
                    vector.z = -MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(skeletonHandle, 3 * i + 2);
                    ret[i] = vector;
                }
                return ret;
            }
        }

        public int[] GetHandSkeletonType(IntPtr handHandle)
        {
            int skeletonCount = GetHandSkeletonCount(handHandle);
            IntPtr typeHandle = IntPtr.Zero;
            NDKAPI.HwArHand_getHandSkeletonType(m_ndkSession.SessionHandle, handHandle, ref typeHandle);
            return MarshalingHelper.GetArrayOfUnmanagedArrayElement<int>(typeHandle, skeletonCount);
        }

        public int[] GetSkeletonConnection(IntPtr handHandle)
        {
            int connectionArraySize = 0;
            IntPtr connectionHandle = IntPtr.Zero;

            NDKAPI.HwArHand_getHandSkeletonConnectionSize(m_ndkSession.SessionHandle, handHandle, ref connectionArraySize);
            if (!ValueLegalityChecker.CheckInt("GetSkeletonConnection", connectionArraySize, 0))
            {
                return new int[0];
            }
            NDKAPI.HwArHand_getHandSkeletonConnection(m_ndkSession.SessionHandle, handHandle, ref connectionHandle);
            return MarshalingHelper.GetArrayOfUnmanagedArrayElement<int>(connectionHandle, connectionArraySize);
        }


        private struct NDKAPI
        {
            //Guesture 
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHand_getGestureCoordinateSystem(IntPtr sessionHandle,
                                         IntPtr handHandle, ref int gestureCoordinateSyste);

            //void HwArHand_getHandId(const HwArSession* session,
            //                        const HwArHand* hand, int32_t* handId);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHand_getGestureType(IntPtr sessionHandle,
                                         IntPtr handHandle, ref int gestureType);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHand_getHandType(IntPtr sessionHandle,
                                         IntPtr handHandle, ref int handType);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHand_getGestureHandBox(IntPtr sessionHandle,
                                         IntPtr handHandle, ref IntPtr data);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHand_getGestureCenter(IntPtr sessionHandle,
                                         IntPtr handHandle, ref IntPtr data);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHand_getGestureActionSize(IntPtr sessionHandle,
                                         IntPtr handHandle, ref int data);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHand_getGestureAction(IntPtr sessionHandle,
                                         IntPtr handHandle, ref IntPtr data);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHand_getGestureOrientation(IntPtr sessionHandle,
                                         IntPtr handHandle, ref IntPtr data);

            //Skeleton
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHand_getSkeletonCoordinateSystem(IntPtr sessionHandle,
                                         IntPtr handHandle, ref int skeletonCoordinateSystem);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHand_getHandSkeletonType(IntPtr sessionHandle,
                                         IntPtr handHandle, ref IntPtr handType);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHand_getHandSkeletonCount(IntPtr sessionHandle,
                                         IntPtr handHandle, ref int data);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHand_getHandSkeletonArray(IntPtr sessionHandle,
                                         IntPtr handHandle, ref IntPtr data);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHand_getHandSkeletonConnectionSize(IntPtr sessionHandle,
                                         IntPtr handHandle, ref int count);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArHand_getHandSkeletonConnection(IntPtr sessionHandle,
                                         IntPtr handHandle, ref IntPtr data);

            
        }
    }
}
