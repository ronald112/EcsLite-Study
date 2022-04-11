using Leopotam.EcsLite;
using UnityEngine.AI;
using Zenject;

namespace Client
{
    sealed class PlayerMovementSystem : IEcsRunSystem, IEcsInitSystem
    {
        private NavMeshAgent _navMeshAgent = null;
            
        private EcsWorld _world = null;
        private EcsFilter _filter = null;
        private EcsPool<MoveToCoordinateComponent> _poolDirection = null;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<PlayerTag>().Inc<MoveToCoordinateComponent>().End();
            _poolDirection = _world.GetPool<MoveToCoordinateComponent>();

            var fliterAgent = _world.Filter<PlayerNavMeshAgentComponent>().End();
            var poolAgent = _world.GetPool<PlayerNavMeshAgentComponent>();
            foreach (var playerAgentEntity in fliterAgent)
            {
                ref var navMeshAgent = ref poolAgent.Get(playerAgentEntity);
                _navMeshAgent = navMeshAgent.navMeshAgent;
            }
        }

        public void Run(EcsSystems systems)
        {
            
            foreach (var playerEntity in _filter)
            {
                ref var directionComponent = ref _poolDirection.Get(playerEntity);

                ref var direction = ref directionComponent.toCoordinate;
                
                _navMeshAgent.SetDestination(direction);

                _poolDirection.Del(playerEntity);
            }
        }
    }
}