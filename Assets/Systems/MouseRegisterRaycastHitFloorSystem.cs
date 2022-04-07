using Leopotam.EcsLite;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Client
{
    sealed class MouseRegisterRaycastHitFloorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private Camera Cam = null;

        private EcsWorld _world = null;
        private EcsFilter _filter = null;
        private EcsPool<MouseRaycastHitFloorResultComponent> _poolDirection = null;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<MouseRaycastHitFloorResultComponent>().End();
            _poolDirection = _world.GetPool<MouseRaycastHitFloorResultComponent>();

            var cameraPool = _world.GetPool<CameraComponent>();
            var cameraFilter = _world.Filter<CameraComponent>().End();
            foreach (var cameraEntity in cameraFilter)
            {
                ref var camera = ref cameraPool.Get(cameraEntity);
                Cam = camera.camera;
            }
        }

        public void Run(EcsSystems systems)
        {
            if (!Input.GetMouseButtonDown(0)) return;
            Ray ray = Cam.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit, 100f, 1 << LayerMask.NameToLayer("Floor"))) return;

            int mouseEntity = _world.NewEntity();
        
            ref var directionComponent = ref _poolDirection.Add(mouseEntity);
            directionComponent.position = hit.point;
        }
    }
}