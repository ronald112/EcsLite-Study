using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEditor;
using UnityEngine;

namespace Factories
{
    class ItemFactory : IItemFactory
    {
        private Dictionary<Enums.PrefabName, string> enumToNameMapping = null;

        public void Init()
        {
            enumToNameMapping = new Dictionary<Enums.PrefabName, string>()
            {
                {Enums.PrefabName.MouseClick, "MouseClick"}
            };

        }
        
        public GameObject CreatePrefab(Enums.PrefabName name)
        {
            if (!enumToNameMapping.TryGetValue(name, out var value)) return null;
            
            var prefab = Resources.Load(value) as GameObject;
            return GameObject.Instantiate(prefab);
        }
    }
}