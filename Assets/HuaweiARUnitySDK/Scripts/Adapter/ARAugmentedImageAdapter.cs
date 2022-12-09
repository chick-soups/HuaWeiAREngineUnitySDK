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

namespace HuaweiARInternal
{
    using System.Runtime.InteropServices;
    using UnityEngine;
    using System;
    internal class ARAugmentedImageAdapter
    {
        private NDKSession m_ndkSession;

        public ARAugmentedImageAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public Pose GetCenterPose(IntPtr AugImgHandle)
        {
            IntPtr poseHandle = m_ndkSession.PoseAdapter.Create();
            NDKAPI.HwArAugmentedImage_getCenterPose(m_ndkSession.SessionHandle, AugImgHandle, poseHandle);
            Pose pose = m_ndkSession.PoseAdapter.GetPoseValue(poseHandle);
            m_ndkSession.PoseAdapter.Destroy(poseHandle);
            return pose;
        }

        public float GetExtentX(IntPtr AugImgHandle)
        {
            float outExtentX = 0;
            NDKAPI.HwArAugmentedImage_getExtentX(m_ndkSession.SessionHandle, AugImgHandle, ref outExtentX);
            return outExtentX;
        }

        public float GetExtentZ(IntPtr AugImgHandle)
        {
            float outExtentZ = 0;
            NDKAPI.HwArAugmentedImage_getExtentZ(m_ndkSession.SessionHandle, AugImgHandle, ref outExtentZ);
            return outExtentZ;
        }

        public int GetDataBaseIndex(IntPtr AugImgHandle)
        {
            int outDatabaseIndex = 0;
            NDKAPI.HwArAugmentedImage_getIndex(m_ndkSession.SessionHandle, AugImgHandle, ref outDatabaseIndex);
            return outDatabaseIndex;
        }

        public string AcquireName(IntPtr AugImgHandle)
        {
            string ImgName = null;
            NDKAPI.HwArAugmentedImage_acquireName(m_ndkSession.SessionHandle, AugImgHandle, ref ImgName);
            ARDebug.LogInfo("AcquireName:{0}", ImgName);
            return ImgName;
        }
        public string GetCloudImageId(IntPtr AugImgHandle){
            string cloudImageId=string.Empty;
            NDKAPI.HwArAugmentedImage_getCloudImageId(m_ndkSession.SessionHandle,AugImgHandle,ref cloudImageId);
            return cloudImageId;
        }

          public string GetCloudImageMetadata(IntPtr AugImgHandle){
            string metadata=string.Empty;
            NDKAPI.HwArAugmentedImage_getCloudImageMetadata(m_ndkSession.SessionHandle,AugImgHandle,ref metadata);
            return metadata;
        }




        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAugmentedImage_getCenterPose(IntPtr sessionHandle, IntPtr augImgHandle, IntPtr poseHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAugmentedImage_getExtentX(IntPtr sessionHandle, IntPtr augImgHandle, ref float outExtentX);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAugmentedImage_getExtentZ(IntPtr sessionHandle, IntPtr augImgHandle, ref float outExtentZ);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAugmentedImage_getIndex(IntPtr sessionHandle, IntPtr augImgHandle, ref int outDatabaseIndex);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAugmentedImage_acquireName(IntPtr sessionHandle, IntPtr augImgHandle, ref string outName);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAugmentedImage_getCloudImageId(IntPtr sessionHandle, IntPtr augImgHandle, ref string outCloudImageId);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAugmentedImage_getCloudImageMetadata(IntPtr sessionHandle, IntPtr augImgHandle, ref string outCloudImageMetadata);
        }
    }
}
