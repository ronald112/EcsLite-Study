using Enums;
using UnityEngine;

namespace Factories
{
    public interface IItemFactory
    {
        public void Init();
        public GameObject CreatePrefab(PrefabName name);
    }
}