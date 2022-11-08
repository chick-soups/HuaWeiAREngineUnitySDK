namespace Common
{
    using UnityEngine;
    using HuaweiARUnitySDK;
    using System.Collections.Generic;
    using HuaweiARInternal;

    public class HandVisualizer : MonoBehaviour
    {
        private ARHand m_hand;

        private Camera m_handCamera;

        private GameObject m_handBox;
        private LineRenderer m_boxLineRenderer;
        private Material m_skeletonMaterial;

        //hand skeleton
        private const int m_kMaxSkeletonPoint = 50;
        private const int m_kMaxSkeletonConnections = 50;
        private GameObject m_handSkeleton;
        private GameObject[] m_handSkeletonPoint = new GameObject[m_kMaxSkeletonPoint];
        private GameObject[] m_handSkeletonLines = new GameObject[m_kMaxSkeletonConnections];
        private LineRenderer[] m_handSkeletonConnectionRenderer = new LineRenderer[m_kMaxSkeletonConnections];


        private Dictionary<ARHand.SkeletonPointName, ARHand.SkeletonPointEntry> m_handSkeletons = new Dictionary
            <ARHand.SkeletonPointName, ARHand.SkeletonPointEntry>();
        private List<KeyValuePair<ARHand.SkeletonPointName, ARHand.SkeletonPointName>> m_connections = new List
            <KeyValuePair<ARHand.SkeletonPointName, ARHand.SkeletonPointName>>();

        public void Initialize(ARHand hand)
        {
            m_hand = hand;
            m_handCamera = Camera.main;

            m_handBox = new GameObject("HandBox");
            m_handBox.transform.localScale = new Vector3(1f, 1f, 1f);
            m_handBox.transform.position = new Vector3(0f, 0f, 0f);
            m_handBox.transform.localPosition = new Vector3(0, 0, 0);
            m_handBox.SetActive(false);

            m_boxLineRenderer = m_handBox.AddComponent<LineRenderer>();
            m_boxLineRenderer.positionCount = 5;
            m_boxLineRenderer.startWidth = 0.03f;
            m_boxLineRenderer.endWidth = 0.03f;

            //hand skeleton
            m_handSkeleton = new GameObject("HandSkeleton");
            m_handSkeleton.SetActive(false);
            for (int i = 0; i < m_kMaxSkeletonPoint; i++)
            {
                m_handSkeletonPoint[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                m_handSkeletonPoint[i].transform.localScale = new Vector3(0.008f, 0.008f, 0.008f);
                m_handSkeletonPoint[i].transform.SetParent(m_handSkeleton.transform, false);
            }
            for (int i = 0; i < m_kMaxSkeletonConnections; i++)
            {
                m_handSkeletonLines[i] = new GameObject();
                m_handSkeletonLines[i].transform.SetParent(m_handSkeleton.transform, false);

                m_handSkeletonConnectionRenderer[i] = m_handSkeletonLines[i].AddComponent<LineRenderer>();
                m_handSkeletonConnectionRenderer[i].positionCount = 2;
                m_handSkeletonConnectionRenderer[i].startWidth = 0.003f;
                m_handSkeletonConnectionRenderer[i].endWidth = 0.003f;
            }

        }
        public void Update()
        {
            if (null == m_hand)
            {
                return;
            }
            _DonotShowHandBox();
            if (m_hand.GetTrackingState() == ARTrackable.TrackingState.STOPPED)
            {
                Destroy(gameObject);
            }
            else if (m_hand.GetTrackingState() == ARTrackable.TrackingState.TRACKING)
            {
                _UpdateHandBox();
                _UpdateHandSkeleton();
            }
            else
            {
            }
        }

        private void _DonotShowHandBox()
        {
            m_handBox.SetActive(false);
            m_handSkeleton.SetActive(false);
        }

        private void _UpdateHandBox()
        {
            var handBox = m_hand.GetHandBox();

            if (handBox.Length < 2)
            {
                ARDebug.LogError("handbox's length is {0}", handBox.Length);
                return;
            }
            Vector3 glLeftTopCorner = handBox[0];
            Vector3 glRightBottomCorner = handBox[1];
            Vector3 glLeftBottomCorner = new Vector3(glLeftTopCorner.x, glRightBottomCorner.y);
            Vector3 glRightTopCorner = new Vector3(glRightBottomCorner.x, glLeftTopCorner.y);

            m_boxLineRenderer.SetPosition(0, _TransferGLCoord2UnityWoldCoordWithDepth(glLeftTopCorner));
            m_boxLineRenderer.SetPosition(1, _TransferGLCoord2UnityWoldCoordWithDepth(glRightTopCorner));
            m_boxLineRenderer.SetPosition(2, _TransferGLCoord2UnityWoldCoordWithDepth(glRightBottomCorner));
            m_boxLineRenderer.SetPosition(3, _TransferGLCoord2UnityWoldCoordWithDepth(glLeftBottomCorner));
            m_boxLineRenderer.SetPosition(4, _TransferGLCoord2UnityWoldCoordWithDepth(glLeftTopCorner));
            m_boxLineRenderer.gameObject.SetActive(true);
        }

        private void _UpdateHandSkeleton()
        {
            if (m_hand.GetSkeletonCoordinateSystemType() == ARCoordinateSystemType.COORDINATE_SYSTEM_TYPE_3D_CAMERA)
            {
                if (m_skeletonMaterial == null)
                {
                    m_skeletonMaterial = new Material(Shader.Find("Diffuse"));
                }
                Matrix4x4 camera2WorldMatrix = m_handCamera.cameraToWorldMatrix;
                m_handSkeleton.SetActive(true);
                //skeleton point 
                m_hand.GetSkeletons(m_handSkeletons);
                foreach (var skeleton in m_handSkeletons)
                {
                    m_handSkeletonPoint[(int)skeleton.Key].name = skeleton.Key.ToString();
                    Vector3 positionInCameraSpace = skeleton.Value.Coordinate;
                    Vector3 positionInWorldSpace = camera2WorldMatrix.MultiplyPoint(positionInCameraSpace);
                    m_handSkeletonPoint[(int)skeleton.Key].transform.position = positionInWorldSpace;
                    m_handSkeletonPoint[(int)skeleton.Key].GetComponent<MeshRenderer>().material = m_skeletonMaterial;
                }
                //skeleton connection
                m_hand.GetSkeletonConnection(m_connections);
                for (int i = 0; i < m_connections.Count; i++)
                {
                    ARHand.SkeletonPointEntry skpStart;
                    ARHand.SkeletonPointEntry skpEnd;
                    if (!m_handSkeletons.TryGetValue((ARHand.SkeletonPointName)m_connections[i].Key, out skpStart) ||
                        !m_handSkeletons.TryGetValue((ARHand.SkeletonPointName)m_connections[i].Value, out skpEnd))
                    {
                        continue;
                    }
                    Vector3 startCameraCoord = skpStart.Coordinate;
                    Vector3 endCameraCoord = skpEnd.Coordinate;
                    m_handSkeletonConnectionRenderer[i].SetPosition(0, camera2WorldMatrix.MultiplyPoint(startCameraCoord));
                    m_handSkeletonConnectionRenderer[i].SetPosition(1, camera2WorldMatrix.MultiplyPoint(endCameraCoord));
                }
            }


        }

        private Vector3 _TransferGLCoord2UnityWoldCoordWithDepth(Vector3 glCoord)
        {
            Vector3 screenCoord = new Vector3
            {
                x = (glCoord.x + 1) / 2,
                y = (glCoord.y + 1) / 2,
                z = 3,
            };
            return m_handCamera.ViewportToWorldPoint(screenCoord);
        }

        public void OnGUI()
        {
            GUIStyle bb = new GUIStyle();
            bb.normal.background = null;
            bb.normal.textColor = new Color(1, 0, 0);
            bb.fontSize = 30;

            if (m_hand.GetTrackingState() == ARTrackable.TrackingState.TRACKING)
            {
                GUI.Label(new Rect(0, 0, 200, 200), string.Format("GuestureType:{0}\n HandType:{1}\n" +
                    " GuestureCoord:{2}\n SkeletonCoord:{3}",
                    m_hand.GetGestureType(), m_hand.GetHandType(), m_hand.GetGestureCoordinateSystemType(),
                    m_hand.GetSkeletonCoordinateSystemType()), bb);
            }
        }
    }
}
