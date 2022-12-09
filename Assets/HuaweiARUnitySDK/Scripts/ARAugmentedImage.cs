//-----------------------------------------------------------------------
// <copyright file="AugmentedImageDatabase.cs" company="Google">
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
///@cond ContainImageAR
namespace HuaweiARUnitySDK
{
    using HuaweiARInternal;
    using System;
    using UnityEngine;

    public class ARAugmentedImage : ARTrackable
    {

        internal ARAugmentedImage(IntPtr trackableHandle, NDKSession session) : base(trackableHandle, session)
        {

        }

        public Pose GetCenterPose()
        {
            return m_ndkSession.AugmentedImageAdapter.GetCenterPose(m_trackableHandle);
        }

        public string GetCloudImageId()
        {
            return m_ndkSession.AugmentedImageAdapter.GetCloudImageId(m_trackableHandle);
        }

        public string GetCloudImageMetadata()
        {
            return m_ndkSession.AugmentedImageAdapter.GetCloudImageMetadata(m_trackableHandle);
        }


        public float GetExtentX()
        {
            return m_ndkSession.AugmentedImageAdapter.GetExtentX(m_trackableHandle);
        }
        public float GetExtentZ()
        {
            return m_ndkSession.AugmentedImageAdapter.GetExtentZ(m_trackableHandle);
        }

        public int GetDataBaseIndex()
        {
            return m_ndkSession.AugmentedImageAdapter.GetDataBaseIndex(m_trackableHandle);
        }

        public string AcquireName()
        {
            return m_ndkSession.AugmentedImageAdapter.AcquireName(m_trackableHandle);
        }
    }
}
///@endcond