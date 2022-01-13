using System;
using System.Collections.Generic;
using Assets.PixelCrew.Model.Data.Properties;
using UnityEngine;

namespace Assets.PixelCrew.Model.Definitions.Localization {
    class LocalizationManager {

        public readonly static LocalizationManager I;
        private StringPersistentProperty _localeKey = new StringPersistentProperty("en", "localization/current");
        private Dictionary<string, string> _localozation;

        public string LocaleKey => _localeKey.Value;

        public event Action OnLocalChanged;

        static LocalizationManager() {
            I = new LocalizationManager();
        }

        public LocalizationManager() {
            LoadLocale(_localeKey.Value);
        }

        private void LoadLocale(string localeToLoad) {
            var def = Resources.Load<LocaleDef>($"Locales/{localeToLoad}");
            _localozation = def.GetData();
            _localeKey.Value = localeToLoad;
            OnLocalChanged?.Invoke();
        }

        public string Localize(string key) {
            return _localozation.TryGetValue(key, out var value) ? value : $"%%%{key}%%%";
        }

        public void SetLocale(string localKey) {
            LoadLocale(localKey);
        }
    }
}
