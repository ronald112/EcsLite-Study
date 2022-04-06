using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    sealed class PropMovementSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private EcsWorld _world = null;
        private EcsFilter _filter = null;
        private EcsPool<MoveToCoordinateComponent> _poolMovePath = null;
        private EcsPool<ModelTransformComponent> _poolTransform = null;
        private SharedConstants _shared = null;

        private Dictionary<int, float> _propStartMovementTime = null;

        public void Init(EcsSystems systems)
        {
            _shared = systems.GetShared<SharedConstants>();
            _world = systems.GetWorld();
            _filter = _world.Filter<MovablePropComponent>().Inc<MoveToCoordinateComponent>()
                .Inc<ModelTransformComponent>().End();
            _poolMovePath = _world.GetPool<MoveToCoordinateComponent>();
            _poolTransform = _world.GetPool<ModelTransformComponent>();
            _propStartMovementTime = new Dictionary<int, float>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var propEntity in _filter)
            {
                ref var movePathComponent = ref _poolMovePath.Get(propEntity);
                ref var transformComponent = ref _poolTransform.Get(propEntity);

                if (!_propStartMovementTime.ContainsKey(propEntity))
                {
                    _propStartMovementTime.Add(propEntity, Time.time);
                }
                
                transformComponent.transform.position = Vector3.Lerp(movePathComponent.fromCoordinate,
                    movePathComponent.toCoordinate, 
                    (Time.time - _propStartMovementTime[propEntity]) * _shared.speed);
                
                if (Vector3.SqrMagnitude(transformComponent.transform.position
                                         - movePathComponent.toCoordinate) < _shared.epsilon)
                {
                    _poolMovePath.Del(propEntity);
                    _propStartMovementTime.Remove(propEntity);
                }
            }
        }

        public void Destroy(EcsSystems systems)
        {
            _propStartMovementTime.Clear();
            _propStartMovementTime = null;
        }
    }
}