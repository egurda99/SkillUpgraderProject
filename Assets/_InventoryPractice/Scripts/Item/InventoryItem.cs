using System;
using System.Linq;
using UnityEngine;

namespace InventoryPractice
{
    [Serializable]
    public class InventoryItem
    {
        public string Id;

        public InventoryItemMetaData MetaData;
        public InventoryItemFlags Flags;

        [SerializeReference] public IItemComponent[] Components; // чтобы через Инспектор выбирать

        public T GetComponent<T>() where T : class
        {
            return Components.FirstOrDefault(component => component is T) as T;
        }

        public bool TryGetComponent<T>(out T component)
        {
            foreach (var itemComponent in Components)
            {
                if (itemComponent is T targetComponent)
                {
                    component = targetComponent;
                    return true;
                }
            }

            component = default;
            return false;
        }

        public InventoryItem Clone()
        {
            var copiedComponents = Array.Empty<IItemComponent>();
            if (Components != null)
            {
                copiedComponents = new IItemComponent[Components.Length];
                for (var i = 0; i < Components.Length; i++)
                {
                    var component = Components[i].Clone();
                    copiedComponents[i] = component;
                }
            }

            return new InventoryItem
            {
                Id = Id,
                Flags = Flags,
                MetaData = new InventoryItemMetaData
                {
                    Name = MetaData.Name,
                    Description = MetaData.Description,
                    Icon = MetaData.Icon
                },
                Components = copiedComponents
            };
        }
    }
}
