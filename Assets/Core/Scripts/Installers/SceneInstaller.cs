namespace Zenject
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlacementManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<RoadManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<RoadFixer>().FromComponentInHierarchy().AsSingle();
            Container.Bind<InputManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<StructureManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ResourcesManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}
