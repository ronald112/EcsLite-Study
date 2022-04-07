using Leopotam.EcsLite;
using UnityEditor;
using UnityEngine;

namespace Factories
{
    class ItemFactory : IItemFactory
    {
        public ItemFactory()
        {
            
        }
        
        public int CreatePrefabWithEntities(string path, EcsWorld ecsWorld, out GameObject gameObject)
        {
            var prefab = Resources.Load(path) as GameObject;
            gameObject = GameObject.Instantiate(prefab);
            return 0;
            // Voody.UniLeo.Lite.ConvertToEntity
        }
    }
}