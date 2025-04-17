using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public class UnitsSaveLoader : SaveLoader<UnitManager, UnitsData>
    {
        protected override UnitsData ConvertToData(UnitManager service)
        {
            Debug.Log($"<color=yellow>Converted to data = {service.SceneUnits}</color>");

            var unitsData = new List<UnitData>();

            foreach (var unit in service.SceneUnits)
            {
                unitsData.Add(new UnitData(unit.Type, unit.ID, unit.HitPoints, unit.Position, unit.Rotation));
            }

            return new UnitsData(unitsData);
        }

        protected override void SetupData(UnitManager service, UnitsData data)
        {
            Debug.Log($"<color=yellow>Setuped data = {data.UnitsDataList}</color>");
            service.SetupUnits(data.UnitsDataList);
        }

        protected override void SetupDefaultData(UnitManager service)
        {
            Debug.Log("<color=yellow>Setup default data</color>");
        }
    }
}
