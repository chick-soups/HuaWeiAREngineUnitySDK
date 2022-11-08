//-----------------------------------------------------------------------
// <copyright file="TrackableManager.cs" company="Google">
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
    using System.Collections.Generic;
    using UnityEngine;
    using HuaweiARUnitySDK;

    internal class ARTrackableManager
    {
        private Dictionary<IntPtr, ARTrackable> m_trackableDict =
            new Dictionary<IntPtr, ARTrackable>(new IntPtrComparer());
        private int m_lastUpdateFrame = -1;
        private NDKSession m_ndkSession;

        private List<ARTrackable> m_newTrackables = new List<ARTrackable>();

        private List<ARTrackable> m_allTrackables = new List<ARTrackable>();

        private List<ARTrackable> m_updatedTrackables = new List<ARTrackable>();

        private HashSet<ARTrackable> m_oldTrackables = new HashSet<ARTrackable>();

        public ARTrackableManager(NDKSession session)
        {
            m_ndkSession = session;
        }

        public ARTrackable ARTrackableFactory(IntPtr nativeHandle, bool isCreate = false)
        {
            if (nativeHandle == IntPtr.Zero)
            {
                return null;
            }
            ARTrackable result;
            if (m_trackableDict.TryGetValue(nativeHandle, out result))
            {
                m_ndkSession.TrackableAdapter.Release(nativeHandle);
                return result;
            }
            if (isCreate)
            {
                NDKARTrackableType ndkTrackableType = m_ndkSession.TrackableAdapter.GetType(nativeHandle);
                ARDebug.LogInfo("trackable type {0}", ndkTrackableType.ToString());
                switch (ndkTrackableType)
                {
                    case NDKARTrackableType.Plane:
                        result = new ARPlane(nativeHandle, m_ndkSession);
                        break;
                    case NDKARTrackableType.Point:
                        result = new ARPoint(nativeHandle, m_ndkSession);
                        break;
                    case NDKARTrackableType.Body:
                        result = new ARBody(nativeHandle, m_ndkSession);
                        break;
                    case NDKARTrackableType.Hand:
                        result = new ARHand(nativeHandle, m_ndkSession);
                        break;
                    case NDKARTrackableType.Face:
                        result = new ARFace(nativeHandle, m_ndkSession);
                        break;
                    case NDKARTrackableType.AugmentedImage:
                        result = new ARAugmentedImage(nativeHandle, m_ndkSession);
                        break;
                    //todo add more trackable
                    default:
                        m_ndkSession.TrackableAdapter.Release(nativeHandle);
                        throw new NotImplementedException("ARTrackableFactory: no constructor for requested type");
                }

                m_trackableDict.Add(nativeHandle, result);
                return result;
            }
            return null;
        }

        public void GetTrackables<T>(List<T> trackables, ARTrackableQueryFilter filter) where T : ARTrackable
        {
            if (m_lastUpdateFrame < Time.frameCount)
            {
                // Get trackables updated this frame.
                m_ndkSession.FrameAdapter.GetUpdatedTrackables(m_updatedTrackables);

                // Get all the trackables in the session.
                m_ndkSession.SessionAdapter.GetAllTrackables(m_allTrackables);

                ARDebug.LogInfo("m_updatedTrackables {0} m_allTrackables {1}", m_updatedTrackables.Count, m_allTrackables.Count);
                // Find trackables that are not in the hashset (new).
                m_newTrackables.Clear();
                for (int i = 0; i < m_allTrackables.Count; i++)
                {
                    ARTrackable trackable = m_allTrackables[i];
                    if (!m_oldTrackables.Contains(trackable))
                    {
                        m_newTrackables.Add(trackable);
                        m_oldTrackables.Add(trackable);
                    }
                }

                m_lastUpdateFrame = Time.frameCount;
            }

            trackables.Clear();

            if (filter == ARTrackableQueryFilter.ALL)
            {
                for (int i = 0; i < m_allTrackables.Count; i++)
                {
                    _SafeAdd<T>(m_allTrackables[i], trackables);
                }
            }
            else if (filter == ARTrackableQueryFilter.NEW)
            {
                for (int i = 0; i < m_newTrackables.Count; i++)
                {
                    _SafeAdd<T>(m_newTrackables[i], trackables);
                }
            }
            else if (filter == ARTrackableQueryFilter.UPDATED)
            {
                for (int i = 0; i < m_updatedTrackables.Count; i++)
                {
                    _SafeAdd<T>(m_updatedTrackables[i], trackables);
                }
            }
        }
        private void _SafeAdd<T>(ARTrackable trackable, List<T> trackables) where T : ARTrackable
        {
            if (trackable is T)
            {
                trackables.Add(trackable as T);
            }
        }
    }
}
