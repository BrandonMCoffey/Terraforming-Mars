using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GridTool.DataScripts
{
    [CreateAssetMenu(menuName = "Level Designer/Object Collection")]
    public class ObjectCollection : ScriptableObject
    {
        public List<ObjectData> Objects = new List<ObjectData>();

        public Texture2D GetTexture(string objName)
        {
            foreach (var obj in Objects.Where(obj => obj.Name == objName)) {
                return obj.Texture;
            }
            return Texture2D.blackTexture;
        }

        public ObjectData GetObject(string objName)
        {
            return Objects.FirstOrDefault(obj => obj.Name == objName);
        }

        public void CheckValid()
        {
            Objects = Objects.Where(obj => obj != null).ToList();
        }
    }
}