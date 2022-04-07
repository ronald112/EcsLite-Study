using System;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Client
{
    sealed class NavMeshRebuildSystem : IEcsInitSystem, IEcsRunSystem
    {
        [Inject]
        private NavMeshSurface _navMeshSurface = null;

        private EcsWorld _world = null;
        private EcsFilter _filter = null;
        private EcsPool<NavMeshSurfaceNeedsRebuildTagComponent> _ecsPool = null;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<NavMeshSurfaceNeedsRebuildTagComponent>().End();
            _ecsPool = _world.GetPool<NavMeshSurfaceNeedsRebuildTagComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                _navMeshSurface.RemoveData();
                _navMeshSurface.BuildNavMesh();
                _ecsPool.Del(entity);
            }
        }
    }
}