using UnityEngine;

namespace GridTool.DataScripts
{
    public struct UnitLevelData
    {
        public int HorzOffset;
        public int VertOffset;
        public bool CanMove;
        public bool CanAttack;

        public UnitLevelData(int horzOffset, int vertOffset)
        {
            HorzOffset = horzOffset;
            VertOffset = vertOffset;
            CanMove = false;
            CanAttack = false;
        }

        public UnitLevelData(int horzOffset, int vertOffset, string moveAndAttack)
        {
            HorzOffset = horzOffset;
            VertOffset = vertOffset;
            CanMove = moveAndAttack.Contains("m") || moveAndAttack.Contains("M");
            CanAttack = moveAndAttack.Contains("a") || moveAndAttack.Contains("A");
        }

        public string GetReadableString()
        {
            return CanMove switch
            {
                true when CanAttack => "M + A",
                true                => "Move",
                _                   => CanAttack ? "Attack" : "-"
            };
        }

        public int GetValue()
        {
            int value = CanMove ? 1 : 0;
            value += CanAttack ? 2 : 0;
            return value;
        }

        public void SetNewData(int moveAndAttack)
        {
            switch (moveAndAttack) {
                case 1:
                    CanMove = true;
                    break;
                case 2:
                    CanAttack = true;
                    break;
                case 3:
                    CanMove = true;
                    CanAttack = true;
                    break;
                default:
                    CanMove = false;
                    CanAttack = false;
                    break;
            }
        }
    }
}