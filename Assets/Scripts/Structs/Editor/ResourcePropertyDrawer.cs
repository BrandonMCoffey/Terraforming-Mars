#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Scripts.Structs.Editor
{
    [CustomPropertyDrawer(typeof(Resource))]
    public class ResourcePropertyDrawer : PropertyDrawer
    {
        private const float BottomSpacing = 2;

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            pos.height -= BottomSpacing;
            label = EditorGUI.BeginProperty(pos, label, prop);
            var contentRect = EditorGUI.PrefixLabel(pos, GUIUtility.GetControlID(FocusType.Passive), label);
            var labels = new[] {new GUIContent("Amount"), new GUIContent("Level")};
            var properties = new[] {prop.FindPropertyRelative("Amount"), prop.FindPropertyRelative("Level")};
            EditorGUI.BeginDisabledGroup(true);
            PropertyFieldHelper.DrawMultiplePropertyFields(contentRect, labels, properties);
            EditorGUI.EndDisabledGroup();

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + BottomSpacing;
        }
    }
}
#endif