#if UNITY_EDITOR
using System.IO;
using GridTool.DataScripts;
using GridTool.DataScripts.GUI;
using UnityEditor;
using UnityEngine;

namespace Assets.GridTool.DataScripts.Editor
{
    [CustomEditor(typeof(LevelData), true)]
    public class LevelDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            LevelData data = (LevelData)target;

            // String Buttons
            EditorGUILayout.Separator();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Save Changes from String", GUILayout.Height(20))) {
                data.ReadFromString();
            }
            if (GUILayout.Button("Reset String", GUILayout.Height(20))) {
                data.SaveLevel();
            }
            if (GUILayout.Button("Export to CSV", GUILayout.Height(20))) {
                var content = data.LevelString;
                var folder = EditorUtility.SaveFilePanel("Save " + data.Name + " as .CSV", Application.streamingAssetsPath, data.Name, "csv");
                using (var writer = new StreamWriter(folder, false)) {
                    writer.Write(content);
                }
                AssetDatabase.Refresh();
            }
            GUILayout.EndHorizontal();

            // Preview Level
            EditorGUILayout.Separator();
            GUILayout.BeginHorizontal();
            data.CheckValid();
            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.MiddleCenter;
            float width = (Screen.width - 20 - data.Width * 4f) / data.Width;
            for (int w = 0; w < data.Width; w++) {
                GUILayout.BeginVertical();
                for (int h = 0; h < data.Height; h++) {
                    GUILayout.Label(data.Get(w, h).DisplayName, centeredStyle, GUILayout.Width(width));
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            EditorGUILayout.Separator();

            // Edit Button
            EditorGUILayout.Separator();
            if (GUILayout.Button("Edit Object", GUILayout.Height(40))) {
                LevelDesignerWindow.OpenWindow(data);
            }
        }
    }
}

#endif