using System.Reflection;
using Factories;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Client
{
    sealed class SpawnPointSystem : IEcsInitSystem, IEcsRunSystem
    {
        private IItemFactory _itemFactory = null;
        
        public SpawnPointSystem(IItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
        }
        
        public void Init(EcsSystems systems)
        {
            // .CreatePrefabWithEntities("MouseClick", systems.GetWorld(), out GameObject o)
            Debug.Log(_itemFactory);
        }

        public void Run(EcsSystems systems)
        {
            //throw new System.NotImplementedException();
        }
    }
}