using Leopotam.EcsLite;
using UnityEngine;

namespace Factories
{
    public interface IItemFactory
    {
        public int CreatePrefabWithEntities(string path, EcsWorld ecsWorld, out GameObject gameObject);
    }
}