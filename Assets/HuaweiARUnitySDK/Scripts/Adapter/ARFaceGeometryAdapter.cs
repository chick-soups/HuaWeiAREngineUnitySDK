namespace HuaweiARInternal
{
    using System.Runtime.InteropServices;
    using UnityEngine;
    using System;
    using HuaweiARUnitySDK;

    internal class ARFaceGeometryAdapter
    {
        private NDKSession m_ndkSession;

        public ARFaceGeometryAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public void Release(IntPtr geometryHandle)
        {
            NDKAPI.HwArFaceGeometry_release(geometryHandle);
        }

        public int GetTriangleCount(IntPtr geometryHandle)
        {
            int count = 0;
            NDKAPI.HwArFaceGeometry_getTriangleCount(m_ndkSession.SessionHandle, geometryHandle, ref count);
            if (!ValueLegalityChecker.CheckInt("GetTriangleCount", count, 0))
            {
                return 0;
            }
            return count;
        }
        public Vector3[] GetVertices(IntPtr geometryHandle)
        {
            int count = 0;
            NDKAPI.HwArFaceGeometry_getVerticesSize(m_ndkSession.SessionHandle, geometryHandle, ref count);
            if (!ValueLegalityChecker.CheckInt("GetVertices", count, 0))
            {
                return new Vector3[0];
            }

            IntPtr verticesHandle = IntPtr.Zero;
            NDKAPI.HwArFaceGeometry_acquireVertices(m_ndkSession.SessionHandle, geometryHandle, ref verticesHandle);
            Vector3[] ret = new Vector3[count / 3];
            for(int i = 0; i < count / 3; i++)
            {
                ret[i] = new Vector3();
                ret[i].x = MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(verticesHandle, 3 * i);
                ret[i].y = MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(verticesHandle, 3 * i + 1);
                ret[i].z = -(MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(verticesHandle, 3 * i + 2));
            }
            return ret;
        }

        public Vector2[] GetTexCoord(IntPtr geometryHandle)
        {
            int count = 0;
            NDKAPI.HwArFaceGeometry_getTexCoordSize(m_ndkSession.SessionHandle, geometryHandle, ref count);
            if (!ValueLegalityChecker.CheckInt("GetTexCoord", count, 0))
            {
                return new Vector2[0];
            }
            IntPtr texcoordHandle = IntPtr.Zero;
            NDKAPI.HwArFaceGeometry_acquireTexCoord(m_ndkSession.SessionHandle, geometryHandle, ref texcoordHandle);
            return MarshalingHelper.GetArrayOfUnmanagedArrayElement<Vector2>(texcoordHandle, count / 2);
        }
        public int[] GetTriangleIndex(IntPtr geometryHandle)
        {
            int count = 0;
            NDKAPI.HwArFaceGeometry_getTriangleIndicesSize(m_ndkSession.SessionHandle, geometryHandle, ref count);
            if (!ValueLegalityChecker.CheckInt("GetTriangleIndex", count, 0))
            {
                return new int[0];
            }
            IntPtr triangleIndexHandle = IntPtr.Zero;
            NDKAPI.HwArFaceGeometry_acquireTriangleIndices(m_ndkSession.SessionHandle, geometryHandle,
                ref triangleIndexHandle);
            //int[] ret = new int[count];
            //for (int i = 0; i < count; i++)
            //{
            //    ret[i] = MarshalingHelper.GetValueOfUnmanagedArrayElement<int>(triangleIndexHandle, i) / 3;
            //}

            int[] ret = new int[count];
            for (int i = 0; i < count; i += 3)
            {
                ret[i] = MarshalingHelper.GetValueOfUnmanagedArrayElement<int>(triangleIndexHandle, i + 2);
                ret[i + 1] = MarshalingHelper.GetValueOfUnmanagedArrayElement<int>(triangleIndexHandle, i + 1);
                ret[i + 2] = MarshalingHelper.GetValueOfUnmanagedArrayElement<int>(triangleIndexHandle, i);
            }
            return ret;
        }

        public ARFaceGeometry.Label[] GetTriangleLabels(IntPtr geometryHandle)
        {
            int count = 0;
            NDKAPI.HwArFaceGeometry_getTriangleLabelsSize(m_ndkSession.SessionHandle, geometryHandle, ref count);
            if (!ValueLegalityChecker.CheckInt("GetTriangleLabels: count", count, 0))
            {
                return new ARFaceGeometry.Label[0];
            }
            ARFaceGeometry.Label[] ret = new ARFaceGeometry.Label[count];
            IntPtr triangleLabelHandle = IntPtr.Zero;
            NDKAPI.HwArFaceGeometry_acquireTriangleLabels(m_ndkSession.SessionHandle, geometryHandle,
                ref triangleLabelHandle);
            for (int i = 0; i < count; i++)
            {
                int val = MarshalingHelper.GetValueOfUnmanagedArrayElement<int>(triangleLabelHandle, i);
                if (!ValueLegalityChecker.CheckInt("GetTriangleLabels: value", val,
                    AdapterConstants.Enum_FaceLabel_MinIntValue,
                    AdapterConstants.Enum_FaceLabel_MaxIntValue - 1))
                {
                    ret[i] = ARFaceGeometry.Label.Label_Non_Face;
                }
                else
                {
                    ret[i] = (ARFaceGeometry.Label)val;
                }
            }
            return ret;
        }

        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFaceGeometry_release(IntPtr geometry);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFaceGeometry_getTriangleCount(IntPtr sessionHandle,
                                       IntPtr geometryHandle, ref int count);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFaceGeometry_getVerticesSize(IntPtr sessionHandle,
                                      IntPtr geometryHandle, ref int count);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFaceGeometry_acquireVertices(IntPtr sessionHandle,
                                      IntPtr geometryHandle, ref IntPtr data);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFaceGeometry_getTexCoordSize(IntPtr sessionHandle,
                                      IntPtr geometryHandle, ref int count);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFaceGeometry_acquireTexCoord(IntPtr sessionHandle,
                                      IntPtr geometryHandle, ref IntPtr data);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFaceGeometry_getTriangleIndicesSize(IntPtr sessionHandle,
                                     IntPtr geometryHandle, ref int count);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFaceGeometry_acquireTriangleIndices(IntPtr sessionHandle,
                                     IntPtr geometryHandle, ref IntPtr data);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFaceGeometry_getTriangleLabelsSize(IntPtr sessionHandle,
                                     IntPtr geometryHandle, ref int count);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArFaceGeometry_acquireTriangleLabels(IntPtr sessionHandle,
                                                 IntPtr geometryHandle, ref IntPtr data);
        }
    }
}
