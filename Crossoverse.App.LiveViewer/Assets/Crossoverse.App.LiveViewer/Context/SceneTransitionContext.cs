using System;
using Crossoverse.App.LiveViewer.Configuration;
using Crossoverse.Toolkit.SceneTransition;

namespace Crossoverse.App.LiveViewer.Context
{
    public sealed class SceneTransitionContext : SceneTransitionContext<StageName, SceneName>
    {
        public SceneTransitionContext(SceneConfiguration sceneConfiguration) : base(sceneConfiguration)
        {
        }
    }
}
