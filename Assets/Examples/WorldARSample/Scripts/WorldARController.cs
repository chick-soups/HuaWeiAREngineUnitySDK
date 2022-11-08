namespace Preview
{
    using UnityEngine;
    using System.Collections.Generic;
    using HuaweiARUnitySDK;
    using System.Collections;
    using System;
    using HuaweiARInternal;
    using Common;

    public class WorldARController : MonoBehaviour
    {
        [Tooltip("plane visualizer")]
        public GameObject planePrefabs;

        [Tooltip("plane label visualizer")]
        public GameObject planeLabelPrefabs;

        [Tooltip("green logo visualizer")]
        public GameObject arDiscoveryLogoPlanePrefabs;

        [Tooltip("blue logo visualizer")]
        public GameObject arDiscoveryLogoPointPrefabs;

        private List<ARAnchor> addedAnchors = new List<ARAnchor>();
        private List<ARPlane> newPlanes = new List<ARPlane>();

        private void Start()
        {
            DeviceChanged.OnDeviceChange += ARSession.SetDisplayGeometry;
        }

        public void Update()
        {
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

        private void _DrawPlane()
        {
            newPlanes.Clear();
            ARFrame.GetTrackables<ARPlane>(newPlanes,ARTrackableQueryFilter.NEW);
            for (int i = 0; i < newPlanes.Count; i++)
            {
                GameObject planeObject = Instantiate(planePrefabs, Vector3.zero, Quaternion.identity, transform);
                planeObject.GetComponent<TrackedPlaneVisualizer>().Initialize(newPlanes[i]);


                GameObject planeLabelObject = Instantiate(planeLabelPrefabs, Vector3.zero, Quaternion.identity, transform);
                planeLabelObject.GetComponent<PlaneLabelVisualizer>().Initialize(newPlanes[i]);
            }
        }

        private void _DrawARLogo(Touch touch)
        {
            List<ARHitResult> hitResults = ARFrame.HitTest(touch);
            ARHitResult hitResult = null;
            ARTrackable trackable = null;
            Boolean hasHitFlag = false;
            ARDebug.LogInfo("_DrawARLogo hitResults count {0}", hitResults.Count);
            foreach (ARHitResult singleHit in hitResults)
            {
                trackable = singleHit.GetTrackable();
                ARDebug.LogInfo("_DrawARLogo GetTrackable {0}", singleHit.GetTrackable());
                if((trackable is ARPlane && ((ARPlane)trackable).IsPoseInPolygon(singleHit.HitPose)) ||
                    (trackable is ARPoint))
                {
                    hitResult = singleHit;
                    hasHitFlag = true;
                    if (trackable is ARPlane)
                    {
                        break;
                    }                 
                }
            }

            if (hasHitFlag != true)
            {
                ARDebug.LogInfo("_DrawARLogo can't hit!");
                return;
            }

            if (addedAnchors.Count > 16)
            {
                ARAnchor toRemove = addedAnchors[0];
                toRemove.Detach();
                addedAnchors.RemoveAt(0);
            }

            GameObject prefab;
            trackable = hitResult.GetTrackable();
            if (trackable is ARPlane)
            {
                prefab = arDiscoveryLogoPlanePrefabs;
            }
            else
            {
                prefab = arDiscoveryLogoPointPrefabs;
            }

            ARAnchor anchor = hitResult.CreateAnchor();
            var logoObject = Instantiate(prefab, anchor.GetPose().position, anchor.GetPose().rotation);
            logoObject.GetComponent<ARDiscoveryLogoVisualizer>().Initialize(anchor);
            addedAnchors.Add(anchor);
        }
    }
}
