using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Installers
{
    public class NavMeshInstaller : MonoInstaller
    {
        [SerializeField] private NavMeshSurface _navMeshSurface;
        public override void InstallBindings()
        {
            Container.Bind<NavMeshSurface>().FromInstance(_navMeshSurface).AsSingle().NonLazy();
        }
    }
}
