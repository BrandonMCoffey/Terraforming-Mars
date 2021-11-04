#if UNITY_EDITOR
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

            EditorGUILayout.Separator();
            if (GUILayout.Button("Edit Unit", GUILayout.Height(40))) {
                UnitDesignerWindow.OpenWindow((UnitData)target);
            }
        }
    }
}

#endif