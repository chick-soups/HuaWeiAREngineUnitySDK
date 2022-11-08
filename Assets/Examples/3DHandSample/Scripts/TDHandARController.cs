namespace TDHandARSample
{
    using UnityEngine;
    using System.Collections.Generic;
    using HuaweiARUnitySDK;
    using System.Collections;
    using System;
    using Common;
    public class TDHandARController : MonoBehaviour
    {
        [Tooltip("hand prefabs")]
        public GameObject handPrefabs;

        private List<ARHand> newHands = new List<ARHand>();

        private void Start()
        {
            DeviceChanged.OnDeviceChange += ARSession.SetDisplayGeometry;
        }

        public void Update()
        {
            _DrawHand();
        }

        private void _DrawHand()
        {
            newHands.Clear();
            ARFrame.GetTrackables<ARHand>(newHands, ARTrackableQueryFilter.NEW);
            for (int i = 0; i < newHands.Count; i++)
            {
                GameObject handObject = Instantiate(handPrefabs, Vector3.zero, Quaternion.identity, transform);
                handObject.GetComponent<HandVisualizer>().Initialize(newHands[i]);
            }
        }
    }
}