using System;
using Crossoverse.App.LiveViewer.Context;
using Cysharp.Threading.Tasks;
using MessagePipe;
using VContainer.Unity;

namespace Crossoverse.App.LiveViewer.Presentation.DefaultScreen
{
    public class DefaultScreenPresenter : IInitializable, IDisposable
    {
        private readonly IDisposable _disposable;

        public DefaultScreenPresenter
        (
            DefaultScreenView view,
            ISceneTransitionContext sceneTransitionContext
        )
        {
            var disposableBagBuilder = DisposableBag.CreateBuilder();

            sceneTransitionContext
                .OnLoadingProgressUpdated
                .Subscribe(async value =>
                {
                    view.SetDisplayText($"Loading: {value * 100f} %");
                    if (value >= 1f)
                    {
                        await UniTask.Delay(TimeSpan.FromSeconds(2));
                        view.SetCanvasActiveState(false);
                    }
                })
                .AddTo(disposableBagBuilder);

            _disposable = disposableBagBuilder.Build();
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
