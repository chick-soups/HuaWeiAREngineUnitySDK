

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace HuaweiARInternal
{
    class ARSceneMeshAdapter
    {
        private NDKSession m_ndkSession;

        public ARSceneMeshAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public void Release(IntPtr geometryHandle)
        {
            NDKAPI.HwArSceneMesh_release(geometryHandle);
        }

        public Vector3[] GetVertices(IntPtr sceneMeshHandle)
        {
            int count = 0;
            NDKAPI.HwArSceneMesh_getVerticesSize(m_ndkSession.SessionHandle, sceneMeshHandle, ref count);
            if (!ValueLegalityChecker.CheckInt("GetVertices", count, 0))
            {
                return new Vector3[0];
            }

            IntPtr verticesHandle = IntPtr.Zero;
            NDKAPI.HwArSceneMesh_acquireVertices(m_ndkSession.SessionHandle, sceneMeshHandle, ref verticesHandle);
            Vector3[] ret = new Vector3[count / 3];
            for (int i = 0; i < count / 3; i++)
            {
                ret[i] = new Vector3();
                ret[i].x = MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(verticesHandle, 3 * i);
                ret[i].y = MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(verticesHandle, 3 * i + 1);
                ret[i].z = -(MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(verticesHandle, 3 * i + 2));
            }
            return ret;
        }

        public Vector3[] GetVertexNormals(IntPtr sceneMeshHandle)
        {
            int count = 0;
            NDKAPI.HwArSceneMesh_getVerticesSize(m_ndkSession.SessionHandle, sceneMeshHandle, ref count);
            if (!ValueLegalityChecker.CheckInt("GetVertices", count, 0))
            {
                return new Vector3[0];
            }

            IntPtr vertexNormalsHandle = IntPtr.Zero;
            NDKAPI.HwArSceneMesh_acquireVertexNormals(m_ndkSession.SessionHandle, sceneMeshHandle, ref vertexNormalsHandle);
            Vector3[] ret = new Vector3[count / 3];
            for (int i = 0; i < count / 3; i++)
            {
                ret[i] = new Vector3();
                ret[i].x = MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(vertexNormalsHandle, 3 * i);
                ret[i].y = MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(vertexNormalsHandle, 3 * i + 1);
                ret[i].z = MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(vertexNormalsHandle, 3 * i + 2);
            }
            return ret;
        }

        public int[] GetTriangleIndex(IntPtr sceneMeshHandle)
        {
            int count = 0;
            NDKAPI.HwArSceneMesh_getTriangleIndicesSize(m_ndkSession.SessionHandle, sceneMeshHandle, ref count);
            if (!ValueLegalityChecker.CheckInt("GetTriangleIndex", count, 0))
            {
                return new int[0];
            }
            IntPtr triangleIndexHandle = IntPtr.Zero;
            NDKAPI.HwArSceneMesh_acquireTriangleIndices(m_ndkSession.SessionHandle, sceneMeshHandle,
                ref triangleIndexHandle);
            int[] ret = new int[count];
            for (int i = 0; i < count; i += 3)
            {
                ret[i] = MarshalingHelper.GetValueOfUnmanagedArrayElement<int>(triangleIndexHandle, i + 2);
                ret[i + 1] = MarshalingHelper.GetValueOfUnmanagedArrayElement<int>(triangleIndexHandle, i + 1);
                ret[i + 2] = MarshalingHelper.GetValueOfUnmanagedArrayElement<int>(triangleIndexHandle, i);
            }
            return ret;
        }

        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArSceneMesh_release(IntPtr sceneMesh);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArSceneMesh_getVerticesSize(IntPtr session, IntPtr sceneMesh, ref int size);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArSceneMesh_acquireVertices(IntPtr session, IntPtr sceneMesh, ref IntPtr data);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArSceneMesh_acquireVertexNormals(IntPtr session, IntPtr sceneMesh, ref IntPtr data);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArSceneMesh_getTriangleIndicesSize(IntPtr session, IntPtr sceneMesh, ref int size);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArSceneMesh_acquireTriangleIndices(IntPtr session, IntPtr sceneMesh, ref IntPtr data);
        }
    }
}
