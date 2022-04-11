using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    sealed class MovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsFilter _filter = null;
        private EcsPool<MoveToCoordinateComponent> _poolMovePath = null;
        private EcsPool<ModelTransformComponent> _poolTransform = null;
        private SharedConstants _shared = null;

        private EcsPool<FinishedMovingEvent> _poolFinishedMoving = null;
        private EcsPool<SpeedComponent> _poolMovementSpeed = null;

        public void Init(EcsSystems systems)
        {
            _shared = systems.GetShared<SharedConstants>();
            _world = systems.GetWorld();
            _filter = _world.Filter<MovableComponent>().Inc<MoveToCoordinateComponent>()
                .Inc<ModelTransformComponent>().End();
            _poolMovePath = _world.GetPool<MoveToCoordinateComponent>();
            _poolTransform = _world.GetPool<ModelTransformComponent>();
            _poolFinishedMoving = _world.GetPool<FinishedMovingEvent>();
            _poolMovementSpeed = _world.GetPool<SpeedComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var propEntity in _filter)
            {
                ref var movePathComponent = ref _poolMovePath.Get(propEntity);
                ref var transformComponent = ref _poolTransform.Get(propEntity);

                var movementSpeed = _shared.defaultSpeed;
                if (_poolMovementSpeed.Has(propEntity))
                {
                    movementSpeed = _poolMovementSpeed.Get(propEntity).speed;
                }
                
                transformComponent.transform.position = Vector3.MoveTowards(transformComponent.transform.position,
                movePathComponent.toCoordinate, 
                Time.deltaTime * movementSpeed);
                
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