using UnityEditor;
using UnityEngine;

namespace Scripts.Structs.Editor
{
    [CustomPropertyDrawer(typeof(PatentCost))]
    public class PatentCostDrawer : PropertyDrawer
    {
        private const float Padding = 5;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var enabledProperty = property.FindPropertyRelative("Active");
            var typeProperty = property.FindPropertyRelative("Resource");
            var amountProperty = property.FindPropertyRelative("Amount");

            EditorGUI.BeginProperty(position, label, property);


            float enabledHeight = EditorGUI.GetPropertyHeight(enabledProperty);
            Rect enabledRect = new Rect(position.x, position.y, enabledHeight, enabledHeight);
            EditorGUI.PropertyField(enabledRect, enabledProperty, GUIContent.none);

            EditorGUI.BeginDisabledGroup(!enabledProperty.boolValue);
            float width = (position.width - enabledHeight - Padding) * 0.5f;

            float amountStart = enabledRect.x + enabledRect.width;
            float amountHeight = EditorGUI.GetPropertyHeight(amountProperty);
            Rect amountRect = new Rect(amountStart, position.y, width, amountHeight);
            EditorGUI.PropertyField(amountRect, amountProperty, GUIContent.none);

            float typeStart = amountRect.x + amountRect.width + Padding;
            float typeHeight = EditorGUI.GetPropertyHeight(typeProperty);
            Rect typeRect = new Rect(typeStart, position.y, width, typeHeight);
            EditorGUI.PropertyField(typeRect, typeProperty, GUIContent.none);

            EditorGUI.EndDisabledGroup();
            EditorGUI.EndProperty();
        }
    }
}