namespace BodyARSample
{
    using UnityEngine;
    using System.Collections.Generic;
    using HuaweiARUnitySDK;
    using Common;
    using HuaweiARInternal;
    public class BodyARController : MonoBehaviour
    {
        [Tooltip("body prefabs")]
        public GameObject bodyPrefabs;

        private List<ARBody> newBodys = new List<ARBody>();

        private void Start()
        {
            DeviceChanged.OnDeviceChange += ARSession.SetDisplayGeometry;
        }

        public void Update()
        {
            _DrawBody();
        }

        private void _DrawBody()
        {
            newBodys.Clear();
            ARFrame.GetTrackables<ARBody>(newBodys, ARTrackableQueryFilter.NEW);
            
            for (int i = 0; i < newBodys.Count; i++)
            {
                GameObject planeObject = Instantiate(bodyPrefabs, Vector3.zero, Quaternion.identity, transform);
                planeObject.GetComponent<BodySkeletonVisualizer>().Initialize(newBodys[i]);
            }
        }
    }
}
