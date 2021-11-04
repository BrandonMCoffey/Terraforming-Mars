using GridTool.DataScripts;
using UnityEngine;

namespace Scripts.Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private UnitData _data = null;

        public bool PlayerOwned { get; set; }

        public UnitData Data => _data;
    }
}