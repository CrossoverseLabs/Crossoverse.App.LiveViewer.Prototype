using Crossoverse.App.LiveViewer.Configuration;
using Crossoverse.App.LiveViewer.Context;
using Cysharp.Threading.Tasks;
using MessagePipe;

namespace Crossoverse.App.LiveViewer.Tests.Context
{
    public class SceneTransitionContextMock : ISceneTransitionContext
    {
        public ISubscriber<float> OnLoadingProgressUpdated { get; }

        private readonly IDisposablePublisher<float> _loadingProgressPublisher;

        public SceneTransitionContextMock(EventFactory eventFactory)
        {
            (_loadingProgressPublisher, OnLoadingProgressUpdated) = eventFactory.CreateEvent<float>();
        }

        public async UniTask LoadStageAsync(StageName nextStageId)
        {
            UnityEngine.Debug.Log($"[{nameof(SceneTransitionContextMock)}] LoadStageAsync: {nextStageId}");

            var loopCount  = 10;
            for (var i = 1; i <= loopCount; i++)
            {
                await UniTask.Delay(500);
                _loadingProgressPublisher.Publish((float)i / loopCount);
            }
        }

        public async UniTask LoadGlobalScenesAndInitialStageAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
