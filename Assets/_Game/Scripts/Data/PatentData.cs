using System.Collections.Generic;
using Scripts.Data.Structs;
using Scripts.Enums;
using UnityEngine;
using Utility.Buttons;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace Scripts.Data
{
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
        public PatentResourceType Alt1;
        public PatentResourceType Alt2;
        public PatentResourceType Alt3;
        [Header("Effects")]
        public PatentEffect Effect1;
        public PatentEffect Effect2;
        public PatentEffect Effect3;
        public PatentEffect Effect4;

        private PatentCollection _collection;

        public List<Sprite> GetAltSprites(IconData icons)
        {
            var list = new List<Sprite>();
            var icon1 = icons.GetIcon(Alt1);
            if (icon1 != null) list.Add(icon1);
            var icon2 = icons.GetIcon(Alt2);
            if (icon2 != null) list.Add(icon2);
            var icon3 = icons.GetIcon(Alt3);
            if (icon3 != null) list.Add(icon3);
            return list;
        }

        public List<Sprite> GetEffectSprites(IconData icons)
        {
            var list = new List<Sprite>();
            if (Effect1.Active && Effect1.Resource != ResourceType.None) {
                list.Add(icons.GetResource(Effect1.Resource));
            }
            if (Effect2.Active && Effect2.Resource != ResourceType.None) {
                list.Add(icons.GetResource(Effect2.Resource));
            }
            if (Effect3.Active && Effect3.Resource != ResourceType.None) {
                list.Add(icons.GetResource(Effect3.Resource));
            }
            if (Effect4.Active && Effect4.Resource != ResourceType.None) {
                list.Add(icons.GetResource(Effect4.Resource));
            }
            return list;
        }

#if UNITY_EDITOR
        public void Init(PatentCollection collection)
        {
            _collection = collection;
        }

        [ContextMenu("Delete this")]
        [Button("Delete This", Spacing = 25)]
        private void EditorDeleteThis()
        {
            if (_collection != null) {
                _collection.EditorDeletePatent(this);
            } else {
                Undo.DestroyObjectImmediate(this);
                AssetDatabase.SaveAssets();
            }
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