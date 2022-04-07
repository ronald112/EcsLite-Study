using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Client
{
    sealed class PlayerMovementSystem : IEcsRunSystem, IEcsInitSystem
    {
        [Inject] private NavMeshAgent navMeshAgent;
            
        private EcsWorld _world = null;
        private EcsFilter _filter = null;
        private EcsPool<MoveToCoordinateComponent> _poolDirection = null;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<PlayerTag>().Inc<MoveToCoordinateComponent>().End();
            _poolDirection = _world.GetPool<MoveToCoordinateComponent>();
        }

        public void Run(EcsSystems systems)
        {
            
            foreach (var playerEntity in _filter)
            {
                ref var directionComponent = ref _poolDirection.Get(playerEntity);

                ref var direction = ref directionComponent.toCoordinate;
                
                navMeshAgent.SetDestination(direction);

                _poolDirection.Del(playerEntity);
            }
        }
    }
}