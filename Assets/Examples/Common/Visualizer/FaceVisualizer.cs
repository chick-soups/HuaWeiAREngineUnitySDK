namespace Common
{
    using UnityEngine;
    using HuaweiARUnitySDK;
    using System.Collections.Generic;
    using HuaweiARInternal;
    using System.Text;

    public class FaceVisualizer : MonoBehaviour
    {
        private ARFace m_face;
        StringBuilder sb = new StringBuilder();
        public void Initialize(ARFace face)
        {
            m_face = face;
            Update();
        }

        public void Update()
        {
            sb.Remove(0, sb.Length);
            sb.Append("No Data");
            if (null == m_face)
            {
                return;
            }
            else if(m_face.GetTrackingState() == ARTrackable.TrackingState.PAUSED)
            {
                //disable face renderer
                return;
            }
            else if (m_face.GetTrackingState() == ARTrackable.TrackingState.STOPPED)
            {
                Destroy(gameObject);
                return;
            }
            //update according to Face parameter
            sb.Remove(0, sb.Length);

            sb.Append("Face Pose: ");
            sb.Append(m_face.GetPose().ToString());
            sb.Append("\n");

            var blendShapes = m_face.GetBlendShapeWithBlendName();
            int indexNum = 0;
            foreach(var bs in blendShapes)
            {
                sb.Append("<" + (indexNum++) + ">");
                sb.Append(bs.Key);
                sb.Append(": ");
                sb.Append(bs.Value);
                sb.Append("\n");
            }
            
            var faceGeometry = m_face.GetFaceGeometry();
            sb.Append("Face Geometry triangle count: ");
            sb.Append(faceGeometry.TriangleCount);
            sb.Append("\n");
        }

        public void OnGUI()
        {
            GUIStyle bb = new GUIStyle();
            bb.normal.background = null;
            bb.normal.textColor = new Color(1, 0, 0);
            bb.fontSize = 20;
            GUI.Label(new Rect(0, 0, 200, 200), sb.ToString(), bb);
        }
    }
}
