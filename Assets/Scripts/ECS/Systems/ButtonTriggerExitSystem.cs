using Leopotam.EcsLite;

namespace Client
{
    sealed class ButtonTriggerExitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsFilter _filterPressedMove = null;
        private EcsPool<UnpressedButtonEvent> _poolUnpressedButtonEvent = null;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterPressedMove = _world.Filter<ButtonTag>().Inc<OnTriggerExitEvent>().End();
            
            _poolUnpressedButtonEvent = _world.GetPool<UnpressedButtonEvent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var entity in _filterPressedMove)
            {
                if (!_poolUnpressedButtonEvent.Has(entity))
                {
                    _poolUnpressedButtonEvent.Add(entity);
                }
            }
        }
    }
}