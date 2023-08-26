using System;
using UnityEngine;

namespace Crossoverse.App.LiveViewer.Configuration
{
    [Serializable]
    [CreateAssetMenu(menuName = "Crossoverse/App/LiveViewer/Create EngineConfiguration", fileName = "EngineConfiguration")]
    public sealed class EngineConfiguration : ScriptableObject
    {
        public int TargetFrameRate = 60;
        public bool EnableDebugLogger = true;
    }
}
