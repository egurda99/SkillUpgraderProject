// [Serializable]
// public sealed class ResourcesSaveLoader : SaveLoader<ResourceService, ResourcesData>
// {
//     protected override ResourcesData ConvertToData(ResourceService service)
//     {
//         Debug.Log($"<color=yellow>Converted to data = {service.SceneResources}</color>");
//
//         var resourcesData = new List<ResourceData>();
//
//         foreach (var resource in service.SceneResources)
//         {
//             resourcesData.Add(new ResourceData(resource.Value.ResourceType, resource.Value.ID, resource.Value.Amount));
//         }
//
//         return new ResourcesData(resourcesData);
//     }
//
//     protected override void SetupData(ResourceService service, ResourcesData data)
//     {
//         Debug.Log($"<color=yellow>Setuped data = {data.ResourcesDataList}</color>");
//
//         var sceneResources = new List<Resource>();
//
//         sceneResources.AddRange(service.SceneResources.Values);
//
//         var resourceDataDict = data.ResourcesDataList.ToDictionary(data => data.Id);
//
//         foreach (var sceneResource in sceneResources)
//         {
//             if (resourceDataDict.TryGetValue(sceneResource.ID, out var resourceData))
//             {
//                 sceneResource.SetupChests(resourceData.ResourceType, resourceData.Amount, resourceData.Id);
//             }
//         }
//
//         service.SetResources(sceneResources);
//     }
//
//     protected override void SetupDefaultData(ResourceService service)
//     {
//         Debug.Log("<color=yellow>SetupChests default data</color>");
//     }
// }

