using Leopotam.EcsLite;

namespace Client
{
    sealed class DoorFinishMovingSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsFilter _filterFinishedMovingDoor = null;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filterFinishedMovingDoor = _world.Filter<DoorTag>().Inc<FinishedMovingEvent>().End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var doorEntity in _filterFinishedMovingDoor)
            {
                _world.GetPool<DoorReadyToMoveTag>().Del(doorEntity);
                _world.GetPool<DoorOpenedTag>().Add(doorEntity);
            }
        }
    }
}