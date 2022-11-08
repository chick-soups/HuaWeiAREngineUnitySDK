using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuaweiARUnitySDK;

public class ARFaceMeshManager : MonoBehaviour {

    private Mesh faceMesh;
    private List<ARFace> m_newFaces = new List<ARFace>();


    // Use this for initialization
    void Start () {
        faceMesh = GetComponent<MeshFilter>().mesh;
        faceMesh.Clear();
    }
	
	// Update is called once per frame
	void Update () {
        m_newFaces.Clear();

        ARFrame.GetTrackables<ARFace>(m_newFaces, ARTrackableQueryFilter.ALL);
        if (m_newFaces.Count > 0)
        {
            if (m_newFaces[0].GetTrackingState() == ARTrackable.TrackingState.TRACKING)
            {
                _DrawFace(m_newFaces[0]);
            }
            else
            {
                faceMesh.Clear();
            }
            
        }
    }

    private void _DrawFace(ARFace face) {
        Pose pose = face.GetPose();
        Vector3 temp = pose.position;
        pose.position.x = temp.x;
        pose.position.y = temp.y;
        pose.position.z = -temp.z;
        temp = pose.rotation.eulerAngles;
        gameObject.transform.position = pose.position;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(temp.x, temp.y, temp.z));

        faceMesh.vertices = face.GetFaceGeometry().Vertices;
        faceMesh.uv = face.GetFaceGeometry().TextureCoordinates;
        faceMesh.triangles = face.GetFaceGeometry().TriangleIndices;
        
        // Assign the mesh object and update it.
        faceMesh.RecalculateBounds();
        faceMesh.RecalculateNormals();
    }
}
