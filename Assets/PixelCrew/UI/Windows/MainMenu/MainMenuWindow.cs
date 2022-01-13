using System;
using Assets.PixelCrew.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.PixelCrew.UI.Windows.MainMenu {

    public class MainMenuWindow : AnimatedWindow {
        private Action _closeAction;

        public void OnShowSettings() {
            WindowUtils.CreateWindow("UI/SettingsWindow");
        }

        public void OnStartGame() {
            _closeAction = () => { SceneManager.LoadScene("Level1"); };
            Close();
        }

        public void OnLanguages() {
            WindowUtils.CreateWindow("UI/LocalizationWindow");
        }

        public void OnExit() {
            _closeAction = () => {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            };
            Close();
        }

        public override void OnCloseAnimationComplete() {
            base.OnCloseAnimationComplete();
            _closeAction?.Invoke();
        }
    }
}