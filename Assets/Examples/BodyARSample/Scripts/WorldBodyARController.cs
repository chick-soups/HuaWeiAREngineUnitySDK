namespace BodyARSample
{
    using UnityEngine;
    using System.Collections.Generic;
    using HuaweiARUnitySDK;
    using Common;
    using HuaweiARInternal;
    public class WorldBodyARController : MonoBehaviour
    {
        [Tooltip("body prefabs")]
        public GameObject bodyPrefabs;

        [Tooltip("plane visualizer")]
        public GameObject planePrefabs;

        [Tooltip("logo visualizer")]
        public GameObject arDiscoveryLogoPrefabs;


        private List<ARAnchor> addedAnchors = new List<ARAnchor>();
        private List<ARPlane> newPlanes = new List<ARPlane>();

        private List<ARBody> newBodys = new List<ARBody>();

        private void Start()
        {
            DeviceChanged.OnDeviceChange += ARSession.SetDisplayGeometry;
        }

        public void Update()
        {
            _DrawBody();
            _DrawPlane();
            Touch touch;
            if (ARFrame.GetTrackingState() != ARTrackable.TrackingState.TRACKING
                || Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            {

            }
            else
            {
                _DrawARLogo(touch);
            }
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
        private void _DrawPlane()
        {
            newPlanes.Clear();
            ARFrame.GetTrackables<ARPlane>(newPlanes, ARTrackableQueryFilter.NEW);
            for (int i = 0; i < newPlanes.Count; i++)
            {
                GameObject planeObject = Instantiate(planePrefabs, Vector3.zero, Quaternion.identity, transform);
                planeObject.GetComponent<TrackedPlaneVisualizer>().Initialize(newPlanes[i]);
            }
        }

        private void _DrawARLogo(Touch touch)
        {
            List<ARHitResult> hitResults = ARFrame.HitTest(touch);

            foreach (ARHitResult singleHit in hitResults)
            {
                ARTrackable trackable = singleHit.GetTrackable();
                if (trackable is ARPlane && ((ARPlane)trackable).IsPoseInPolygon(singleHit.HitPose))
                {
                    if (addedAnchors.Count > 16)
                    {
                        ARAnchor toRemove = addedAnchors[0];
                        toRemove.Detach();
                        addedAnchors.RemoveAt(0);
                    }
                    ARAnchor anchor = singleHit.CreateAnchor();
                    var logoObject = Instantiate(arDiscoveryLogoPrefabs, anchor.GetPose().position, anchor.GetPose().rotation);
                    logoObject.GetComponent<ARDiscoveryLogoVisualizer>().Initialize(anchor);
                    addedAnchors.Add(anchor);
                    break;
                }
            }
        }
    }
}
