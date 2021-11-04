using UnityEngine;

namespace GridTool.DataScripts
{
    [CreateAssetMenu(menuName = "Level Designer/Unit Data")]
    public class UnitData : ScriptableObject
    {
        [Header("Unit Properties")]
        public string Name = "";
        public int MaxMoveDistance = 1;
        public UnitLevelData[,] UnitOptions;

        public int UnitOptionLength => MaxMoveDistance * 2 + 1;

        public void Verify()
        {
            if (UnitOptions != null) return;
            UnitOptions = new UnitLevelData[UnitOptionLength, UnitOptionLength];
            for (int x = 0; x < UnitOptionLength; x++) {
                for (int y = 0; y < UnitOptionLength; y++) {
                    UnitOptions[x, y] = new UnitLevelData(x, y);
                }
            }
        }
    }
}