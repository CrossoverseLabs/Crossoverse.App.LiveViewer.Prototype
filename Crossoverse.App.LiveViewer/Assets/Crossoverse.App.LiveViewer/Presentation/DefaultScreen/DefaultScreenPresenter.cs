using System;
using Crossoverse.App.LiveViewer.Context;
using MessagePipe;

namespace Crossoverse.App.LiveViewer.Presentation.DefaultScreen
{
    public class DefaultScreenPresenter : IDisposable
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
                .Subscribe(value =>
                {
                    view.SetText($"Loading: {value * 100f} %");
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
