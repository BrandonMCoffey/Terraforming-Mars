using System.Collections.Generic;
using UnityEngine;

namespace GridTool.DataScripts
{
    [CreateAssetMenu(menuName = "Level Designer/Unit Data")]
    public class UnitData : ScriptableObject
    {
        [Header("Unit Properties")]
        public string Name = "";
        public int MaxMoveDistance = 1;

        [TextArea] public string UnitString = "";
        public UnitLevelData[,] UnitOptions;

        public int UnitOptionLength => MaxMoveDistance * 2 + 1;

        public List<UnitLevelData> GetReadableData()
        {
            CheckValid();
            var usableData = new List<UnitLevelData>();
            for (int x = 0; x < UnitOptionLength; x++) {
                for (int y = 0; y < UnitOptionLength; y++) {
                    if (UnitOptions[x, y].IsUseful) {
                        usableData.Add(UnitOptions[x, y]);
                    }
                }
            }
            return usableData;
        }

        public void CheckValid(int maxDistance)
        {
            if (MaxMoveDistance == maxDistance) return;
            MaxMoveDistance = maxDistance;
            CheckValid();
        }

        public void CheckValid()
        {
            if (UnitOptions == null) {
                if (!string.IsNullOrEmpty(UnitString)) {
                    if (ReadFromString()) {
                        return;
                    }
                }
                UnitOptions = new UnitLevelData[UnitOptionLength, UnitOptionLength];
                for (int x = 0; x < UnitOptionLength; x++) {
                    for (int y = 0; y < UnitOptionLength; y++) {
                        UnitOptions[x, y] = new UnitLevelData(x - MaxMoveDistance, y - MaxMoveDistance);
                    }
                }
            }
            // Check for resizing Unit
            if (UnitOptions.GetLength(0) != UnitOptionLength || UnitOptions.GetLength(1) != UnitOptionLength) {
                var newArr = new UnitLevelData[UnitOptionLength, UnitOptionLength];
                for (int x = 0; x < UnitOptionLength; x++) {
                    for (int y = 0; y < UnitOptionLength; y++) {
                        if (x < UnitOptions.GetLength(0) && y < UnitOptions.GetLength(1)) {
                            newArr[x, y] = UnitOptions[x, y];
                        } else {
                            newArr[x, y] = new UnitLevelData();
                        }
                    }
                }
                UnitOptions = newArr;
            }
        }

        public bool ReadFromString()
        {
            try {
                var arr = UnitString.Split('\n');
                var rowArr = arr[0].Split(',');
                int width = rowArr.Length;
                int height = arr.Length;
                if (width != height) {
                    return false;
                }

                var newOptions = new UnitLevelData[width, width];
                for (int y = 0; y < width; y++) {
                    rowArr = arr[y].Split(',');
                    for (int x = 0; x < width; x++) {
                        if (x >= rowArr.Length || y > arr.Length) {
                            newOptions[x, y] = new UnitLevelData(x - MaxMoveDistance, y - MaxMoveDistance);
                        } else {
                            newOptions[x, y] = new UnitLevelData(x - MaxMoveDistance, y - MaxMoveDistance, rowArr[x]);
                        }
                    }
                }
                MaxMoveDistance = (width - 1) / 2;
                UnitOptions = newOptions;
                return true;
            } catch {
                Debug.LogWarning("Invalid unit string");
                UnitString = "";
            }
            return false;
        }

        public void SaveUnitOptions()
        {
            if (UnitOptions == null) {
                Debug.Log("Error: Invalid unit options");
                return;
            }
            string levelDataString = "";

            for (int y = 0; y < UnitOptionLength; y++) {
                for (int x = 0; x < UnitOptionLength; x++) {
                    levelDataString += UnitOptions[x, y].GetReadableString() + (x == UnitOptionLength - 1 ? "" : ",");
                }
                levelDataString += (y == UnitOptionLength - 1 ? "" : "\n");
            }
            UnitString = levelDataString;
        }
    }
}