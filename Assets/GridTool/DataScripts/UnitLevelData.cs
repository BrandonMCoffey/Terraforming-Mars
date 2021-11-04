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

        public UnitLevelData(int horzOffset, int vertOffset, bool canMove, bool canAttack)
        {
            HorzOffset = horzOffset;
            VertOffset = vertOffset;
            CanMove = canMove;
            CanAttack = canAttack;
        }

        public UnitLevelData(int horzOffset, int vertOffset, int moveAndAttack)
        {
            HorzOffset = horzOffset;
            VertOffset = vertOffset;
            CanMove = moveAndAttack == 1 || moveAndAttack == 3;
            CanAttack = moveAndAttack == 2 || moveAndAttack == 3;
        }

        public int GetValue()
        {
            Debug.Log(HorzOffset + "," + VertOffset + "  " + CanMove + " " + CanAttack);
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