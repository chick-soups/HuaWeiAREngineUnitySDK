namespace BodyARSample
{
    using UnityEngine;
    using System.Collections.Generic;
    using HuaweiARUnitySDK;
    using Common;
    using HuaweiARInternal;
    public class videoBodyARController : MonoBehaviour
    {
        [Tooltip("body prefabs")]
        public GameObject bodyPrefabs;

        private List<ARBody> newBodys = new List<ARBody>();

        private string videoPath2 = "/storage/emulated/0/DCIM/Camera/test.mp4";

        private ARVideoSource videoSource;

        private bool isPause = false;
        private void Start()
        {
            DeviceChanged.OnDeviceChange += ARSession.SetDisplayGeometry;

            videoSource = new ARVideoSource(videoPath2, ARSessionManager.Instance.m_ndkSession);
            videoSource.InitVideoPlayer();
            ARSession.SetDisplayGeometryForVideoMode(ScreenOrientation.Portrait, Screen.width, Screen.height);
            videoSource.StartPlayVideo();
        }

        public void Update()
        {
            _DrawBody();
        }

        void OnApplicationPause()
        {
            if (!isPause)
            {
                videoSource.PauseVideoPlay();
                ARDebug.LogInfo("Player is paused.");
                isPause = true;
            }
            else
            {
                videoSource.ResumeVideoPlay();
                ARDebug.LogInfo("Player has resume.");
                isPause = false;
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
    }
}
