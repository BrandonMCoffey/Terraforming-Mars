#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Utility.Inspector.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyAttributeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            UnityEngine.GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            UnityEngine.GUI.enabled = true;
        }
    }
}

#endif