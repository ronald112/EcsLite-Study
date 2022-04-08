using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    sealed class DoorOpenedSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsPool<PathByTwoPointsComponent> _poolTwoPoints = null;
        private EcsPool<ModelTransformComponent> _poolModelTransform = null;
        private EcsFilter _filterMovingDoor = null;
        private SharedConstants _shared = null;

        public void Init(EcsSystems systems)
        {
            _shared = systems.GetShared<SharedConstants>();
            _world = systems.GetWorld();
            
            _filterMovingDoor = _world.Filter<DoorTag>().Inc<DoorReadyToMoveTag>()
                .Inc<PathByTwoPointsComponent>().Inc<ModelTransformComponent>().End();

            _poolTwoPoints = _world.GetPool<PathByTwoPointsComponent>();
            _poolModelTransform = _world.GetPool<ModelTransformComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var doorEntity in _filterMovingDoor)
            {
                var moveByTwoPointsComponent = _poolTwoPoints.Get(doorEntity);
                var modelTransformComponent = _poolModelTransform.Get(doorEntity);
                
                if (Vector3.SqrMagnitude(modelTransformComponent.transform.position
                                         - moveByTwoPointsComponent.end) <= _shared.epsilon)
                {
                    _world.GetPool<DoorReadyToMoveTag>().Del(doorEntity);
                    _world.GetPool<DoorOpenedTag>().Add(doorEntity);
                    int newEntity = _world.NewEntity();
                    _world.GetPool<NavMeshSurfaceNeedsRebuildEventComponent>().Add(newEntity);
                }
            }
        }
    }
}