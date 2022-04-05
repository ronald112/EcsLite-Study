using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class NavMeshAgentInstaller : MonoInstaller
{
    [SerializeField] private NavMeshAgent agent;
    public override void InstallBindings()
    {
        Container.Bind<NavMeshAgent>().FromInstance(agent);
    }
}
