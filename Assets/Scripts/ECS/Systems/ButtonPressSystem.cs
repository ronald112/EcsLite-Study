using Leopotam.EcsLite;

namespace Client
{
    sealed class ButtonPressSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsFilter _filterPressedMove = null;
        private EcsPool<PathByTwoPointsComponent> _poolTwoPoints = null;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterPressedMove = _world.Filter<PressedButtonEvent>().Inc<ModelTransformComponent>()
                .Inc<PathByTwoPointsComponent>().End();
            
            _poolTwoPoints = _world.GetPool<PathByTwoPointsComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var buttonEntity in _filterPressedMove)
            {
                ref var newPositionComponent = ref _poolTwoPoints.Get(buttonEntity);
                
                var poolMoveToCoordinate = _world.GetPool<MoveToCoordinateComponent>();
                if(poolMoveToCoordinate.Has(buttonEntity))
                    poolMoveToCoordinate.Del(buttonEntity);
                ref var move = ref poolMoveToCoordinate.Add(buttonEntity);
                move.toCoordinate =  newPositionComponent.end;
            }
        }
    }
}