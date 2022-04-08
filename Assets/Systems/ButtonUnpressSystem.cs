using Leopotam.EcsLite;

namespace Client
{
    sealed class ButtonUnpressSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsFilter _filterPressedMove = null;
        private EcsPool<MoveByTwoPointsComponent> _poolTwoPoints = null;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterPressedMove = _world.Filter<UnpressedButtonTagComponent>()
                .Inc<MoveByTwoPointsComponent>().Exc<MoveToCoordinateComponent>().End();
            
            _poolTwoPoints = _world.GetPool<MoveByTwoPointsComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var buttonEntity in _filterPressedMove)
            {
                ref var newPositionComponent = ref _poolTwoPoints.Get(buttonEntity);

                _world.GetPool<UnpressedButtonTagComponent>().Del(buttonEntity);
                ref var move = ref _world.GetPool<MoveToCoordinateComponent>().Add(buttonEntity);
                ref var modelTransform = ref _world.GetPool<ModelTransformComponent>().Get(buttonEntity);
                move.toCoordinate =  newPositionComponent.start;
                move.fromCoordinate = modelTransform.transform.position;
            }
        }
    }
}