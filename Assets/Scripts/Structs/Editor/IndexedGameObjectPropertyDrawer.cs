#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Scripts.Structs.Editor
{
    [CustomPropertyDrawer(typeof(IndexedGameObject))]
    public class IndexedGameObjectPropertyDrawer : PropertyDrawer
    {
        private const float IntSize = 24;
        private const float Spacing = 6;

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            label = EditorGUI.BeginProperty(pos, label, prop);
            var contentRect = EditorGUI.PrefixLabel(pos, GUIUtility.GetControlID(FocusType.Passive), label);
            var indexProp = prop.FindPropertyRelative("Index");
            var gameObjectProp = prop.FindPropertyRelative("gameObject");

            float width = contentRect.width;
            contentRect.width = IntSize;
            EditorGUI.PropertyField(contentRect, indexProp, GUIContent.none);
            contentRect.x += IntSize + Spacing;
            contentRect.width = width - IntSize - Spacing;
            EditorGUI.PropertyField(contentRect, gameObjectProp, GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
}
#endif