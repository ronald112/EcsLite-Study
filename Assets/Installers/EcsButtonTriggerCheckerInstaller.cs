using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EcsButtonTriggerCheckerInstaller : MonoInstaller
{
    [SerializeField] private string _target;
    public override void InstallBindings()
    {
        Container.Bind<string>().FromInstance(_target)
            .AsSingle().NonLazy();
    }
}
