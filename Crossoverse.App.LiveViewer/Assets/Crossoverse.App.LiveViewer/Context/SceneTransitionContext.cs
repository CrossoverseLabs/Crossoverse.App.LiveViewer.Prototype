using System;
using Crossoverse.App.LiveViewer.Configuration;
using Crossoverse.Toolkit.SceneTransition;
using Cysharp.Threading.Tasks;
using MessagePipe;

namespace Crossoverse.App.LiveViewer.Context
{
    public interface ISceneTransitionContext
    {
        ISubscriber<float> OnLoadingProgressUpdated { get; }
        UniTask LoadStageAsync(StageName nextStageId, TimeSpan delayTimeOfSwitchingActiveScene = default);
        UniTask LoadGlobalScenesAndInitialStageAsync(TimeSpan delayTimeOfSwitchingActiveScene = default);
    }

    public sealed class SceneTransitionContext : SceneTransitionContext<StageName, SceneName>, ISceneTransitionContext
    {
        public ISubscriber<float> OnLoadingProgressUpdated { get; }

        private readonly IDisposablePublisher<float> _loadingProgressPublisher;

        public SceneTransitionContext
        (
            SceneConfiguration sceneConfiguration,
            EventFactory eventFactory
        ) : base(sceneConfiguration)
        {
            (_loadingProgressPublisher, OnLoadingProgressUpdated) = eventFactory.CreateEvent<float>();
        }

        public async UniTask LoadStageAsync(StageName nextStageId, TimeSpan delayTimeOfSwitchingActiveScene = default)
        {
            foreach (var stage in _stages)
            {
                if (stage.StageId == nextStageId)
                {
                    await LoadStageAsync(stage, progress: Progress.Create<float>(value => 
                    {
                        UnityEngine.Debug.Log($"<color=lime>[{nameof(SceneTransitionContext)}] Stage loading progress: {value}</color>");
                        _loadingProgressPublisher.Publish(value);
                    }));
                    return;
                }
            }
        }

        public async UniTask LoadGlobalScenesAndInitialStageAsync(TimeSpan delayTimeOfSwitchingActiveScene = default)
        {
            await base.LoadGlobalScenesAndInitialStageAsync(delayTimeOfSwitchingActiveScene, Progress.Create<float>(value => 
            {
                UnityEngine.Debug.Log($"<color=lime>[{nameof(SceneTransitionContext)}] Stage loading progress: {value}</color>");
                _loadingProgressPublisher.Publish(value);
            }));
        }
    }
}
