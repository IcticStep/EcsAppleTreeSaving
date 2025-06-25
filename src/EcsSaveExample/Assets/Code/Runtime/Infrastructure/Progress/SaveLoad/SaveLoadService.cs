using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Infrastructure.Progress.Data;
using Code.Runtime.Infrastructure.Progress.Extensions;
using Code.Runtime.Infrastructure.Progress.Provider;
using JetBrains.Annotations;
using UnityEngine;
using Code.Runtime.Infrastructure.Progress.Recreator;

namespace Code.Runtime.Infrastructure.Progress.SaveLoad
{
    [UsedImplicitly]
    internal sealed class SaveLoadService : ISaveLoadService
    {
        private const string PlayerProgressKey = "PlayerProgress";
        
        private readonly IRecreatorService _recreatorService;
        private readonly GameContext _gameContext;
        private readonly IProgressProvider _progressProvider;

        public bool HasSavedProgress => PlayerPrefs.HasKey(PlayerProgressKey);
        public bool ProgressWasLoaded { get; private set; }

        public SaveLoadService(
            IProgressProvider progressProvider, 
            IRecreatorService recreatorService,
            GameContext gameContext)
        {
            _progressProvider = progressProvider;
            _recreatorService = recreatorService;
            _gameContext = gameContext;
        }
        
        public void SaveProgress()
        {
            ProgressWasLoaded = false;
            PreservePersistantDataEntities();
            ProgressData progressData = _progressProvider.ProgressData;
            string serialized = progressData.ToJson();
            PlayerPrefs.SetString(PlayerProgressKey, serialized);
            PlayerPrefs.Save();
            Debug.Log("Progress saved.");
        }

        public void LoadProgress()
        {
            HydrateProgress(PlayerPrefs.GetString(PlayerProgressKey));
            ProgressWasLoaded = true;
            Debug.Log("Progress loaded.");
        }

        private void HydrateProgress(string serializedProgress)
        {
            ProgressData progressData = serializedProgress.FromJson<ProgressData>();
            _progressProvider.SetProgressData(progressData);
            HydratePersistantDataEntities();
        }

        private void HydratePersistantDataEntities()
        {
            List<EntitySnapshot> gameSnapshots = _progressProvider.ProgressData.GameSnapshots;
            
            foreach (EntitySnapshot metaSnapshot in gameSnapshots)
                _recreatorService.Recreate(metaSnapshot, _gameContext);
        }

        private void PreservePersistantDataEntities() =>
            _progressProvider.ProgressData.GameSnapshots = _gameContext
                .GetEntities()
                .Where(entity => entity.RequiresSaving())
                .Select(entity => entity.AsSavedEntity())
                .ToList();
    }
}