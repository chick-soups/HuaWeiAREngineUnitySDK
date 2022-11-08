using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaweiARUnitySDK;
using UnityEngine;
using Common;
using HuaweiARInternal;

namespace Assets.Examples.Common.Visualizer
{
    public class SceneMeshVisualizer : MonoBehaviour
    {
        private Mesh m_mesh;
        private ARSceneMesh sceneMesh = null;
        private Vector3[] m_points = null;
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
            sceneMesh = ARFrame.AcquireSceneMesh();
            m_points = sceneMesh.Vertices;
            ARDebug.LogInfo("_DrawMeshPoint Point count {0}",m_points.Length);
            if (m_points.Length > 0)
            {
                m_mesh.Clear();

                m_mesh.vertices = m_points;


                m_mesh.triangles = sceneMesh.TriangleIndices;

                m_mesh.RecalculateBounds();
                m_mesh.RecalculateNormals();
            }
        }
    }

}
