using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    sealed class PropMovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsFilter _filter = null;
        private EcsPool<MoveToCoordinateComponent> _poolMovePath = null;
        private EcsPool<ModelTransformComponent> _poolTransform = null;
        private SharedConstants _shared = null;

        private EcsPool<FinishedMovingEvent> _poolFinishedMoving = null;

        public void Init(EcsSystems systems)
        {
            _shared = systems.GetShared<SharedConstants>();
            _world = systems.GetWorld();
            _filter = _world.Filter<MovablePropComponent>().Inc<MoveToCoordinateComponent>()
                .Inc<ModelTransformComponent>().End();
            _poolMovePath = _world.GetPool<MoveToCoordinateComponent>();
            _poolTransform = _world.GetPool<ModelTransformComponent>();
            _poolFinishedMoving = _world.GetPool<FinishedMovingEvent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var propEntity in _filter)
            {
                ref var movePathComponent = ref _poolMovePath.Get(propEntity);
                ref var transformComponent = ref _poolTransform.Get(propEntity);


                transformComponent.transform.position = Vector3.MoveTowards(transformComponent.transform.position,
                    movePathComponent.toCoordinate, 
                    Time.deltaTime * _shared.speed);
                
                if (Vector3.SqrMagnitude(transformComponent.transform.position
                                         - movePathComponent.toCoordinate) < _shared.epsilon)
                {
                    _poolMovePath.Del(propEntity);
                    _poolFinishedMoving.Add(propEntity);
                }
            }
        }
    }
}