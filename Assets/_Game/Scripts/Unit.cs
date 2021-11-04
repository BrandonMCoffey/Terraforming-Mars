using GridTool.DataScripts;
using UnityEngine;

namespace Scripts
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private UnitData _data = null;
        [SerializeField] private GameObject _art = null;

        public UnitData Data => _data;
    }
}