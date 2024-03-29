﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Assets.PixelCrew.Components.LevelManagment;
using Assets.PixelCrew.Model.Data;
using Assets.PixelCrew.Model.Definitions.Player;
using Assets.PixelCrew.Model.Models;
using Assets.PixelCrew.Utils.Disposables;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

namespace Assets.PixelCrew.Model {

    public class GameSession : MonoBehaviour {

        [SerializeField] private int _levelIndex;
        [SerializeField] private PlayerData _data;
        [SerializeField] private string _defaultCheckpoint;

        public static  GameSession Instance { get; private set; }
        public PlayerData Data => _data;
        public QuickInventoryModel QuickInventory { get; private set; }
        public PerksModel PerksModel { get; private set; }
        public StatsModel StatsModel { get; private set; }

        private PlayerData _save;
        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private readonly List<string> _checkpoints = new List<string>();

        private void Awake() {

            var existSession = GetExistSession();

            if (existSession != null) {
                existSession.StartSession(_defaultCheckpoint, _levelIndex);
                Destroy(gameObject);
            }
            else {
                SavePlayerData(); // ??
                InitModels();
                DontDestroyOnLoad(this);
                Instance = this;
                StartSession(_defaultCheckpoint, _levelIndex);
            }
        }

        private void StartSession(string defaultCheckpoint, int levelIndex) {
            SetChecked(defaultCheckpoint);
            TrackSessionStart(levelIndex);
            LoadUIs();
            SpawnHero();
        }

        private void TrackSessionStart(int levelIndex) {
            var eventParams = new Dictionary<string, object> {
                { "level_index", levelIndex }
            };
            AnalyticsEvent.Custom("level_start", eventParams);
        }

        private void SpawnHero() {
            var checkpoints = FindObjectsOfType<CheckPointComponent>();
            var lastCheckpoint = _checkpoints.Last();
            foreach (var checkpoint in checkpoints) {
                if (checkpoint.Id == lastCheckpoint) {
                    checkpoint.SpawnHero();
                    return;
                }
            }
            checkpoints.First(c => c.Id == _defaultCheckpoint).SpawnHero();
        }

        internal void SetChecked(string id) {
            if (!_checkpoints.Contains(id)) {
                SavePlayerData();
                _checkpoints.Add(id);
            }
        }

        public bool IsChecked(string id) {
            return _checkpoints.Contains(id);
        }

        private void InitModels() {
            QuickInventory = new QuickInventoryModel(Data);
            _trash.Retain(QuickInventory);

            PerksModel = new PerksModel(_data);
            _trash.Retain(PerksModel);

            StatsModel = new StatsModel(_data);
            _trash.Retain(StatsModel);

            _data.Hp.Value = (int)StatsModel.GetValue(StatId.Hp);
        }

        private void LoadUIs() {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
            LoadOnScreenControls();
        }

        [Conditional("USE_ONSCREEN_CONTROLS")]
        private void LoadOnScreenControls() {
            SceneManager.LoadScene("Controls", LoadSceneMode.Additive);
        }

        public void SavePlayerData() {
            _save = _data.Clone();
        }

        public void LoadPlayerData() {
            _data = _save.Clone();
            _trash.Dispose();
            InitModels();
        }

        private GameSession GetExistSession() {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSession in sessions) {
                if (gameSession != this) {
                    return gameSession;
                }
            }
            return null;
        }

        private readonly List<string> _removedItems = new List<string>();

        public bool RestoreState(string itemId) {
            return _removedItems.Contains(itemId);
        }

        public void StoreState(string itemId) {
            if (!_removedItems.Contains(itemId))
                _removedItems.Add(itemId);
        }

        private void OnDestroy() {
            if(Instance == this)
                Instance = null;
            _trash.Dispose();
        }
    }
}