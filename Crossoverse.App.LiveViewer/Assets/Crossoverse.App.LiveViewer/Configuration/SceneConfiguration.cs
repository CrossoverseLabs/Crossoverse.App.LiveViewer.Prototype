using System;
using System.Collections.Generic;
using Crossoverse.Toolkit.SceneTransition;
using UnityEngine;

namespace Crossoverse.App.LiveViewer.Configuration
{
    [Serializable]
    [CreateAssetMenu(menuName = "Crossoverse/App/LiveViewer/Create SceneConfiguration", fileName = "SceneConfiguration")]
    public sealed class SceneConfiguration : ScriptableObject, ISceneConfiguration<StageName, SceneName>
    {
        [SerializeField] SceneName _initialActiveScene;
        [SerializeField] List<SceneName> _globalScenes = new();

        [Header("Stage Transition")]
        [SerializeField] List<Stage<StageName, SceneName>> _stages = new();

        public SceneName InitialActiveScene => _initialActiveScene;
        public List<SceneName> GlobalScenes => _globalScenes;
        public List<Stage<StageName, SceneName>> Stages => _stages;
    }
}
