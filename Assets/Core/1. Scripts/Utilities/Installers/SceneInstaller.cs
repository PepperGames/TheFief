using UnityEngine;

namespace Zenject
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Services _services;

        public override void InstallBindings()
        {
            Container.BindInstance(_services).AsSingle();
        }
    }
}
