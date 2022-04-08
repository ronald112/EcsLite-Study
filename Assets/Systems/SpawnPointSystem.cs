using Enums;
using Factories;
using Leopotam.EcsLite;
using UnityEngine;

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
            
            _itemFactory.Init();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var entity in _filterMouseFloorHit)
            {
                var pos = _poolMouseFloorHit.Get(entity).position;
                if (!_pointOfGameObject)
                {
                    _pointOfGameObject = _itemFactory.CreatePrefab(PrefabName.MouseClick);
                }
                _pointOfGameObject.GetComponent<Transform>().position = pos;
            }
        }
    }
}