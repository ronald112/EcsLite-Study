using Leopotam.EcsLite;

namespace Client
{
    sealed class ButtonPressSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsFilter _filterPressedMove = null;
        private EcsPool<MoveByTwoPointsComponent> _poolTwoPoints = null;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterPressedMove = _world.Filter<PressedButtonTagComponent>().Inc<ModelTransformComponent>()
                .Inc<MoveByTwoPointsComponent>().Exc<MoveToCoordinateComponent>().End();
            
            _poolTwoPoints = _world.GetPool<MoveByTwoPointsComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var buttonEntity in _filterPressedMove)
            {
                ref var newPositionComponent = ref _poolTwoPoints.Get(buttonEntity);
                
                ref var move = ref _world.GetPool<MoveToCoordinateComponent>().Add(buttonEntity);
                ref var modelTransform = ref _world.GetPool<ModelTransformComponent>().Get(buttonEntity);
                move.toCoordinate =  newPositionComponent.end;
                move.fromCoordinate = modelTransform.transform.position;
                
                _world.GetPool<PressedButtonTagComponent>().Del(buttonEntity);
            }
        }
    }
}