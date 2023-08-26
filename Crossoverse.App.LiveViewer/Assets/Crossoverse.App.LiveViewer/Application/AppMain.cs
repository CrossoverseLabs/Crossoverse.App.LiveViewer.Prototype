using Crossoverse.App.LiveViewer.Configuration;
using Crossoverse.App.LiveViewer.Context;
using MessagePipe;
using VContainer;
using VContainer.Unity;
using UnityEngine;

namespace Crossoverse.App.LiveViewer
{
    public class AppMain : LifetimeScope
    {
        [Header("Crossoverse.App.LiveViewer")]

        [SerializeField] private EngineConfiguration _engineConfiguration;
        [SerializeField] private SceneConfiguration _sceneConfiguration;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterMessagePipe();
            builder.RegisterInstance(_engineConfiguration);
            builder.RegisterInstance(_sceneConfiguration);
            builder.Register<ApplicationContext>(Lifetime.Singleton);
            builder.Register<SceneTransitionContext>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }

        private async void Start()
        {
            var applicationContext = Container.Resolve<ApplicationContext>();
            var sceneTransitionContext = Container.Resolve<SceneTransitionContext>();

            applicationContext.Initialize();
            await sceneTransitionContext.LoadGlobalScenesAndInitialStageAsync();
        }
    }
}
