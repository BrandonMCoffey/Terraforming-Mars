#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Utility.Audio.Systems.Events;

namespace Utility.Audio.Systems.Editor
{
    [CustomEditor(typeof(MusicEvent), true)]
    public class MusicEventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Separator();

            EditorGUI.BeginDisabledGroup(!Application.isPlaying);
            if (GUILayout.Button("Play Music", GUILayout.Height(40))) {
                ((MusicEvent)target).Play();
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}
#endif