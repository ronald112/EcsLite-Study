using Leopotam.EcsLite;

namespace Client
{
    sealed class ButtonTriggerEnterSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsFilter _filterPressedMove = null;
        private EcsPool<PressedButtonEvent> _poolPressedButtonEvent = null;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterPressedMove = _world.Filter<ButtonTag>().Inc<OnTriggerEnterEvent>().End();
            
            _poolPressedButtonEvent = _world.GetPool<PressedButtonEvent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var entity in _filterPressedMove)
            {
                if (!_poolPressedButtonEvent.Has(entity))
                {
                    _poolPressedButtonEvent.Add(entity);
                }
            }
        }
    }
}