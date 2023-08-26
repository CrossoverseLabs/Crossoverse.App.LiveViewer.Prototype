using System;
using Crossoverse.App.LiveViewer.Configuration;
using Crossoverse.App.LiveViewer.Tests.Context;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Crossoverse.App.LiveViewer.Tests.Presentation
{
    public class DefaultScreenSceneTest
    {
        private readonly SceneTransitionContextMock _contextMock;

        public DefaultScreenSceneTest(SceneTransitionContextMock sceneTransitionContextMock)
        {
            _contextMock = sceneTransitionContextMock;
        }

        public async UniTask StartAsync()
        {
            Debug.Log($"[{nameof(DefaultScreenSceneTest)} Start");

            await _contextMock.LoadStageAsync(StageName.TitleStage);

            Debug.Log($"[{nameof(DefaultScreenSceneTest)} End");
        }
    }
}
