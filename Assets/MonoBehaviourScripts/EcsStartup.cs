using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.UnityEditor;
using UnityEngine;
using Voody.UniLeo.Lite;
using Zenject;

namespace Client {
    sealed class EcsStartup : MonoBehaviour {
        EcsSystems _systems;
        
        private DiContainer _diContainer;

        [Inject]
        public void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        void Start() {
            _systems = new EcsSystems(new EcsWorld(), new SharedConstants());
            
            AddSystems();

            _systems.ConvertScene();
            _systems.Init();
        }

        void Update() {
            _systems?.Run();
        }

        private void AddSystems()
        {
#if UNITY_EDITOR
            _systems.Add(new EcsWorldDebugSystem());
#endif
            
            AddNewSystem<MouseRegisterRaycastHitFloorSystem>();
            AddNewSystem<SpawnPointSystem>();
            AddNewSystem<PlayerInputSystem>();
            AddNewSystem<PlayerMovementSystem>();
            
            AddNewSystem<DoorInitOpeningByButtonPressSystem>();
            AddNewSystem<DoorStopMovingSystem>();
            
            
            AddNewSystem<ButtonPressSystem>();
            AddNewSystem<ButtonUnpressSystem>();
            AddNewSystem<PropMovementSystem>();
            
            AddNewSystem<DoorFinishMovingSystem>();
            AddNewSystem<NavMeshRebuildSystem>();
            
            
            _systems.DelHere<PressedButtonEventComponent>();
            _systems.DelHere<UnpressedButtonEventComponent>();
            _systems.DelHere<MouseRaycastHitFloorResultComponent>();
            _systems.DelHere<NavMeshSurfaceNeedsRebuildEventComponent>();
            _systems.DelHere<FinishedMovingEvent>();
        }
        
        public void AddNewSystem<T>() where T : IEcsSystem
        {
            _systems.Add(_diContainer.Instantiate<T>());
        }

        void OnDestroy() {
            if (_systems != null) {
                _systems.Destroy();
                // add here cleanup for custom worlds, for example:
                // _systems.GetWorld ("events").Destroy ();
                _systems.GetWorld().Destroy();
                _systems = null;
            }
        }
    }
}