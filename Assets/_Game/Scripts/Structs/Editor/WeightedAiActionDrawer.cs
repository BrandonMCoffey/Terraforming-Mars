using UnityEditor;
using UnityEngine;

namespace Scripts.Structs.Editor
{
    [CustomPropertyDrawer(typeof(WeightedAiAction))]
    public class WeightedAiActionDrawer : PropertyDrawer
    {
        private const float Spacing = 6;
        private const float EnumSize = 100;

        public override void OnGUI(Rect rect, SerializedProperty prop, GUIContent label)
        {
            var weightProp = prop.FindPropertyRelative("Weight");
            var actionProp = prop.FindPropertyRelative("Action");

            var width = rect.width;
            var enumSize = width > EnumSize * 2.5f ? width * 0.4f : EnumSize;
            rect.width = enumSize;
            EditorGUI.PropertyField(rect, actionProp, GUIContent.none);

            rect.width = width - enumSize - Spacing;
            rect.x += enumSize + Spacing;
            EditorGUI.PropertyField(rect, weightProp, GUIContent.none);
        }
    }
}