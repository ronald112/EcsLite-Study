using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Factories
{
    class ItemFactory : IItemFactory
    {
        private Dictionary<PrefabName, string> enumToNameMapping = null;

        public void Init()
        {
            enumToNameMapping = new Dictionary<PrefabName, string>()
            {
                {PrefabName.MouseClick, "MouseClick"}
            };

        }
        
        public GameObject CreatePrefab(PrefabName name)
        {
            if (!enumToNameMapping.TryGetValue(name, out var value)) return null;
            
            var prefab = Resources.Load(value) as GameObject;
            return GameObject.Instantiate(prefab);
        }
    }
}