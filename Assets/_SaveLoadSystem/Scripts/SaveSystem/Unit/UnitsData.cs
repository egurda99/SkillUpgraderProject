using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class UnitsData
    {
        public List<UnitData> UnitsDataList = new();

        // public UnitsData(List<UnitData> unitsData)
        // {
        //     Debug.Log($"units data {unitsData} ");
        //     UnitsDataList.AddRange(unitsData);
        // }

        public UnitsData(List<UnitData> unitsData)
        {
            if (unitsData != null)
            {
                UnitsDataList.AddRange(unitsData);
            }
            else
            {
                Debug.LogWarning("[UnitsData] ?? Constructor received null list!");
            }
        }
    }
}
