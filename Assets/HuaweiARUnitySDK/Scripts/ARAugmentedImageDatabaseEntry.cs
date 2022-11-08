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

#if UNITY_EDITOR
    using UnityEditor;
#endif

    [Serializable]
    public struct ARAugmentedImageDatabaseEntry
    {
        public string Name;
        public float Width;
        public string Quality;
        public string TextureGUID;
#if UNITY_EDITOR
        public ARAugmentedImageDatabaseEntry(string name, Texture2D texture, float width)
        {
            Name = name;
            TextureGUID = "";
            Width = width;
            Quality = "";
            Texture = texture;
        }


        public ARAugmentedImageDatabaseEntry(string name, Texture2D texture)
        {
            Name = name;
            TextureGUID = "";
            Width = 0;
            Quality = "";
            Texture = texture;
        }

        public ARAugmentedImageDatabaseEntry(Texture2D texture)
        {
            Name = "Unnamed";
            TextureGUID = "";
            Width = 0;
            Quality = "";
            Texture = texture;
        }

        public Texture2D Texture
        {
            get
            {
                return AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(TextureGUID));
            }
            set
            {
                TextureGUID = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(value));
            }
        }
#endif
    }
}
///@endcond