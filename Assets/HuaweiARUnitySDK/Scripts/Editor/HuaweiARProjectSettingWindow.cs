namespace HuaweiARInternal
{
    using UnityEditor;
    using UnityEngine;
    internal class HuaweiARProjectSettingWindow : EditorWindow
    {
        [MenuItem("Edit/Project Settings/HuaweiAR")]
        private static void ShowProjectSettingsWindow()
        {
            HuaweiARProjectSettings.Instance.LoadSettings();

            Rect rect = new Rect(500, 300, 400, 150);
            HuaweiARProjectSettingWindow settingWindow = GetWindowWithRect<HuaweiARProjectSettingWindow>(
                rect);
            settingWindow.titleContent = new GUIContent("Huawei AR");
            settingWindow.Show();
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;
            style.stretchWidth = true;
            style.fontSize = 20;
            style.fixedHeight = 20;
            EditorGUILayout.LabelField("HuaweiAR Project Settings", style);
            GUILayout.Space(10);
            HuaweiARProjectSettings.Instance.IsHuaweiARRequired =
                EditorGUILayout.Toggle("Huawei AR Required", HuaweiARProjectSettings.Instance.IsHuaweiARRequired);
            GUILayout.Space(10);

            if (GUI.changed)
            {
                HuaweiARProjectSettings.Instance.SaveSettings();
            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Close", GUILayout.Width(60), GUILayout.Height(20)))
            {
                Close();
            }

            EditorGUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
    }
}