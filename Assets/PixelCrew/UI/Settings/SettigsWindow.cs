using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.PixelCrew.UI.Settings
{
    public class SettigsWindow : AnimatedWindow
    {
        public void OnShowSettings()
        {

        }

        public void OnStartGame()
        {
            SceneManager.LoadScene("Level1");
        }

        public void OnExit()
        {
            Close();
        }

        public override void OnCloseAnimationComplete()
        {
            base.OnCloseAnimationComplete();
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
