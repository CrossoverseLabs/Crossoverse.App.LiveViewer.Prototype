using Crossoverse.App.LiveViewer.Configuration;

namespace Crossoverse.App.LiveViewer.Context
{
    public sealed class ApplicationContext
    {
        private readonly int _targetFrameRate;
        private readonly bool _enableDebugLogger;

        public ApplicationContext(EngineConfiguration engineConfiguration)
        {
            _targetFrameRate = engineConfiguration.TargetFrameRate;
            _enableDebugLogger = engineConfiguration.EnableDebugLogger;
        }

        public void Initialize()
        {
            UnityEngine.QualitySettings.vSyncCount = 0;
            UnityEngine.Application.targetFrameRate = _targetFrameRate;
            UnityEngine.Debug.unityLogger.logEnabled = _enableDebugLogger;
            UnityEngine.Debug.Log($"[{nameof(ApplicationContext)}] Initialized");
        }
    }
}
