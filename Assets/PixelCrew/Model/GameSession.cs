using System.Collections.Generic;
using System.Linq;
using Assets.PixelCrew.Components.LevelManagment;
using Assets.PixelCrew.Model.Data;
using Assets.PixelCrew.Model.Models;
using Assets.PixelCrew.Utils.Disposables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.PixelCrew.Model {

    public class GameSession : MonoBehaviour {

        [SerializeField] private PlayerData _data;
        [SerializeField] private string _defaultCheckpoint;

        public PlayerData Data => _data;
        public QuickInventoryModel QuickInventory { get; private set; }
        public PerksModel PerksModel { get; private set; }

        private PlayerData _save;
        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private readonly List<string> _checkpoints = new List<string>();

        private void Awake() {
            var existSession = GetExistSession();
            if (existSession != null) {
                existSession.StartSession(_defaultCheckpoint);
                Destroy(gameObject);
            }
            else {
                SavePlayerData();
                InitModels();
                DontDestroyOnLoad(this);
                StartSession(_defaultCheckpoint);
            }
        }

        private void StartSession(string defaultCheckpoint) {
            SetChecked(defaultCheckpoint);
            LoadHud();
            SpawnHero();
        }

        private void SpawnHero() {
            var checkpoints = FindObjectsOfType<CheckPointComponent>();
            var lastCheckpoint = _checkpoints.Last();
            foreach (var checkpoint in checkpoints) {
                if (checkpoint.Id == lastCheckpoint) {
                    checkpoint.SpawnHero();
                    break;
                }
            }
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
        }

        private void LoadHud() {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
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

        private void OnDestroy() {
            _trash.Dispose();
        }
    }
}