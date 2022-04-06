using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;

namespace Client
{
    sealed class DoorStatusTagChangeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsPool<MoveByTwoPointsComponent> _poolTwoPoints = null;
        private EcsPool<ModelTransformComponent> _poolModelTransform = null;
        private EcsFilter _filterMovingDoor = null;
        private SharedConstants _shared = null;

        public void Init(EcsSystems systems)
        {
            _shared = systems.GetShared<SharedConstants>();
            _world = systems.GetWorld();
            
            _filterMovingDoor = _world.Filter<DoorTag>().Inc<DoorReadyToMoveTag>()
                .Inc<MoveByTwoPointsComponent>().Inc<ModelTransformComponent>().End();

            _poolTwoPoints = _world.GetPool<MoveByTwoPointsComponent>();
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
                    _world.GetPool<NavMeshSurfaceNeedsRebuildTagComponent>().Add(newEntity);
                }
                // else if (Vector3.SqrMagnitude(modelTransformComponent.transform.position
                //                               - moveByTwoPointsComponent.start) < _shared.epsilon)
                // {
                //     _world.GetPool<Do>().Del(doorEntity);
                // }
            }
        }
    }
}