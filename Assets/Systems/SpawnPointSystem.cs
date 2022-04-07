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
        private EcsWorld _world = null;
        private EcsFilter _filterMouseFloorHit = null;
        private EcsPool<MouseRaycastHitFloorResultComponent> _poolMouseFloorHit = null;

        private GameObject _pointOfGameObject = null;

        public SpawnPointSystem(IItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
        }
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filterMouseFloorHit = _world.Filter<MouseRaycastHitFloorResultComponent>().End();
            _poolMouseFloorHit = _world.GetPool<MouseRaycastHitFloorResultComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var entity in _filterMouseFloorHit)
            {
                var pos = _poolMouseFloorHit.Get(entity).position;
                if (!_pointOfGameObject)
                {
                    _itemFactory.CreatePrefab("MouseClick", out _pointOfGameObject);
                }
                _pointOfGameObject.GetComponent<Transform>().position = pos;
            }
        }
    }
}