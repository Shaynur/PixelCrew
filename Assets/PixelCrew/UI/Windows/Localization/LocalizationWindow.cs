using System.Collections.Generic;
using Assets.PixelCrew.Model.Definitions.Localization;
using Assets.PixelCrew.UI.Widgets;
using UnityEngine;

namespace Assets.PixelCrew.UI.Windows.Localization {

    public class LocalizationWindow : AnimatedWindow {

        [SerializeField] private LocaleItemWidget _prefab;
        [SerializeField] private Transform _container;

        private DataGroup<LocaleInfo, LocaleItemWidget> _dataGroup;
        private string[] _supportedLocales = new[] { "en", "ru" };


        protected override void Start() {
            base.Start();
            _dataGroup = new DataGroup<LocaleInfo, LocaleItemWidget>(_prefab, _container);
            _dataGroup.SetData(ComposeData());
        }

        private List<LocaleInfo> ComposeData() {
            var data = new List<LocaleInfo>();
            foreach (var locale in _supportedLocales) {
                data.Add(new LocaleInfo { LocaleId = locale });
            }
            return data;
        }

        public void OnSelected(string selectedLocale) {
            LocalizationManager.I.SetLocale(selectedLocale);
        }
    }
}
