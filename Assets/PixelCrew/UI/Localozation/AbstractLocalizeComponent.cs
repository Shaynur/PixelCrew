using Assets.PixelCrew.Model.Definitions.Localization;
using UnityEngine;

namespace Assets.PixelCrew.UI.Localozation {

    public abstract class AbstractLocalizeComponent : MonoBehaviour {

        protected abstract void Localize();

        protected virtual void Awake() {
            LocalizationManager.I.OnLocalChanged += OnLocaleChanged;
            Localize();
        }

        private void OnLocaleChanged() {
            Localize();
        }

        private void OnDestroy() {
            LocalizationManager.I.OnLocalChanged -= OnLocaleChanged;
        }
    }
}