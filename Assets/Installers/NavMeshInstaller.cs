using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Installers
{
    public class NavMeshInstaller : MonoInstaller
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private NavMeshSurface _navMeshSurface;
        public override void InstallBindings()
        {
            Container.Bind<NavMeshAgent>().FromInstance(_agent).AsSingle().NonLazy();
            Container.Bind<NavMeshSurface>().FromInstance(_navMeshSurface).AsSingle().NonLazy();
        }
    }
}
