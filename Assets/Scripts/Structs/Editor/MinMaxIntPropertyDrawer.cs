#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Scripts.Structs.Editor
{
    [CustomPropertyDrawer(typeof(PlanetStatusLevel))]
    public class MinMaxIntPropertyDrawer : PropertyDrawer
    {
        private const float BottomSpacing = 2;

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            pos.height -= BottomSpacing;
            label = EditorGUI.BeginProperty(pos, label, prop);
            var contentRect = EditorGUI.PrefixLabel(pos, GUIUtility.GetControlID(FocusType.Passive), label);
            var labels = new[] {new GUIContent("Min"), new GUIContent("Max"), new GUIContent("Step")};
            var properties = new[] {prop.FindPropertyRelative("MinValue"), prop.FindPropertyRelative("MaxValue"), prop.FindPropertyRelative("StepValue")};
            PropertyFieldHelper.DrawMultiplePropertyFields(contentRect, labels, properties);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + BottomSpacing;
        }
    }
}
#endif