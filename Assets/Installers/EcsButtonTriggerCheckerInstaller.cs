using UnityEngine;
using Zenject;

namespace Installers
{
    public class EcsButtonTriggerCheckerInstaller : MonoInstaller
    {
        [SerializeField] private string _target;
        public override void InstallBindings()
        {
            Container.Bind<string>().FromInstance(_target)
                .AsSingle().NonLazy();
        }
    }
}