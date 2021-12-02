using UnityEditor;
using UnityEngine;

namespace Scripts.Data.Structs.Editor
{
    [CustomPropertyDrawer(typeof(PatentEffect))]
    public class PatentEffectDrawer : PropertyDrawer
    {
        private const float Padding = 5;
        private const float AmountWidth = 50;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var enabledProperty = property.FindPropertyRelative("Active");
            var typeProperty = property.FindPropertyRelative("Type");
            var amountProperty = property.FindPropertyRelative("Amount");
            var resourceProperty = property.FindPropertyRelative("Resource");
            var tileProperty = property.FindPropertyRelative("Tile");
            var statusProperty = property.FindPropertyRelative("Status");

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

            float amountStart = typeRect.x + typeRect.width + Padding;
            float amountHeight = EditorGUI.GetPropertyHeight(amountProperty);
            Rect amountRect = new Rect(amountStart, position.y, AmountWidth, amountHeight);
            EditorGUI.PropertyField(amountRect, amountProperty, GUIContent.none);

            float effectStart = amountRect.x + amountRect.width + Padding;
            switch (typeProperty.enumValueIndex) {
                case 2: {
                    float tileHeight = EditorGUI.GetPropertyHeight(tileProperty);
                    Rect tileRect = new Rect(effectStart, position.y, enumWidth, tileHeight);
                    EditorGUI.PropertyField(tileRect, tileProperty, GUIContent.none);
                    break;
                }
                case 3: {
                    float statusHeight = EditorGUI.GetPropertyHeight(statusProperty);
                    Rect statusRect = new Rect(effectStart, position.y, enumWidth, statusHeight);
                    EditorGUI.PropertyField(statusRect, statusProperty, GUIContent.none);
                    break;
                }
                default: {
                    float resourceHeight = EditorGUI.GetPropertyHeight(resourceProperty);
                    Rect resourceRect = new Rect(effectStart, position.y, enumWidth, resourceHeight);
                    EditorGUI.PropertyField(resourceRect, resourceProperty, GUIContent.none);
                    break;
                }
            }

            EditorGUI.EndDisabledGroup();
            EditorGUI.EndProperty();
        }
    }
}