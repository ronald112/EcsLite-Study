using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Client
{
    sealed class MouseRegisterRaycastHitFloorSystem : IEcsInitSystem, IEcsRunSystem
    {
        [Inject]
        private Camera _camera = null;

        private EcsWorld _world = null;
        private EcsPool<MouseRaycastHitFloorResultComponent> _poolDirection = null;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _poolDirection = _world.GetPool<MouseRaycastHitFloorResultComponent>();
        }

        public void Run(EcsSystems systems)
        {
            if (!Input.GetMouseButtonDown(0)) return;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit, 100f, 1 << LayerMask.NameToLayer("Floor"))) return;

            int mouseEntity = _world.NewEntity();
        
            ref var directionComponent = ref _poolDirection.Add(mouseEntity);
            directionComponent.position = hit.point;
        }
    }
}