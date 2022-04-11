using Leopotam.EcsLite;

namespace Client
{
    sealed class DoorStopMovingSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsFilter _filterMovingDoor = null;
        private EcsPool<MoveToCoordinateComponent> _poolMoveToCoordinate = null;
        private EcsFilter _filterUnpressedButton = null;
        private EcsPool<ModelColorComponent> _poolModelColor = null;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filterMovingDoor = _world.Filter<DoorTag>().Inc<MoveToCoordinateComponent>()
                .Inc<ModelColorComponent>().End();
            _filterUnpressedButton = _world.Filter<UnpressedButtonEvent>().Inc<ModelColorComponent>().End();
            
            _poolMoveToCoordinate = _world.GetPool<MoveToCoordinateComponent>();
            _poolModelColor = _world.GetPool<ModelColorComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var entityButton in _filterUnpressedButton)
            {
                foreach (var entityDoor in _filterMovingDoor)
                {
                    if (_poolModelColor.Get(entityButton).color == _poolModelColor.Get(entityDoor).color)
                    {
                        _poolMoveToCoordinate.Del(entityDoor);
                    }
                }
            }
        }
    }
}