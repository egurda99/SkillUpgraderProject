using System;
using System.Collections.Generic;
using GameEngine;
using UnityEngine;

[Serializable]
public class ResourcesSaveLoader : SaveLoader<ResourceService, ResourcesData>
{
    protected override ResourcesData ConvertToData(ResourceService service)
    {
        Debug.Log($"<color=yellow>Convert to data = {service.SceneResources}</color>");

        var resourcesData = new List<ResourceData>();

        foreach (var resource in service.SceneResources)
        {
            resourcesData.Add(new ResourceData(resource.ResourceType, resource.ID, resource.Amount));
        }

        return new ResourcesData(resourcesData);
    }

    protected override void SetupData(ResourceService service, ResourcesData data)
    {
        Debug.Log($"<color=yellow>Setup data = {data.ResourcesDataList}</color>");
        service.SetupResources(data);
    }

    protected override void SetupDefaultData(ResourceService service)
    {
        Debug.Log("<color=yellow>Setup default data</color>");
        //List<ResourceData> ResourcesDataList
    }
}
