using Leopotam.EcsLite;

namespace Client
{
    sealed class MouseUnregisterRaycastHitFloorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsFilter _filter = null;
        private EcsPool<MouseRaycastHitFloorResultComponent> _poolMouseHit = null;
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<MouseRaycastHitFloorResultComponent>().End();
            _poolMouseHit = _world.GetPool<MouseRaycastHitFloorResultComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var mouseHit in _filter)
            {
                _poolMouseHit.Del(mouseHit);
            }
        }
    }
}