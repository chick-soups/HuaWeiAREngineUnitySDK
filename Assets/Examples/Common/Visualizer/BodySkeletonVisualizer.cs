
namespace Common
{
    using UnityEngine;
    using HuaweiARUnitySDK;
    using System.Collections.Generic;
    using HuaweiARInternal;
    using System.Text;
    public class BodySkeletonVisualizer : MonoBehaviour
    {
        private ARBody m_body;
        StringBuilder sb = new StringBuilder();
        private static int m_maxSkeletonCnt = 50;
        private static int m_maxSkeletonConnectionCnt = 100;

        private GameObject[] m_skeletonPointObject = new GameObject[m_maxSkeletonCnt];
        private GameObject[] m_lines = new GameObject[m_maxSkeletonConnectionCnt];
        private LineRenderer[] m_skeletonConnectionRenderer = new LineRenderer[m_maxSkeletonConnectionCnt];
        private Material m_skeletonMaterial;

        private Camera m_skeletonCamera;
        private ARCameraConfig m_cameraConfig;

        private Vector2Int m_Imagesize = new Vector2Int(0, 0);
        private Vector2Int m_Texturesize = new Vector2Int(0, 0);
        private float m_Confidence_first = 2;
        private short m_Depth_first = 2;
        private float m_Confidence_last = 2;
        private short m_Depth_last = 2;

        private Dictionary<ARBody.SkeletonPointName, ARBody.SkeletonPointEntry> m_bodySkeletons= new Dictionary
            <ARBody.SkeletonPointName, ARBody.SkeletonPointEntry>();
        private List<KeyValuePair<ARBody.SkeletonPointName, ARBody.SkeletonPointName>> m_connections = new List
            <KeyValuePair<ARBody.SkeletonPointName, ARBody.SkeletonPointName>>();
        public void Initialize(ARBody body)
        {
            m_body = body;
            m_skeletonMaterial = new Material(Shader.Find("Diffuse"));
            m_cameraConfig = ARSession.GetCameraConfig();

            for (int i = 0; i < m_maxSkeletonCnt; i++)
            {
                m_skeletonPointObject[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                m_skeletonPointObject[i].transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
                m_skeletonPointObject[i].SetActive(false);
            }

            for(int i = 0; i < m_maxSkeletonConnectionCnt; i++)
            {
                m_lines[i] = new GameObject("Lines");
                m_lines[i].transform.localScale = new Vector3(1f, 1f, 1f);
                m_lines[i].transform.position = new Vector3(0f, 0f, 0f);
                m_lines[i].transform.localPosition = new Vector3(0, 0, 0);
                m_lines[i].SetActive(false);

                m_skeletonConnectionRenderer[i] = m_lines[i].AddComponent<LineRenderer>();
                m_skeletonConnectionRenderer[i].positionCount = 2;
                m_skeletonConnectionRenderer[i].startWidth = 0.03f;
                m_skeletonConnectionRenderer[i].endWidth = 0.03f;
            }
            m_skeletonCamera = Camera.main;
            Update();
        }


        public void Update()
        {
            sb.Remove(0, sb.Length);
            
            if (null == m_body)
            {
                return;
            }
            _DonotShowPointAndConnections();
            if (m_body.GetTrackingState() == ARTrackable.TrackingState.STOPPED)
            {
                Destroy(gameObject);
            }
            else if (m_body.GetTrackingState() == ARTrackable.TrackingState.TRACKING)
            {
                sb.Append("BodyAction: " + m_body.GetBodyAction() + "\n");
                _UpdateBody();
            }
            else
            {

            }
        }


        private void _DonotShowPointAndConnections()
        {
            for (int i = 0; i < m_maxSkeletonCnt; i++)
            {
                m_skeletonPointObject[i].SetActive(false);
            }
            for (int i = 0; i < m_maxSkeletonConnectionCnt; i++)
            {
                m_lines[i].SetActive(false);
            }
        }
        private void _UpdateBody()
        {
            if (m_body.GetCoordinateSystemType() == ARCoordinateSystemType.COORDINATE_SYSTEM_TYPE_3D_CAMERA)
            {
                sb.Append("BodyType: 3D\n\n");
                BodyMaskHelper.UseMask = 1;
                Vector2Int size = m_cameraConfig.GetTextureDimensions();
                BodyMaskHelper.width = size.x;
                BodyMaskHelper.height = size.y;
                BodyMaskHelper.bodyMaskPtr = m_body.GetMaskConfidence();
                _Draw3DBody();
            }
            else
            {
                sb.Append("BodyType: 2D\n\n");
                _Draw2DBody();
            }
        }

        private void _Draw2DBody()
        {
            sb.Append("Skeleton Confidence: \n");
            m_body.GetSkeletons(m_bodySkeletons);
            foreach (var pair in m_bodySkeletons)
            {
                if (!pair.Value.Is2DValid)
                {
                    continue;
                }
                m_skeletonPointObject[(int)pair.Key].name = pair.Key.ToString();

                Vector3 glCoord = pair.Value.Coordinate2D;
                Vector3 worldCoord = new Vector3((glCoord.x + 1) / 2,
                    (glCoord.y + 1) / 2, 3);

                m_skeletonPointObject[(int)pair.Key].transform.position = m_skeletonCamera.ViewportToWorldPoint(worldCoord);
                m_skeletonPointObject[(int)pair.Key].GetComponent<MeshRenderer>().material = m_skeletonMaterial;
                m_skeletonPointObject[(int)pair.Key].SetActive(true);
                sb.Append(pair.Key.ToString());
                sb.Append(": ");
                sb.Append(pair.Value.Confidence);
                sb.Append("\n");
            }

            m_body.GetSkeletonConnection(m_connections);
            for (int i = 0; i < m_connections.Count; i++)
            {
                ARBody.SkeletonPointEntry skpStart;
                ARBody.SkeletonPointEntry skpEnd;
                if (!m_bodySkeletons.TryGetValue(m_connections[i].Key, out skpStart) ||
                    !m_bodySkeletons.TryGetValue(m_connections[i].Value, out skpEnd) ||
                    !skpStart.Is2DValid || !skpEnd.Is2DValid)
                {
                    continue;
                }
                Vector3 startGLScreenCoord = skpStart.Coordinate2D;
                Vector3 startScreenCoord = new Vector3((startGLScreenCoord.x + 1) / 2,
                    (startGLScreenCoord.y + 1) / 2, 3);

                Vector3 endGLScreenCoord = skpEnd.Coordinate2D;
                Vector3 endScreenCoord = new Vector3((endGLScreenCoord.x + 1) / 2,
                    (endGLScreenCoord.y + 1) / 2, 3);

                m_skeletonConnectionRenderer[i].SetPosition(0, m_skeletonCamera.ViewportToWorldPoint(startScreenCoord));
                m_skeletonConnectionRenderer[i].SetPosition(1, m_skeletonCamera.ViewportToWorldPoint(endScreenCoord));
                m_skeletonConnectionRenderer[i].gameObject.SetActive(true);
            }
        }

        private void _Draw3DBody()
        {
            m_Imagesize = m_cameraConfig.GetImageDimensions();
            m_Texturesize = m_cameraConfig.GetTextureDimensions();

            if (SessionComponent.isEnableMask)
            {
                m_Confidence_first = MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(m_body.GetMaskConfidence(), 0);
                m_Depth_first = MarshalingHelper.GetValueOfUnmanagedArrayElement<short>(m_body.GetMaskDepth(), 0);
                m_Confidence_last = MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(m_body.GetMaskConfidence(), m_Texturesize[0] * m_Texturesize[1] - 1);
                m_Depth_last = MarshalingHelper.GetValueOfUnmanagedArrayElement<short>(m_body.GetMaskDepth(), m_Texturesize[0] * m_Texturesize[1] - 1);

                sb.Append("ImageSize: " + m_Imagesize + "\n");
                sb.Append("TextureSize: " + m_Texturesize + "\n");
                sb.Append("Mask_Confidence_first: " + m_Confidence_first + "\n");
                sb.Append("Mask_Depth_first: " + m_Depth_first + "\n");
                sb.Append("Mask_Confidence_last: " + m_Confidence_last + "\n");
                sb.Append("Mask_Depth_last: " + m_Depth_last + "\n\n");
                sb.Append("Skeleton Confidence: \n");
            }


            m_body.GetSkeletons(m_bodySkeletons);
            Matrix4x4 camera2WorldMatrix = m_skeletonCamera.cameraToWorldMatrix;
            foreach (var pair in m_bodySkeletons)
            {
                if (!pair.Value.Is3DValid)
                {
                    continue;
                }
                m_skeletonPointObject[(int)pair.Key].name = pair.Key.ToString();

                Vector3 positionInCameraSpace = pair.Value.Coordinate3D;
                Vector3 positionInWorldSpace = camera2WorldMatrix.MultiplyPoint(positionInCameraSpace);

                m_skeletonPointObject[(int)pair.Key].transform.position = positionInWorldSpace;
                m_skeletonPointObject[(int)pair.Key].GetComponent<MeshRenderer>().material = m_skeletonMaterial;
                m_skeletonPointObject[(int)pair.Key].SetActive(true);
                sb.Append(pair.Key.ToString());
                sb.Append(": ");
                sb.Append(pair.Value.Confidence);
                sb.Append("\n");
            }

            m_body.GetSkeletonConnection(m_connections);
            for (int i = 0; i < m_connections.Count; i++)
            {
                ARBody.SkeletonPointEntry skpStart;
                ARBody.SkeletonPointEntry skpEnd;
                if (!m_bodySkeletons.TryGetValue(m_connections[i].Key, out skpStart) ||
                    !m_bodySkeletons.TryGetValue(m_connections[i].Value, out skpEnd) ||
                    !skpStart.Is3DValid || !skpEnd.Is3DValid)
                {
                    continue;
                }
                Vector3 startGLScreenCoord = skpStart.Coordinate3D;

                Vector3 endGLScreenCoord = skpEnd.Coordinate3D;

                m_skeletonConnectionRenderer[i].SetPosition(0, camera2WorldMatrix.MultiplyPoint(startGLScreenCoord));
                m_skeletonConnectionRenderer[i].SetPosition(1, camera2WorldMatrix.MultiplyPoint(endGLScreenCoord));
                m_skeletonConnectionRenderer[i].gameObject.SetActive(true);
            }
        }


        void OnGUI()
        {
            GUIStyle bb = new GUIStyle();
            bb.normal.background = null;
            bb.normal.textColor = new Color(1, 0, 0);
            bb.fontSize = 40;
            if (m_body.GetTrackingState() == ARTrackable.TrackingState.TRACKING)
            {
                GUI.Label(new Rect(0, 0, 200, 200), sb.ToString(), bb);
            }
        }
    }
}
