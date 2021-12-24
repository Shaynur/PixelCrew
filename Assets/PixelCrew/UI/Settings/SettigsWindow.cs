using Assets.PixelCrew.Model.Data;
using Assets.PixelCrew.UI.Widgets;
using UnityEngine;

namespace Assets.PixelCrew.UI.Settings
{
    public class SettigsWindow : AnimatedWindow
    {
        [SerializeField] private AudioSettingsWidget _music;
        [SerializeField] private AudioSettingsWidget _sfx;

        protected override void Start()
        {
            base.Start();
            _music.SetModel(GameSettings.I.Music);
            _sfx.SetModel(GameSettings.I.Sfx);
        }
        public void OnShowMainMenu()
        {
            Close();
            var window = Resources.Load<GameObject>("UI/MainMenuWindow");
            var canvas = FindObjectOfType<Canvas>();
            if (window && canvas)
            {
                Instantiate(window, canvas.transform);
            }
        }
    }
}
