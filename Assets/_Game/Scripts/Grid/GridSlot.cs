using GridTool.DataScripts;
using Scripts.GridActions;
using Scripts.Units;
using UnityEngine;
using Utility.Other;

namespace Scripts.Grid
{
    public class GridSlot : MonoBehaviour
    {
        [SerializeField] private Unit _gridUnit;

        public bool CanMove => _gridUnit == null;
        public bool CanAttack => _gridUnit != null && !_gridUnit.PlayerOwned;

        private GridController _controller;
        private int _xPosition;
        private int _yPosition;

        private GridSlot _actingSlot;
        private bool _actionReady;

        public void Setup(GridController controller, int x, int y, GameObject tileArt)
        {
            _controller = controller;
            _xPosition = x;
            _yPosition = y;
            gameObject.name = "Grid Slot (" + (x + 1) + "," + (y + 1) + ")";
            transform.localPosition = new Vector3(x, 0, y);
            if (tileArt != null) {
                var art = Instantiate(tileArt, transform);
                RandomizeArt.RotateRandomClamped(art.transform);
            } else {
                Debug.LogWarning("No tile art");
            }
        }

        public void OnHover()
        {
            HoverSelectedController.instance.SetHoverParent(transform);
        }

        public void OnSelect(bool isPlayer)
        {
            if (_actionReady) {
                if (_gridUnit == null) {
                    _gridUnit = _actingSlot.ClearGridUnit();
                    _gridUnit.transform.SetParent(transform, false);
                    _gridUnit.AddUnitMoved();
                } else {
                    Destroy(_gridUnit.gameObject);
                    _gridUnit.AddUnitAttacked();
                }
                HoverSelectedController.instance.DisableSelected();
                HoverSelectedController.instance.ClearUnitOptions();
                _controller.ClearSelection();
                return;
            }
            _controller.ClearSelection();
            HoverSelectedController.instance.SetSelectedParent(transform);
            if (_gridUnit != null && (isPlayer == _gridUnit.PlayerOwned)) {
                DisplayUnitOptions(_gridUnit.Data, _gridUnit.CanMove, _gridUnit.CanAttack);
            }
        }

        public Unit ClearGridUnit()
        {
            var unit = _gridUnit;
            _gridUnit = null;
            return unit;
        }

        public bool PlaceObject(Unit unitToPlace, bool playerOwned)
        {
            if (_gridUnit != null) return false;

            _gridUnit = Instantiate(unitToPlace, transform);
            _gridUnit.PlayerOwned = playerOwned;
            if (!playerOwned) {
                EnemyToGrid.AddNewEnemy?.Invoke(_gridUnit);
            }
            RandomizeArt.RotateRandomClamped(unitToPlace.transform);
            return true;
        }

        public void ReadyAction(GridSlot actingSlot, bool ready = true)
        {
            _actingSlot = actingSlot;
            _actionReady = ready;
        }

        private void DisplayUnitOptions(UnitData data, bool move, bool attack)
        {
            HoverSelectedController.instance.ClearUnitOptions();
            var options = data.GetReadableData();
            foreach (var option in options) {
                int x = _xPosition + option.HorzOffset;
                int y = _yPosition + option.VertOffset;
                var slot = _controller.GetSlot(x, y);
                if (slot == null) continue;
                if (move && slot.CanMove && option.CanMove) {
                    HoverSelectedController.instance.SetMoveOption(slot.transform);
                    slot.ReadyAction(this);
                }
                if (attack && slot.CanAttack && option.CanAttack) {
                    HoverSelectedController.instance.SetAttackOption(slot.transform);
                    slot.ReadyAction(this);
                }
            }
        }
    }
}