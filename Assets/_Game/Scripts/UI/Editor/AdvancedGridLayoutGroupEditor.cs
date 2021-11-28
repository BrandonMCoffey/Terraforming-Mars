#if UNITY_EDITOR
using UnityEditor;

namespace Scripts.UI.Editor {
    [CustomEditor(typeof(AdvancedGridLayoutGroup))]
    public class AdvancedGridLayoutGroupEditor : UnityEditor.Editor {
        private bool _foldoutOpen;

        public override void OnInspectorGUI()
        {
            var group = target as AdvancedGridLayoutGroup;
            var useDynamicPadding = serializedObject.FindProperty("_useDynamicPadding");
            EditorGUILayout.PropertyField(useDynamicPadding);

            _foldoutOpen = EditorGUILayout.BeginFoldoutHeaderGroup(_foldoutOpen, "Padding");
            if (_foldoutOpen) {
                if (useDynamicPadding.boolValue) {
                    var paddingLr = serializedObject.FindProperty("_dynamicPaddingLr");
                    var paddingTb = serializedObject.FindProperty("_dynamicPaddingTb");
                    var lr = paddingLr.vector2Value;
                    lr.x = EditorGUILayout.FloatField("Left", lr.x);
                    lr.y = EditorGUILayout.FloatField("Right", lr.y);
                    paddingLr.vector2Value = lr;
                    var tb = paddingTb.vector2Value;
                    tb.x = EditorGUILayout.FloatField("Top", tb.x);
                    tb.y = EditorGUILayout.FloatField("Bottom", tb.y);
                    paddingTb.vector2Value = tb;
                }
                else {
                    var paddingLr = serializedObject.FindProperty("_normalPaddingLr");
                    var paddingTb = serializedObject.FindProperty("_normalPaddingTb");
                    var lr = paddingLr.vector2IntValue;
                    lr.x = EditorGUILayout.IntField("Left", lr.x);
                    lr.y = EditorGUILayout.IntField("Right", lr.y);
                    paddingLr.vector2IntValue = lr;
                    var tb = paddingTb.vector2IntValue;
                    tb.x = EditorGUILayout.IntField("Top", tb.x);
                    tb.y = EditorGUILayout.IntField("Bottom", tb.y);
                    paddingTb.vector2IntValue = tb;
                }
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
            var useDynamicSpacing = serializedObject.FindProperty("_useDynamicSpacing");
            EditorGUILayout.PropertyField(useDynamicSpacing);
            EditorGUILayout.PropertyField(useDynamicSpacing.boolValue
                ? serializedObject.FindProperty("_dynamicSpacing")
                : serializedObject.FindProperty("_normalSpacing"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_StartCorner"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_StartAxis"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_ChildAlignment"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_cellsPerLine"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_aspectRatio"));
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.Vector2Field("Cell size", group.cellSize);
            EditorGUI.EndDisabledGroup();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif