using Assets.PixelCrew.Model;
using Assets.PixelCrew.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.PixelCrew.UI.Windows.InGameMenu {

    public class InGameMenuWindow : AnimatedWindow {

        private float _defaultTimeScale;

        protected override void Start() {
            base.Start();
            _defaultTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        public void OnShowSettings() {
            WindowUtils.CreateWindow("UI/SettingsWindow");
        }

        public void OnExit() {
            SceneManager.LoadScene("MainMenu");
            var session = FindObjectOfType<GameSession>();
            Destroy(session.gameObject);
        }

        private void OnDestroy() {
            Time.timeScale = _defaultTimeScale;
        }
    }
}