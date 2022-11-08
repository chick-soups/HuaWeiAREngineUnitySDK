namespace Common
{
    using System.Collections.Generic;
    using HuaweiARUnitySDK;
    using UnityEngine;
    using HuaweiARInternal;

    public class TrackedPlaneVisualizer : MonoBehaviour
    {
        private static int s_planeCount = 0;

        private readonly Color[] k_planeColors = new Color[]
        {
            new Color(1.0f, 1.0f, 1.0f),
            new Color(0.5f,0.3f,0.9f),
            new Color(0.8f,0.4f,0.8f),
            new Color(0.5f,0.8f,0.4f),
            new Color(0.5f,0.9f,0.8f)
        };

        private ARPlane m_trackedPlane;

        // Keep previous frame's mesh polygon to avoid mesh update every frame.
        private List<Vector3> m_previousFrameMeshVertices = new List<Vector3>();
        private List<Vector3> m_meshVertices3D = new List<Vector3>();
        private List<Vector2> m_meshVertices2D = new List<Vector2>();

        private List<Color> m_meshColors = new List<Color>();

        private Mesh m_mesh;

        private MeshRenderer m_meshRenderer;

        public void Awake()
        {
            m_mesh = GetComponent<MeshFilter>().mesh;
            m_meshRenderer = GetComponent<MeshRenderer>();
        }

        public void Update()
        {
            if (m_trackedPlane == null)
            {
                return;
            }
            else if (m_trackedPlane.GetSubsumedBy() != null
                || m_trackedPlane.GetTrackingState() == ARTrackable.TrackingState.STOPPED)
            {
                Destroy(gameObject);
                return;
            }
            else if (m_trackedPlane.GetTrackingState()==ARTrackable.TrackingState.PAUSED) // whether to destory gameobject if not tracking
            {
                m_meshRenderer.enabled = false;
                return;
            }

            m_meshRenderer.enabled = true;
            _UpdateMeshIfNeeded();
        }

		public void Initialize(ARPlane plane)
        {
            m_trackedPlane = plane;
            m_meshRenderer.material.SetColor("_GridColor", k_planeColors[s_planeCount++ % k_planeColors.Length]);
            Update();
        }

        private void _UpdateMeshIfNeeded()
        {
            m_meshVertices3D.Clear();
            m_trackedPlane.GetPlanePolygon(m_meshVertices3D);

            if (_AreVerticesListsEqual(m_previousFrameMeshVertices, m_meshVertices3D))
            {
                return;
            }

            Pose centerPose = m_trackedPlane.GetCenterPose();
            for(int i = 0; i < m_meshVertices3D.Count; i++)
            {
                m_meshVertices3D[i] = centerPose.rotation * m_meshVertices3D[i] + centerPose.position;
            }

            Vector3 planeNormal = centerPose.rotation * Vector3.up;
            m_meshRenderer.material.SetVector("_PlaneNormal", planeNormal);

            m_previousFrameMeshVertices.Clear();
            m_previousFrameMeshVertices.AddRange(m_meshVertices3D);

            m_meshVertices2D.Clear();
            m_trackedPlane.GetPlanePolygon(ref m_meshVertices2D);

            Triangulator tr = new Triangulator(m_meshVertices2D);

            m_mesh.Clear();
            m_mesh.SetVertices(m_meshVertices3D);
            m_mesh.SetIndices(tr.Triangulate(), MeshTopology.Triangles, 0);
            m_mesh.SetColors(m_meshColors);

        }

        private bool _AreVerticesListsEqual(List<Vector3> firstList, List<Vector3> secondList)
        {
            if (firstList.Count != secondList.Count)
            {
                return false;
            }

            for (int i = 0; i < firstList.Count; i++)
            {
                if (firstList[i] != secondList[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
