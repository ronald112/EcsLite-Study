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
            
            AddNewSystem<ButtonTriggerEnterSystem>();
            AddNewSystem<ButtonTriggerExitSystem>();
            
            AddNewSystem<PlayerMovementAnimationSystem>();
            AddNewSystem<PlayerInputSystem>();
            AddNewSystem<PlayerMovementSystem>();

            AddNewSystem<ButtonPressSystem>();
            AddNewSystem<ButtonUnpressSystem>();
            
            AddNewSystem<DoorInitOpeningByButtonPressSystem>();
            AddNewSystem<DoorStopMovingSystem>();
            
            AddNewSystem<DoorFinishMovingSystem>();
            
            AddNewSystem<MovementSystem>();

            _systems.DelHere<PressedButtonEvent>();
            _systems.DelHere<UnpressedButtonEvent>();
            _systems.DelHere<MouseRaycastHitFloorResultComponent>();
            _systems.DelHere<FinishedMovingEvent>();
            _systems.DelHere<OnTriggerEnterEvent>();
            _systems.DelHere<OnTriggerExitEvent>();
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