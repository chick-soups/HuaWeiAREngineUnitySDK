///@cond ContainSceneMeshAR
namespace HuaweiARUnitySDK
{
    using System;
    using HuaweiARInternal;
    using UnityEngine;

    public class ARSceneMesh
    {
        private IntPtr m_sceneMeshHandle;
        private NDKSession m_session;

        internal ARSceneMesh(IntPtr handle, NDKSession session)
        {
            m_sceneMeshHandle = handle;
            m_session = session;
        }
        ~ARSceneMesh()
        {
            m_session.SceneMeshAdapter.Release(m_sceneMeshHandle);
        }
        /**
         * \if english
         * @brief Get vertex coordinates.
         * @return Vertex coordinate Vector array.
         * \else
         * @brief 获取顶点坐标。
         * @return 顶点坐标Vector数组。
         * \endif
         */
        public Vector3[] Vertices
        {
            get
            {
                return m_session.SceneMeshAdapter.GetVertices(m_sceneMeshHandle);
            }
        }
        /**
         * \if english
         * @brief Get vertex normal vector.
         * @return Vertex normal vector Vector array.
         * \else
         * @brief 获取顶点法向量。
         * @return 顶点法向量Vector数组。
         * \endif
         */
        public Vector3[] VertexNormals
        {
            get
            {
                return m_session.SceneMeshAdapter.GetVertexNormals(m_sceneMeshHandle);
            }
        }
        /**
         * \if english
         * @brief Get the triangle vertex subscript.
         * @return Triangle subscript array.
         * \else
         * @brief 获取三角形顶点下标。
         * @return 三角形顶点下标数组。
         * \endif
         */
        public int[] TriangleIndices
        {
            get
            {
                return m_session.SceneMeshAdapter.GetTriangleIndex(m_sceneMeshHandle);
            }
        }
    }
}
///@endcond