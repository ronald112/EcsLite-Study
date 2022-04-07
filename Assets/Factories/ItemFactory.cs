using Leopotam.EcsLite;
using UnityEditor;
using UnityEngine;

namespace Factories
{
    class ItemFactory : IItemFactory
    {
        public void CreatePrefab(string path, out GameObject gameObject)
        {
            var prefab = Resources.Load(path) as GameObject;
            gameObject = GameObject.Instantiate(prefab);
        }
    }
}