using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    sealed class PlayerMovementSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _world = null;
        private EcsFilter _filter = null;
        private EcsPool<MoveToCoordinateComponent> _poolDirection = null;
        private EcsPool<NavMeshAgentComponent> _poolNavMeshAgent = null;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<PlayerTag>().Inc<MoveToCoordinateComponent>().End();
            _poolDirection = _world.GetPool<MoveToCoordinateComponent>();
            _poolNavMeshAgent = _world.GetPool<NavMeshAgentComponent>();
        }

        public void Run(EcsSystems systems)
        {
            
            foreach (var playerEntity in _filter)
            {
                ref var directionComponent = ref _poolDirection.Get(playerEntity);
                ref var navMeshComponent = ref _poolNavMeshAgent.Get(playerEntity);

                ref var direction = ref directionComponent.toCoordinate;
                
                navMeshComponent.agent.SetDestination(direction);

                _poolDirection.Del(playerEntity);
            }
        }
    }
}