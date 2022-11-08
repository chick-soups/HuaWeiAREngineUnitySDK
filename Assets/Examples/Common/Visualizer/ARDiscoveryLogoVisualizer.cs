namespace Common
{
    using UnityEngine;
    using HuaweiARUnitySDK;
    using HuaweiARInternal;
    public class ARDiscoveryLogoVisualizer:MonoBehaviour
    {
        private ARAnchor m_anchor;
        private MeshRenderer m_MeshRenderer;

        public void Awake()
        {
            m_MeshRenderer = GetComponent<MeshRenderer>();
            
        }
        public void Initialize(ARAnchor anchor)
        {
            m_anchor = anchor;
            Update();
        }
        public void Update()
        {
            if (null == m_anchor)
            {
                m_MeshRenderer.enabled = false;
                return;
            }
            switch (m_anchor.GetTrackingState())
            {
                case ARTrackable.TrackingState.TRACKING:
                    Pose p = m_anchor.GetPose();
                    gameObject.transform.position = p.position;
                    gameObject.transform.rotation = p.rotation;
                    gameObject.transform.Rotate(0f, 225f, 0f, Space.Self);
                    m_MeshRenderer.enabled = true;
                    break;
                case ARTrackable.TrackingState.PAUSED:
                    m_MeshRenderer.enabled = false;
                    break;
                case ARTrackable.TrackingState.STOPPED:
                default:
                    m_MeshRenderer.enabled = false;
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
