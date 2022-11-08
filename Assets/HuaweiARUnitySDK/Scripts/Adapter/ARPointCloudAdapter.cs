//-----------------------------------------------------------------------
// <copyright file="PointCloudApi.cs" company="Google">
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
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using System.Collections.Generic;

    internal class ARPointCloudAdapter
    {
        private NDKSession m_ndkSession;
        private float[] m_CachedVector = new float[4];
        public ARPointCloudAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public long GetTimestamp(IntPtr pointCloudHandle)
        {
            long timestamp = 0;
            NDKAPI.HwArPointCloud_getTimestamp(m_ndkSession.SessionHandle, pointCloudHandle, ref timestamp);
            return timestamp;
        }
        public int GetNumberOfPoints(IntPtr pointCloudHandle)
        {
            int pointCount = 0;
            NDKAPI.HwArPointCloud_getNumberOfPoints(m_ndkSession.SessionHandle, pointCloudHandle, ref pointCount);
            return pointCount;
        }
        public Vector4 GetPoint(IntPtr pointCloudHandle, int index)
        {
            IntPtr pointCloudNativeHandle = IntPtr.Zero;
            NDKAPI.HwArPointCloud_getData(m_ndkSession.SessionHandle, pointCloudHandle, ref pointCloudNativeHandle);
            IntPtr pointHandle = new IntPtr(pointCloudNativeHandle.ToInt64() +
                                            (Marshal.SizeOf(typeof(Vector4)) * index));
            Marshal.Copy(pointHandle, m_CachedVector, 0, 4);

            return new Vector4(m_CachedVector[0], m_CachedVector[1], -m_CachedVector[2], m_CachedVector[3]);
        }

        public void CopyPoints(IntPtr pointCloudHandle, List<Vector4> points)
        {
            points.Clear();
            int pointCloudSize = GetNumberOfPoints(pointCloudHandle);
            List<Vector4> tmpPoints = _GetPointsInPointCloud(pointCloudHandle);

            for (int i = 0; i < pointCloudSize; ++i)
            {
                points.Add(new Vector4(tmpPoints[i].x, tmpPoints[i].y,-tmpPoints[i].z, tmpPoints[i].w));
            }
        }
        private List<Vector4> _GetPointsInPointCloud(IntPtr pointCloudHandle)
        {
            IntPtr pointsArrayNativeHandle = IntPtr.Zero;
            int pointCloudSize = GetNumberOfPoints(pointCloudHandle);
            NDKAPI.HwArPointCloud_getData(m_ndkSession.SessionHandle, pointCloudHandle, ref pointsArrayNativeHandle);
            List<Vector4> tmpPoints = new List<Vector4>();
            MarshalingHelper.AppendUnManagedArray2List<Vector4>(pointsArrayNativeHandle, pointCloudSize, tmpPoints);
            return tmpPoints;
        }
        public void CopyPoints(IntPtr pointCloudHandle, List<Vector3> points)
        {
            points.Clear();
            int pointCloudSize = GetNumberOfPoints(pointCloudHandle);
            List<Vector4> tmpPoints = _GetPointsInPointCloud(pointCloudHandle);
            
            for (int i = 0; i < pointCloudSize; ++i)
            {
                points.Add( new Vector3(tmpPoints[i].x, tmpPoints[i].y,-tmpPoints[i].z));
            }
        }

        public void Release(IntPtr pointCloudHandle)
        {
            NDKAPI.HwArPointCloud_release(pointCloudHandle);
        }


        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPointCloud_getNumberOfPoints(IntPtr sessionHandle, IntPtr pointCloudHandle,
                                      ref int outNumberOfPoints);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPointCloud_getData(IntPtr sessionHandle,IntPtr pointCloudHandle,
                            ref IntPtr pointCloudData);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPointCloud_getTimestamp(IntPtr sessionHandle, IntPtr pointCloudHandle,
                                 ref long timestamp);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPointCloud_release(IntPtr pointCloudHandle);
        }
    }
}
