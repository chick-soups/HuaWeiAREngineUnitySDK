//-----------------------------------------------------------------------
// <copyright file="PoseApi.cs" company="Google">
//
// Copyright 2017 Google Inc. All Rights Reserved.
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
namespace HuaweiARInternal
{
    using System.Runtime.InteropServices;
    using UnityEngine;
    using System;
    internal class ARPoseAdapter
    {
        private NDKSession m_ndkSession;

        public ARPoseAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public IntPtr Create()
        {
            return Create(Pose.identity);
        }
        public IntPtr Create(Pose pose)
        {
            NDKPoseData nDKPoseData = new NDKPoseData(pose);
            IntPtr poseHandle = IntPtr.Zero;
            NDKAPI.HwArPose_create(m_ndkSession.SessionHandle,ref nDKPoseData,ref poseHandle);
            return poseHandle;
        }

        public void Destroy(IntPtr poseHandle)
        {
            NDKAPI.HwArPose_destroy(poseHandle);
        }

        public Pose GetPoseValue(IntPtr poseHandle)
        {
            NDKPoseData poseValue = new NDKPoseData(Pose.identity);
            NDKAPI.HwArPose_getPoseRaw(m_ndkSession.SessionHandle, poseHandle, ref poseValue);
            return poseValue.ToUnityPose();
        }

        private struct NDKAPI {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPose_create(IntPtr sessionHandle,
                     ref NDKPoseData poseRaw, ref IntPtr outPoseHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPose_destroy(IntPtr poseHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPose_getPoseRaw(IntPtr sessionHandle,IntPtr  poseHandle,
                         ref NDKPoseData outPoseRaw);
        }
    }
}
