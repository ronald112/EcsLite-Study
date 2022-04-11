using Leopotam.EcsLite;

namespace Client
{
    sealed class PlayerInputSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _world = null;
        private EcsFilter _filter = null;
        private EcsPool<MoveToCoordinateComponent> _poolDirection = null;
        private EcsFilter _filterMouseFloorHit = null;
        private EcsPool<MouseRaycastHitFloorResultComponent> _poolMouseFloorHit = null;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterMouseFloorHit = _world.Filter<MouseRaycastHitFloorResultComponent>().End();
            _filter = _world.Filter<PlayerTag>().End();
            _poolDirection = _world.GetPool<MoveToCoordinateComponent>();
            _poolMouseFloorHit = _world.GetPool<MouseRaycastHitFloorResultComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var mouseHit in _filterMouseFloorHit)
            {
                foreach (var player in _filter)
                {
                    ref var directionComponent = ref _poolDirection.Add(player);
                    directionComponent.toCoordinate = _poolMouseFloorHit.Get(mouseHit).position;
                }
            }

        }
    }
}

    