using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using Zenject;

namespace Installers
{
    public class NavMeshInstaller : MonoInstaller
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private NavMeshSurface _navMeshSurface;
        [SerializeField] private ThirdPersonCharacter _character;
        public override void InstallBindings()
        {
            Container.Bind<NavMeshAgent>().FromInstance(_agent).AsSingle().NonLazy();
            Container.Bind<NavMeshSurface>().FromInstance(_navMeshSurface).AsSingle().NonLazy();
            Container.Bind<ThirdPersonCharacter>().FromInstance(_character).AsSingle().NonLazy();
        }
    }
}
