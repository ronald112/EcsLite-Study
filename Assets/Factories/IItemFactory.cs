using Leopotam.EcsLite;
using UnityEngine;

namespace Factories
{
    public interface IItemFactory
    {
        public void Init();
        public GameObject CreatePrefab(Enums.PrefabName name);
    }
}