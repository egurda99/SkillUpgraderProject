using System;
using UnityEngine;

[Serializable]
public class MoneySaveLoader : SaveLoader<MoneyStorage, MoneyData>
{
    protected override MoneyData ConvertToData(MoneyStorage service)
    {
        Debug.Log($"<color=yellow>Convert to data = {service.Money}</color>");
        return new MoneyData
        {
            Money = service.Money
        };
    }

    protected override void SetupData(MoneyStorage service, MoneyData data)
    {
        Debug.Log($"<color=yellow>Setup data = {data.Money}</color>");
        service.SetupMoney(data.Money);
    }

    protected override void SetupDefaultData(MoneyStorage service)
    {
        Debug.Log($"<color=yellow>Setup default data = {100}</color>");
        service.SetupMoney(100);
    }
}
