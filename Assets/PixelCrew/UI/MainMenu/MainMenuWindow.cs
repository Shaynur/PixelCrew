using System;
using Assets.PixelCrew.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.PixelCrew.UI.MainMenu {
    public class MainMenuWindow : AnimatedWindow {
        private Action _closeAction;

        public void OnShowSettings() {
            //_closeAction = () =>
            //{
            WindowUtils.CreateWindow("UI/SettingsWindow");
            //};
            //Close();
        }

        public void OnStartGame() {
            _closeAction = () => { SceneManager.LoadScene("Level1"); };
            Close();
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