namespace Common
{
    using System.Collections.Generic;
    using HuaweiARUnitySDK;
    using UnityEngine;
    using HuaweiARInternal;
    public class PointcloudVisualizer : MonoBehaviour
    {
        private const int k_maxPointCount = 2000;

        private Mesh m_mesh;

        private List<Vector3> m_points = new List<Vector3>();
        private List<int> m_pointIndex = new List<int>();
        public void Start()
        {
            m_mesh = GetComponent<MeshFilter>().mesh;
            m_mesh.Clear();
        }

        public void Update()
        {
            // Do not update if huaweiAR is not tracking.
            if (ARFrame.GetTrackingState() != ARTrackable.TrackingState.TRACKING)
            {
                m_mesh.Clear();
                return;
            }

            // Fill in the data to draw the point cloud.
            ARPointCloud pointCloud = ARFrame.AcquirePointCloud();
            pointCloud.GetPoints(m_points);
            pointCloud.Release();
            if (m_points.Count > 0)
            {
                // Update the mesh indicies array.
                m_pointIndex.Clear();
                for (int i = 0; i <Mathf.Min( m_points.Count,k_maxPointCount); i++)
                {
                    m_pointIndex.Add(i);
                }

                m_mesh.Clear();
                m_mesh.vertices = m_points.ToArray();
                m_mesh.SetIndices(m_pointIndex.ToArray(), MeshTopology.Points, 0);
            }
        }
    }
}