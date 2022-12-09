//-----------------------------------------------------------------------
// <copyright file="AugmentedImageVisualizer.cs" company="Google">
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
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using HuaweiARUnitySDK;
    using HuaweiARInternal;
    using UnityEngine;

    /// <summary>
    /// Uses 4 frame corner objects to visualize an AugmentedImage.
    /// </summary>
    public class AugmentedImageVisualizer : MonoBehaviour
    {
        /// <summary>
        /// The AugmentedImage to visualize.
        /// </summary>
        public ARAugmentedImage Image;

        /// <summary>
        /// A model for the lower left corner of the frame to place when an image is detected.
        /// </summary>
        public GameObject FrameLowerLeft;

        /// <summary>
        /// A model for the lower right corner of the frame to place when an image is detected.
        /// </summary>
        public GameObject FrameLowerRight;

        /// <summary>
        /// A model for the upper left corner of the frame to place when an image is detected.
        /// </summary>
        public GameObject FrameUpperLeft;

        /// <summary>
        /// A model for the upper right corner of the frame to place when an image is detected.
        /// </summary>
        public GameObject FrameUpperRight;

        /// <summary>
        /// The Unity Update method.
        /// </summary>
        public void Update()
        {
            if (Image == null || Image.GetTrackingState() != ARTrackable.TrackingState.TRACKING)
            {
                FrameLowerLeft.SetActive(false);
                FrameLowerRight.SetActive(false);
                FrameUpperLeft.SetActive(false);
                FrameUpperRight.SetActive(false);
                return;
            }

            float halfWidth = Image.GetExtentX() / 2;
            float halfHeight = Image.GetExtentZ() / 2;

            //arengine GetExtentX&GetExtentZ() isn't changing when camera moving,so using Image.GetCenterPose to move FrameLowerLeft etc.
            Quaternion poseRotation = Image.GetCenterPose().rotation;
            FrameLowerLeft.transform.position = poseRotation * ((halfWidth * Vector3.left) + (halfHeight * Vector3.back)) + Image.GetCenterPose().position;
            FrameLowerLeft.transform.rotation = poseRotation;
            FrameLowerRight.transform.position = poseRotation * ((halfWidth * Vector3.right) + (halfHeight * Vector3.back)) + Image.GetCenterPose().position;
            FrameLowerRight.transform.rotation = poseRotation;
            FrameUpperLeft.transform.position = poseRotation * ((halfWidth * Vector3.left) + (halfHeight * Vector3.forward)) + Image.GetCenterPose().position;
            FrameUpperLeft.transform.rotation = poseRotation;
            FrameUpperRight.transform.position = poseRotation * ((halfWidth * Vector3.right) + (halfHeight * Vector3.forward)) + Image.GetCenterPose().position;
            FrameUpperRight.transform.rotation = poseRotation;

            ARDebug.LogInfo("image position {0} rotation {1} width {2} height {3}", Image.GetCenterPose().position, 
                Image.GetCenterPose().rotation, halfWidth, halfHeight);

            FrameLowerLeft.SetActive(true);
            FrameLowerRight.SetActive(true);
            FrameUpperLeft.SetActive(true);
            FrameUpperRight.SetActive(true);
        }
    }
}
