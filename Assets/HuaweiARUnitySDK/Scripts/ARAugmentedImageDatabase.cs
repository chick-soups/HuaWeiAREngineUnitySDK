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
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using UnityEngine;
#if UNITY_EDITOR
    using System.IO;
    using UnityEditor;
#endif
    public class ARAugmentedImageDatabase : ScriptableObject
    {
        [SerializeField]
        private List<ARAugmentedImageDatabaseEntry> m_Images = new List<ARAugmentedImageDatabaseEntry>();
        [SuppressMessage("UnityRules.UnityStyleRules", "CS0169:FieldIsNeverUsedIssue",
         Justification = "Used in editor.")]
        [SerializeField]
        private byte[] m_RawData = null;
        [SerializeField]
        private bool m_IsRawDataDirty = true;
        [SerializeField]
        private string m_CliVersion = string.Empty;

        public int Count
        {
            get
            {
                return m_Images.Count;
            }
        }

        public ARAugmentedImageDatabaseEntry this[int index]
        {
            get
            {
                return m_Images[index];
            }
#if UNITY_EDITOR
            set
            {
                var oldValue = m_Images[index];
                m_Images[index] = value;

                if (oldValue.TextureGUID != m_Images[index].TextureGUID
                    || oldValue.Name != m_Images[index].Name
                    || oldValue.Width != m_Images[index].Width)
                {
                    m_IsRawDataDirty = true;
                }

                EditorUtility.SetDirty(this);
            }
#endif
        }

#if UNITY_EDITOR
        public void Add(ARAugmentedImageDatabaseEntry entry)
        {
            m_Images.Add(entry);
            m_IsRawDataDirty = true;
            EditorUtility.SetDirty(this);
        }

        public void RemoveAt(int index)
        {
            m_Images.RemoveAt(index);
            m_IsRawDataDirty = true;
            EditorUtility.SetDirty(this);
        }

        public bool IsBuildNeeded()
        {
            return m_IsRawDataDirty;
        }

        private void BuildHWDatabase(string cliBinaryPath, out string error)
        {
            string output;
            error = null;
            var tempDirectoryPath = FileUtil.GetUniqueTempPathInProject();
            Directory.CreateDirectory(tempDirectoryPath);
            var rawDatabasePath = Path.Combine(tempDirectoryPath, "HwDatabase.bin");
            for (int i = 0; i < m_Images.Count; i++)
            {
                var imagePath = AssetDatabase.GetAssetPath(m_Images[i].Texture);
                ShellHelper.RunCommand(cliBinaryPath,
                string.Format("add-img --input_image_path {0} --input_database_path {1}",
                              imagePath, rawDatabasePath), out output, out error);
            }
            if (!string.IsNullOrEmpty(error))
            {
                ARDebug.LogInfo("RunCommand err" + error);
                return;
            }

            m_RawData = File.ReadAllBytes(rawDatabasePath);
        }

        public void BuildIfNeeded(out string error)
        {
            error = "";
            if (!m_IsRawDataDirty)
            {
                return;
            }

            string cliBinaryPath;
            if (!FindCliBinaryPath(out cliBinaryPath))
            {
                return;
            }

            BuildHWDatabase(cliBinaryPath, out error);


            m_IsRawDataDirty = false;
            EditorUtility.SetDirty(this);

            // Force a save to make certain build process will get updated asset.
            AssetDatabase.SaveAssets();

            const int BYTES_IN_KBYTE = 1024;

            // TODO:: Remove this log when all errors/warnings are moved to stderr for CLI tool.
            ARDebug.LogInfo("Built AugmentedImageDatabase '{0}' ({1} Images, {2} KBytes)", name, m_Images.Count,
                m_RawData.Length / BYTES_IN_KBYTE);
        }

        public List<ARAugmentedImageDatabaseEntry> GetDirtyQualityEntries()
        {
            var dirtyEntries = new List<ARAugmentedImageDatabaseEntry>();
            string cliBinaryPath;
            if (!FindCliBinaryPath(out cliBinaryPath))
            {
                return dirtyEntries;
            }

            string currentCliVersion;
            {
                string error;
#if !UNITY_EDITOR_WIN
                string output;
                ShellHelper.RunCommand("chmod", "+x " + cliBinaryPath, out output, out error);
                if (!string.IsNullOrEmpty(error))
                {
                    Debug.LogWarning(error);
                    return dirtyEntries;
                }
#endif
                ShellHelper.RunCommand(cliBinaryPath, "version", out currentCliVersion, out error);
                if (!string.IsNullOrEmpty(error))
                {
                    ARDebug.LogInfo(error);
                    return dirtyEntries;
                }
            }

            bool cliUpdated = m_CliVersion != currentCliVersion;
            // When CLI is updated, mark all entries dirty.
            if (cliUpdated)
            {
                for (int i = 0; i < m_Images.Count; ++i)
                {
                    ARAugmentedImageDatabaseEntry updatedImage = m_Images[i];
                    updatedImage.Quality = string.Empty;
                    m_Images[i] = updatedImage;
                }

                m_CliVersion = currentCliVersion;
                EditorUtility.SetDirty(this);
            }

            for (int i = 0; i < m_Images.Count; ++i)
            {
                if (!string.IsNullOrEmpty(m_Images[i].Quality))
                {
                    continue;
                }

                dirtyEntries.Add(m_Images[i]);
            }

            return dirtyEntries;
        }

        public static bool FindCliBinaryPath(out string path)
        {
            string binaryName = UtilConstants.HWAugmentedImageCliBinaryName;
            string[] cliBinaryGuid = AssetDatabase.FindAssets(binaryName);
            if (cliBinaryGuid.Length == 0)
            {
                ARDebug.LogInfo("Could not find required tool for building AugmentedImageDatabase: {0}. " +
                    "Was it removed from the SDK?", binaryName);
                path = string.Empty;
                return false;
            }

            // Remove the '/Assets' from the project path since it will be added in the path below.
            string projectPath = Application.dataPath.Substring(0, Application.dataPath.Length - 6);
            path = Path.Combine(projectPath, AssetDatabase.GUIDToAssetPath(cliBinaryGuid[0]));
            return !string.IsNullOrEmpty(path);
        }
#endif
        internal byte[] GetRawData()
        {
            return m_RawData;
        }
    }
}
///@endcond