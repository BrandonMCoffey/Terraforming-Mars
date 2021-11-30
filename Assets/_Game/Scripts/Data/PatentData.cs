using Scripts.Data.Structs;
using Scripts.Enums;
using UnityEngine;
using Utility.Buttons;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "TM/Patent Data")]
    public class PatentData : ScriptableObject
    {
        [Header("Patent Info")]
        public string Name;
        public int Cost;
        public int Honor;
        [Header("Requirements")]
        public PatentConstraint Constraint1;
        public PatentConstraint Constraint2;
        [Header("Alt Resources")]
        public ResourceType Alt1;
        public ResourceType Alt2;
        public ResourceType Alt3;
        [Header("Effects")]
        public PatentEffect Effect1;
        public PatentEffect Effect2;
        public PatentEffect Effect3;
        public PatentEffect Effect4;

        private PatentCollection _collection;

#if UNITY_EDITOR
        public void Init(PatentCollection collection)
        {
            _collection = collection;
        }

        [ContextMenu("Delete this")]
        [Button("Delete This", Spacing = 25)]
        private void EditorDeleteThis()
        {
            _collection.EditorDeletePatent(this);
        }

        [Button("Update Patent Name")]
        private void EditorRename()
        {
            name = Name;
            AssetDatabase.SaveAssets();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}