using Assets.PixelCrew.Model.Data;
using Assets.PixelCrew.Utils.Disposables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.PixelCrew.Model {

    public class GameSession : MonoBehaviour {

        [SerializeField] private PlayerData _data;

        public PlayerData Data => _data;
        private PlayerData _save;
        public QuickInventoryModel QuickInventory { get; private set; }
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private void Awake() {
            LoadHud();
            if (IsSessionExist()) {
                Destroy(gameObject);
            }
            else {
                SavePlayerData();
                InitModels();
                DontDestroyOnLoad(this);
            }
        }

        private void InitModels() {
            QuickInventory = new QuickInventoryModel(Data);
            _trash.Retain(QuickInventory);
        }

        private void LoadHud() {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
        }

        public void SavePlayerData() {
            _save = _data.Clone();
        }

        public void LoadPlayerData() {
            _data = _save.Clone();
            InitModels();
        }

        private bool IsSessionExist() {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSession in sessions) {
                if (gameSession != this) {
                    return true;
                }
            }
            return false;
        }

        private void OnDestroy() {
            _trash.Dispose();
        }
    }
}