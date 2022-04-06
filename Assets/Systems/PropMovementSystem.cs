using Leopotam.EcsLite;
using TMPro;
using UnityEngine;

namespace Client
{
    sealed class PropMovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsFilter _filter = null;
        private EcsPool<MoveToCoordinateComponent> _poolDirection = null;
        private EcsPool<ModelTransformComponent> _poolTransform = null;
        private SharedConstants _shared = null;

        public void Init(EcsSystems systems)
        {
            _shared = systems.GetShared<SharedConstants>();
            _world = systems.GetWorld();
            _filter = _world.Filter<MovablePropComponent>().Inc<MoveToCoordinateComponent>().End();
            _poolDirection = _world.GetPool<MoveToCoordinateComponent>();
            _poolTransform = _world.GetPool<ModelTransformComponent>();
        }

        public void Run(EcsSystems systems)
        {
            
            foreach (var propEntity in _filter)
            {
                ref var directionComponent = ref _poolDirection.Get(propEntity);
                ref var transformComponent = ref _poolTransform.Get(propEntity);

                ref var newPositon = ref directionComponent.coordinate;
                ref var propPosition = ref transformComponent.transform;
                
                propPosition.position = Vector3.Lerp(propPosition.position,
                    newPositon, Time.deltaTime * _shared.speed);
                
                if (Vector3.SqrMagnitude(propPosition.position - newPositon) < 9.99999944E-11f)
                    _poolDirection.Del(propEntity);
            }
        }
    }
}