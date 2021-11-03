#if UNITY_EDITOR
using Scripts.Utility.Audio.Systems.Base;
using UnityEditor;
using UnityEngine;

namespace Scripts.Utility.Audio.Systems.Editor
{
    [CustomEditor(typeof(SfxBase), true)]
    public class SfxBaseEditor : UnityEditor.Editor
    {
        private AudioSourceController _audioSourcePreview;

        public void OnEnable()
        {
            _audioSourcePreview = EditorUtility.CreateGameObjectWithHideFlags("Audio Clip Preview", HideFlags.HideAndDontSave, typeof(AudioSourceController)).GetComponent<AudioSourceController>();
        }

        public void OnDisable()
        {
            DestroyImmediate(_audioSourcePreview.gameObject);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Separator();

            if (GUILayout.Button("Preview Sfx", GUILayout.Height(40))) {
                ((SfxBase)target).Play(_audioSourcePreview);
            }
        }
    }
}
#endif