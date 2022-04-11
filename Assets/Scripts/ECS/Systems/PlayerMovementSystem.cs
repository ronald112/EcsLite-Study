using Leopotam.EcsLite;

namespace Client
{
    sealed class PlayerMovementSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _world = null;
        private EcsFilter _filter = null;
        private EcsPool<MoveToCoordinateComponent> _poolDirection = null;
        private EcsPool<PlayerNavMeshAgentComponent> _poolAgent = null;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<PlayerTag>().Inc<MoveToCoordinateComponent>()
                .Inc<PlayerNavMeshAgentComponent>().End();
            _poolDirection = _world.GetPool<MoveToCoordinateComponent>();
            _poolAgent = _world.GetPool<PlayerNavMeshAgentComponent>();
        }

        public void Run(EcsSystems systems)
        {
            
            foreach (var playerEntity in _filter)
            {
                ref var directionComponent = ref _poolDirection.Get(playerEntity);

                ref var direction = ref directionComponent.toCoordinate;

                ref var navMeshAgent = ref _poolAgent.Get(playerEntity);
                navMeshAgent.navMeshAgent.SetDestination(direction);

                _poolDirection.Del(playerEntity);
            }
        }
    }
}