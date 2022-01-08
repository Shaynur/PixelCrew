using Assets.PixelCrew.Model.Data;
using Assets.PixelCrew.UI.Widgets;
using UnityEngine;

namespace Assets.PixelCrew.UI.Settings {
    public class SettigsWindow : AnimatedWindow {
        [SerializeField] private AudioSettingsWidget _music;
        [SerializeField] private AudioSettingsWidget _sfx;

        protected override void Start() {
            base.Start();
            _music.SetModel(GameSettings.I.Music);
            _sfx.SetModel(GameSettings.I.Sfx);
        }

        public void OnExit() {
            Close();
        }
    }
}
