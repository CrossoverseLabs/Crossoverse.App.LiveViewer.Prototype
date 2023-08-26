using Crossoverse.App.LiveViewer.Presentation.DefaultScreen;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;
using UnityEngine;
#if UNITY_EDITOR
using Crossoverse.App.LiveViewer.Tests.Context;
using Crossoverse.App.LiveViewer.Tests.Presentation;
using MessagePipe;
#endif

namespace Crossoverse.App.LiveViewer
{
    public class DefaultScreenLifecycle : LifetimeScope
    {
        [SerializeField] private DefaultScreenView defaultScreenView;

#if UNITY_EDITOR
        [Header("Scene Test")]
        [SerializeField] private GameObject _sceneTestOnlyObjectRoot;

        private bool _isSceneTestMode = false;
#endif

        protected override void Configure(IContainerBuilder builder)
        {
            if (Parent != null) // Run as an additive scene of multiple scenes.
            {
                Debug.Log($"[{nameof(DefaultScreenLifecycle)}] Run as an additive scene of multiple scenes.");
                ConfigureCore(builder);
            }
#if UNITY_EDITOR
            else // Run as a single scene for testing or debugging.
            {
                Debug.Log($"[{nameof(DefaultScreenLifecycle)}] Run as a single scene for testing or debugging.");

                ConfigureCore(builder);
                ConfigureSceneTestMode(builder);

                _isSceneTestMode = true;
                _sceneTestOnlyObjectRoot.SetActive(true);
            }
#endif
        }
 
        private void ConfigureCore(IContainerBuilder builder)
        {
            builder.RegisterComponent(defaultScreenView);
            builder.RegisterEntryPoint<DefaultScreenPresenter>().AsSelf();
        }

        private async UniTask Start()
        {
#if UNITY_EDITOR
            if (_isSceneTestMode) await StartSceneTestAsync();
#endif
        }

#if UNITY_EDITOR
        private void ConfigureSceneTestMode(IContainerBuilder builder)
        {   
            builder.RegisterMessagePipe();
            builder.Register<SceneTransitionContextMock>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }

        private async UniTask StartSceneTestAsync()
        {
            var context = Container.Resolve<SceneTransitionContextMock>();
            var sceneTest = new DefaultScreenSceneTest(context);
            await sceneTest.StartAsync();
        }
#endif
    }
}