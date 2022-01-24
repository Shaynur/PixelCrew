using System;
using Assets.PixelCrew.UI.LevelsLoader;
using Assets.PixelCrew.Utils;
using UnityEngine;

namespace Assets.PixelCrew.UI.Windows.MainMenu {

    public class MainMenuWindow : AnimatedWindow {
        private Action _closeAction;

        public void OnShowSettings() {
            WindowUtils.CreateWindow("UI/SettingsWindow");
        }

        public void OnStartGame() {
            _closeAction = () => {
                var loader = FindObjectOfType<LevelLoader>();
                loader.LoadLevel("Level1");
            };
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