using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Client {
    class SharedConstants
    {
        public float speed = 1;
        public int accuracy = 1;
    }
    sealed class EcsStartup : MonoBehaviour {
        EcsSystems _systems;

        void Start () {        
            // register your shared data here, for example:
            // var shared = new Shared ();
            // systems = new EcsSystems (new EcsWorld (), shared);
            _systems = new EcsSystems (new EcsWorld (), new SharedConstants());
            
            AddSystems();

            _systems.ConvertScene();
            _systems.Init ();
        }

        void Update () {
            _systems?.Run ();
        }

        private void AddSystems()
        {
#if UNITY_EDITOR
            // add debug systems for custom worlds here, for example:
            // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
            _systems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif
            _systems
                .Add(new PlayerMovementSystem())
                .Add(new PlayerInputSystem())
                ;
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                // add here cleanup for custom worlds, for example:
                // _systems.GetWorld ("events").Destroy ();
                _systems.GetWorld ().Destroy ();
                _systems = null;
            }
        }
    }
}