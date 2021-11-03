#if UNITY_EDITOR
using GridTool.DataScripts.GUI;
using UnityEditor;
using UnityEngine;

namespace GridTool.DataScripts.Editor
{
    [CustomEditor(typeof(ObjectData), true)]
    public class ObjectDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Separator();
            if (GUILayout.Button("Edit art", GUILayout.Height(40))) {
                ObjectDesignerWindow.OpenWindow((ObjectData)target);
            }
        }
    }
}

#endif