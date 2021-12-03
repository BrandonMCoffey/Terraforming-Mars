using UnityEditor;
using UnityEngine;

namespace Scripts.Structs.Editor
{
    [CustomPropertyDrawer(typeof(PatentConstraint))]
    public class PatentConstraintDrawer : PropertyDrawer
    {
        private const float Padding = 5;
        private const float AmountWidth = 50;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var enabledProperty = property.FindPropertyRelative("Active");
            var typeProperty = property.FindPropertyRelative("Type");
            var comparisonProperty = property.FindPropertyRelative("Comparison");
            var amountProperty = property.FindPropertyRelative("Amount");

            EditorGUI.BeginProperty(position, label, property);


            float enabledHeight = EditorGUI.GetPropertyHeight(enabledProperty);
            Rect enabledRect = new Rect(position.x, position.y, enabledHeight, enabledHeight);
            EditorGUI.PropertyField(enabledRect, enabledProperty, GUIContent.none);

            EditorGUI.BeginDisabledGroup(!enabledProperty.boolValue);
            float enumWidth = (position.width - enabledHeight - AmountWidth - Padding * 2) / 2;

            float typeStart = enabledRect.x + enabledRect.width;
            float typeHeight = EditorGUI.GetPropertyHeight(typeProperty);
            Rect typeRect = new Rect(typeStart, position.y, enumWidth, typeHeight);
            EditorGUI.PropertyField(typeRect, typeProperty, GUIContent.none);

            float comparisonStart = typeRect.x + typeRect.width + Padding;
            float comparisonHeight = EditorGUI.GetPropertyHeight(comparisonProperty);
            Rect comparisonRect = new Rect(comparisonStart, position.y, enumWidth, comparisonHeight);
            EditorGUI.PropertyField(comparisonRect, comparisonProperty, GUIContent.none);

            float amountStart = comparisonRect.x + comparisonRect.width + Padding;
            float amountHeight = EditorGUI.GetPropertyHeight(amountProperty);
            Rect amountRect = new Rect(amountStart, position.y, AmountWidth, amountHeight);
            EditorGUI.PropertyField(amountRect, amountProperty, GUIContent.none);

            EditorGUI.EndDisabledGroup();
            EditorGUI.EndProperty();
        }
    }
}