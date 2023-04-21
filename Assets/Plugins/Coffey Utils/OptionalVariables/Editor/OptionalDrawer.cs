#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Utility.OptionalVariables.Editor
{
    [CustomPropertyDrawer(typeof(Optional<>))]
    public class OptionalDrawer : PropertyDrawer
    {
        private const float ButtonOffset = 24f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var enabledProperty = property.FindPropertyRelative("_enabled");
            var valueProperty = property.FindPropertyRelative("_value");

            EditorGUI.BeginProperty(position, label, property);

            position.width -= ButtonOffset;
            EditorGUI.BeginDisabledGroup(!enabledProperty.boolValue);
            EditorGUI.PropertyField(position, valueProperty, label);
            EditorGUI.EndDisabledGroup();

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            position.x += position.width + ButtonOffset;
            position.width = position.height = EditorGUI.GetPropertyHeight(enabledProperty);
            position.x -= position.width;
            EditorGUI.PropertyField(position, enabledProperty, GUIContent.none);
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative("_value");
            return EditorGUI.GetPropertyHeight(valueProperty);
        }
    }
}
#endif