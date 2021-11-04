#if UNITY_EDITOR
using System.IO;
using GridTool.DataScripts.GUI;
using UnityEditor;
using UnityEngine;

namespace GridTool.DataScripts.Editor
{
    [CustomEditor(typeof(UnitData), true)]
    public class UnitDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            UnitData data = (UnitData)target;

            // String Buttons
            EditorGUILayout.Separator();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Save Changes from String", GUILayout.Height(20))) {
                data.ReadFromString();
            }
            if (GUILayout.Button("Reset String", GUILayout.Height(20))) {
                data.SaveUnitOptions();
            }
            if (GUILayout.Button("Export to CSV", GUILayout.Height(20))) {
                var content = data.UnitString;
                var folder = EditorUtility.SaveFilePanel("Save " + data.Name + " as .CSV", Application.streamingAssetsPath, data.Name, "csv");
                using (var writer = new StreamWriter(folder, false)) {
                    writer.Write(content);
                }
                AssetDatabase.Refresh();
            }
            GUILayout.EndHorizontal();

            // Preview Unit
            EditorGUILayout.Separator();
            GUILayout.BeginHorizontal();
            data.CheckValid();
            var centeredStyle = UnityEngine.GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.MiddleCenter;
            float width = (Screen.width - 20 - data.UnitOptionLength * 4f) / data.UnitOptionLength;
            for (int w = 0; w < data.UnitOptionLength; w++) {
                GUILayout.BeginVertical();
                for (int h = 0; h < data.UnitOptionLength; h++) {
                    GUILayout.Label(data.UnitOptions[w, h].GetReadableString(), centeredStyle, GUILayout.Width(width));
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            EditorGUILayout.Separator();

            EditorGUILayout.Separator();
            if (GUILayout.Button("Edit Unit", GUILayout.Height(40))) {
                UnitDesignerWindow.OpenWindow((UnitData)target);
            }
        }
    }
}

#endif