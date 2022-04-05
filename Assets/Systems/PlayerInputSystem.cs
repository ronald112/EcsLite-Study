using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    sealed class PlayerInputSystem : IEcsRunSystem, IEcsInitSystem
    {

        private Camera Cam = null;

        private EcsWorld _world = null;
        private EcsFilter _filter = null;
        private EcsPool<MoveToCoordinateComponent> _poolDirection = null;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<PlayerTag>().End();
            _poolDirection = _world.GetPool<MoveToCoordinateComponent>();

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

            if (!Physics.Raycast(ray, out var hit)) return;
            foreach (var player in _filter)
            {
                ref var directionComponent = ref _poolDirection.Add(player);
                directionComponent.coordinate = hit.point;
            }
        }
    }
}