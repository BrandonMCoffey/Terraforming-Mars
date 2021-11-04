using GridTool.DataScripts;
using UnityEngine;
using Utility.Other;

namespace Scripts
{
    public class GridSlot : MonoBehaviour
    {
        [SerializeField] private Unit _gridUnit;

        public bool IsClear => _gridUnit == null;

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

        public void OnSelect()
        {
            if (_actionReady) {
                if (_gridUnit == null) {
                    Debug.Log("Move " + _actingSlot.name + " to " + name, gameObject);
                    _gridUnit = _actingSlot.ClearGridUnit();
                    _gridUnit.transform.SetParent(transform, false);
                } else {
                    Debug.Log("Kill " + name, gameObject);
                    Destroy(_gridUnit.gameObject);
                }
                HoverSelectedController.instance.ClearUnitOptions();
                _controller.ClearSelection();
                return;
            }
            _controller.ClearSelection();
            HoverSelectedController.instance.SetSelectedParent(transform);
            if (_gridUnit != null) {
                DisplayUnitOptions(_gridUnit.Data);
            }
        }

        public Unit ClearGridUnit()
        {
            var unit = _gridUnit;
            _gridUnit = null;
            return unit;
        }

        public bool PlaceObject(Unit unitToPlace)
        {
            if (_gridUnit != null) return false;

            _gridUnit = Instantiate(unitToPlace, transform);
            RandomizeArt.RotateRandomClamped(unitToPlace.transform);
            return true;
        }

        public void ReadyAction(GridSlot actingSlot, bool ready = true)
        {
            _actingSlot = actingSlot;
            _actionReady = ready;
        }

        private void DisplayUnitOptions(UnitData data)
        {
            HoverSelectedController.instance.ClearUnitOptions();
            var options = data.GetReadableData();
            foreach (var option in options) {
                int x = _xPosition + option.HorzOffset;
                int y = _yPosition + option.VertOffset;
                var slot = _controller.GetSlot(x, y);
                if (slot == null) continue;
                bool slotClear = slot.IsClear;
                if (slotClear && option.CanMove) {
                    HoverSelectedController.instance.SetMoveOption(slot.transform);
                    slot.ReadyAction(this);
                }
                if (!slotClear && option.CanAttack) {
                    HoverSelectedController.instance.SetAttackOption(slot.transform);
                    slot.ReadyAction(this);
                }
            }
        }
    }
}