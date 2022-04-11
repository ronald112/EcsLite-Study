using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    sealed class PlayerMovementAnimationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsFilter _filter = null;
        private EcsPool<PlayerNavMeshAgentComponent> _poolAgent = null;
        private EcsPool<ThirdPersonCharacterComponent> _poolThirdPersonCharacter = null;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<PlayerTag>()
                .Inc<PlayerNavMeshAgentComponent>().Inc<ThirdPersonCharacterComponent>().End();
            _poolAgent = _world.GetPool<PlayerNavMeshAgentComponent>();
            _poolThirdPersonCharacter = _world.GetPool<ThirdPersonCharacterComponent>();

            foreach (var entityPlayer in _filter)
            {
                ref var navMeshAgent = ref _poolAgent.Get(entityPlayer).navMeshAgent;
                navMeshAgent.updateRotation = false;
            }
        }

        public void Run(EcsSystems systems)
        {
            foreach (var entityPlayer in _filter)
            {
                ref var navMeshAgent = ref _poolAgent.Get(entityPlayer).navMeshAgent;
                ref var thirdPersonCharacter = ref _poolThirdPersonCharacter.Get(entityPlayer).character;
                thirdPersonCharacter.Move(
                    navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance
                        ? navMeshAgent.desiredVelocity
                        : Vector3.zero, false, false);
            }
        }
    }
}