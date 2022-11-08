//-----------------------------------------------------------------------
// <copyright file="AugmentedImageExampleController.cs" company="Google">
//
// Copyright 2018 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace HuaweiARUnitySDK.Examples.AugmentedImage
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using HuaweiARUnitySDK;
    using UnityEngine;
    using HuaweiARInternal;
    using UnityEngine.UI;
    using Common;

    /// <summary>
    /// Controller for AugmentedImage example.
    /// </summary>
    public class AugmentedImageExampleController : MonoBehaviour
    {
        /// <summary>
        /// A prefab for visualizing an AugmentedImage.
        /// </summary>
        public AugmentedImageVisualizer AugmentedImageVisualizerPrefab;

        /// <summary>
        /// The overlay containing the fit to scan user guide.
        /// </summary>
        public GameObject FitToScanOverlay;

        private Dictionary<int, AugmentedImageVisualizer> m_Visualizers
            = new Dictionary<int, AugmentedImageVisualizer>();

        private List<ARAugmentedImage> m_TempAugmentedImages = new List<ARAugmentedImage>();

        /// <summary>
        /// The Unity Update method.
        /// </summary>
		public void Awake()
		{
			FitToScanOverlay.SetActive(true);
            DeviceChanged.OnDeviceChange += ARSession.SetDisplayGeometry;
        }
        public void Update()
        {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            // Check that motion tracking is tracking.
            if (ARFrame.GetTrackingState() != ARTrackable.TrackingState.TRACKING)
            {
                ARDebug.LogInfo("GetTrackingState no tracing return <<");
                return;
            }

            // Get updated augmented images for this frame.
            ARFrame.GetTrackables<ARAugmentedImage>(m_TempAugmentedImages, ARTrackableQueryFilter.UPDATED);
            ARDebug.LogInfo("m_TempAugmentedImages size {0}", m_TempAugmentedImages.Count);
            
            // Create visualizers and anchors for updated augmented images that are tracking and do not previously
            // have a visualizer. Remove visualizers for stopped images.
            foreach (var image in m_TempAugmentedImages)
            {
                AugmentedImageVisualizer visualizer = null;
                m_Visualizers.TryGetValue(image.GetDataBaseIndex(), out visualizer);

                ARDebug.LogInfo("GetTrackingState {0}", image.GetTrackingState());
                if (image.GetTrackingState() == ARTrackable.TrackingState.TRACKING && visualizer != null)
                {
                    visualizer.Image = image;
                    ARDebug.LogInfo("update position {0} rotation {1}", image.GetCenterPose().position, image.GetCenterPose().rotation);
                }
                
                if (image.GetTrackingState() == ARTrackable.TrackingState.TRACKING && visualizer == null)
                {
                    // Create an anchor to ensure that sdk keeps tracking this augmented image.
                    ARAnchor anchor = image.CreateAnchor(image.GetCenterPose());
                    visualizer = (AugmentedImageVisualizer)Instantiate(AugmentedImageVisualizerPrefab, anchor.GetPose().position, anchor.GetPose().rotation);
                    //visualizer = (AugmentedImageVisualizer)Instantiate(AugmentedImageVisualizerPrefab, image.GetCenterPose().position, image.GetCenterPose().rotation);
                    ARDebug.LogInfo("create position {0} rotation {1}", image.GetCenterPose().position, image.GetCenterPose().rotation);
                    visualizer.Image = image;
                    m_Visualizers.Add(image.GetDataBaseIndex(), visualizer);
                }
                else if (image.GetTrackingState() == ARTrackable.TrackingState.STOPPED && visualizer != null)
                {
                    m_Visualizers.Remove(image.GetDataBaseIndex());
                    GameObject.Destroy(visualizer.gameObject);
                }
            }

            // Show the fit-to-scan overlay if there are no images that are Tracking.
            foreach (var visualizer in m_Visualizers.Values)
            {
                if (visualizer.Image.GetTrackingState() == ARTrackable.TrackingState.TRACKING)
                {
                    FitToScanOverlay.SetActive(false);
                    return;
                }
            }
        }
    }
}
