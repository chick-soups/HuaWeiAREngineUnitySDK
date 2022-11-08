//-----------------------------------------------------------------------
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
    using HuaweiARUnitySDK;
    using System.Collections.Generic;
    internal class ARCameraMetadataAdapter
    {
        private NDKSession m_ndkSession;

        public ARCameraMetadataAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }
        public void Release(IntPtr metadataHandle)
        {
            NDKAPI.HwArImageMetadata_release(metadataHandle);
        }

        public void GetValues(IntPtr cameraMetadataHandle,
            ARCameraMetadataTag tag, List<ARCameraMetadataValue> resultList)
        {
            IntPtr ndkMetadataHandle = IntPtr.Zero;
            NDKAPI.HwArImageMetadata_getNdkCameraMetadata(m_ndkSession.SessionHandle,
                cameraMetadataHandle, ref ndkMetadataHandle);

            resultList.Clear();
            NdkCameraMetadata entry = new NdkCameraMetadata();
            NdkCameraStatus status = NDKAPI.ACameraMetadata_getConstEntry(ndkMetadataHandle, tag, ref entry);
            if (status != NdkCameraStatus.Ok)
            {
                ARDebug.LogError("ACameraMetadata_getConstEntry error with native camera error code: {0}",
                    status);
                return ;
            }

            for (int i = 0; i < entry.Count; i++)
            {
                switch (entry.Type)
                {
                    case NdkCameraMetadataType.Byte:
                        sbyte byteValue = MarshalingHelper.GetValueOfUnmanagedArrayElement<sbyte>(entry.Value, i);
                        resultList.Add(new ARCameraMetadataValue(byteValue));
                        break;
                    case NdkCameraMetadataType.Int32:
                        int intValue = MarshalingHelper.GetValueOfUnmanagedArrayElement<int>(entry.Value, i);
                        resultList.Add(new ARCameraMetadataValue(intValue));
                        break;
                    case NdkCameraMetadataType.Float:
                        float floatValue = MarshalingHelper.GetValueOfUnmanagedArrayElement<float>(entry.Value, i);
                        resultList.Add(new ARCameraMetadataValue(floatValue));
                        break;
                    case NdkCameraMetadataType.Int64:
                        long longValue = MarshalingHelper.GetValueOfUnmanagedArrayElement<long>(entry.Value, i);
                        resultList.Add(new ARCameraMetadataValue(longValue));
                        break;
                    case NdkCameraMetadataType.Double:
                        double doubleValue = MarshalingHelper.GetValueOfUnmanagedArrayElement<double>(entry.Value, i);
                        resultList.Add(new ARCameraMetadataValue(doubleValue));
                        break;
                    case NdkCameraMetadataType.Rational:
                        ARCameraMetadataRational rationalValue = MarshalingHelper.GetValueOfUnmanagedArrayElement<
                            ARCameraMetadataRational>(entry.Value, i);
                        resultList.Add(new ARCameraMetadataValue(rationalValue));
                        break;
                    default:
                        return ;
                }
            }
        }


        public void GetAllCameraMetadataTags(IntPtr cameraMetadataHandle, List<ARCameraMetadataTag> resultList)
        {
            IntPtr metadataHandle = IntPtr.Zero;
            NDKAPI.HwArImageMetadata_getNdkCameraMetadata(m_ndkSession.SessionHandle,
                cameraMetadataHandle, ref metadataHandle);

            IntPtr tagsHandle = IntPtr.Zero;
            int tagsCount = 0;
            NdkCameraStatus status = NDKAPI.ACameraMetadata_getAllTags(metadataHandle, ref tagsCount, ref tagsHandle);
            if (status != NdkCameraStatus.Ok)
            {
                ARDebug.LogError("ACameraMetadata_getAllTags error with native camera error code: {0}",
                    status);
                return ;
            }

            for (int i = 0; i < tagsCount; i++)
            {
                resultList.Add((ARCameraMetadataTag)MarshalingHelper.GetValueOfUnmanagedArrayElement<int>(tagsHandle,i));
            }
        }


        private struct NDKAPI
        {
            
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArImageMetadata_release(IntPtr metadataHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArImageMetadata_getNdkCameraMetadata(IntPtr sessionHandle, IntPtr imageMetadataHandle,
                ref IntPtr outNDKMetadataHandle);

            
            [DllImport(AdapterConstants.NDKCameraApi)]
            public static extern NdkCameraStatus ACameraMetadata_getConstEntry(IntPtr ndkCameraMetadata,
                ARCameraMetadataTag tag, ref NdkCameraMetadata entry);
            [DllImport(AdapterConstants.NDKCameraApi)]
            public static extern NdkCameraStatus ACameraMetadata_getAllTags(IntPtr ndkCameraMetadata,
                ref int numEntries, ref IntPtr tags);
        }
    }
}
