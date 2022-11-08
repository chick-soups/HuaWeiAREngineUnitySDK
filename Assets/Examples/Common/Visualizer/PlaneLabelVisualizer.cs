using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuaweiARUnitySDK;
using HuaweiARInternal;

public class PlaneLabelVisualizer : MonoBehaviour
{

    private ARPlane m_trackedPlane;
    private TextMesh m_textMesh;
    private MeshRenderer m_meshRenderer;

    public void Awake()
    {
        m_textMesh = GetComponent<TextMesh>();
        m_meshRenderer = GetComponent<MeshRenderer>();
        ARDebug.LogInfo("PlaneLabelVisualizer Awake");
    }

    public void Initialize(ARPlane plane)
    {
        m_trackedPlane = plane;
        Update();
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
        else if (m_trackedPlane.GetTrackingState() == ARTrackable.TrackingState.PAUSED) // whether to destory gameobject if not tracking
        {
            m_textMesh.text = "";
            return;
        }

        Pose centerPose = m_trackedPlane.GetCenterPose();

        ARPlane.ARPlaneSemanticLabel label = m_trackedPlane.GetARPlaneLabel();

        m_textMesh.text = label.ToString();
        m_textMesh.anchor = TextAnchor.MiddleCenter;
        m_textMesh.transform.position = centerPose.position;

        m_textMesh.transform.rotation = centerPose.rotation;
        transform.RotateAround(centerPose.position, centerPose.rotation * Vector3.right, 90f);

        transform.RotateAround(centerPose.position, centerPose.rotation * Vector3.up, 180f);

    }
}
