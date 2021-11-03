#if UNITY_EDITOR
using GridTool.DataScripts;
using UnityEditor;
using UnityEngine;

namespace Assets.GridTool.DataScripts.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyAttributeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
        }
    }
}

#endif