using Leopotam.EcsLite;
using UnityEngine;

namespace Factories
{
    public interface IItemFactory
    {
        public void CreatePrefab(string path, out GameObject gameObject);
    }
}