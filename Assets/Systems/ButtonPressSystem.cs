using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    sealed class ButtonPressSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsFilter _filter = null;
        private EcsPool<MoveToCoordinateComponent> _poolDirection = null;
        private EcsPool<ModelColorComponent> _poolModelColor = null;
        private EcsPool<ModelBoundsComponent> _poolModelBounds = null;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<PressedButtonTagComponent>().Inc<ModelColorComponent>()
                .Inc<MoveToCoordinateComponent>().End();
            _poolDirection = _world.GetPool<MoveToCoordinateComponent>();
            _poolModelColor = _world.GetPool<ModelColorComponent>();
            _poolModelBounds = _world.GetPool<ModelBoundsComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var buttonEntity in _filter)
            {
                ref ModelColor color = ref _poolModelColor.Get(buttonEntity).color;
                ref Vector3 bounds = ref _poolModelBounds.Get(buttonEntity).bounds;
                
                ref var newPositionComponent = ref _poolDirection.Add(buttonEntity);
                newPositionComponent.coordinate = bounds;
                newPositionComponent.coordinate.y -= bounds.y * 0.9f;

            }
        }
    }
}