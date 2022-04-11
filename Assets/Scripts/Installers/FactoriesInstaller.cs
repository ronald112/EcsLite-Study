using Factories;
using Zenject;

namespace Installers
{
    public class FactoriesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IItemFactory>().To<ItemFactory>()
                .AsSingle()
                .NonLazy();
        }
    }
}