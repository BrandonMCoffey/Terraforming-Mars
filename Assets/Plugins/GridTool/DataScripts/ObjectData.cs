using UnityEngine;
using Utility.Inspector;

namespace GridTool.DataScripts
{
    [CreateAssetMenu(menuName = "Level Designer/Object Data")]
    public class ObjectData : ScriptableObject
    {
        [Header("art Properties")]
        public string Name = "";
        public int SortingPriority = 1;
        public Color MixColor = Color.white;

        [Header("Sprite Properties")]
        [ReadOnly] public ObjectSpriteType SpriteType;
        [ReadOnly] public int SpriteAnimationFrames = 1;

        [Header("Sprites")]
        public Texture2D Texture;
        [HideInInspector] public Sprite[] Static = { null };
        [HideInInspector] public Sprite[] Up = { null };
        [HideInInspector] public Sprite[] Left = { null };
        [HideInInspector] public Sprite[] Down = { null };
        // Sprite right == Static Sprite
    }

    public enum ObjectSpriteType
    {
        Static,
        Directional,
    }
}