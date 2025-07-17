namespace MyCodeBase
{
    // [Serializable]
    // public sealed class UnitsSaveLoader : SaveLoader<UnitManager, UnitsData>
    // {
    //     private readonly UnitSpawner _unitSpawner;
    //
    //     public UnitsSaveLoader(UnitSpawner unitSpawner)
    //     {
    //         _unitSpawner = unitSpawner;
    //     }
    //
    //     protected override UnitsData ConvertToData(UnitManager service)
    //     {
    //         Debug.Log($"<color=yellow>Converted to data = {service.SceneUnits}</color>");
    //
    //         var unitsData = new List<UnitData>();
    //
    //         foreach (var unit in service.SceneUnits)
    //         {
    //             unitsData.Add(new UnitData(unit.Type, unit.ID, unit.HitPoints, unit.Position, unit.Rotation));
    //         }
    //
    //         return new UnitsData(unitsData);
    //     }
    //
    //     protected override void SetupData(UnitManager service, UnitsData data)
    //     {
    //         Debug.Log($"<color=yellow>Setuped data = {data.UnitsDataList}</color>");
    //
    //         var units = new List<Unit>();
    //
    //         foreach (var unitData in data.UnitsDataList)
    //         {
    //             var unit = _unitSpawner.SpawnUnitByType(unitData.Type, unitData.Position, unitData.EulerAngles);
    //             unit.SetupChests(unitData.Type, unitData.Id, unitData.HitPoints);
    //             units.Add(unit);
    //         }
    //
    //         service.SetupUnits(units);
    //     }
    //
    //     protected override void SetupDefaultData(UnitManager service)
    //     {
    //         Debug.Log("<color=yellow>SetupChests default data</color>");
    //     }
    // }
}