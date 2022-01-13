using System;
using System.Collections;
using Assets.PixelCrew.Model.Definitions.Localization;
using Assets.PixelCrew.UI.Widgets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.PixelCrew.UI.Windows.Localization {

    public class LocaleItemWidget : MonoBehaviour, IItemRenderer<LocaleInfo> {

        [SerializeField] private Text _text;
        [SerializeField] private GameObject _selector;
        [SerializeField] private SelectLocale _onSelected;

        private LocaleInfo _data;

        private void Start() {
            LocalizationManager.I.OnLocalChanged += UpdateSelection;
        }

        public void SetData(LocaleInfo localeInfo, int index) {
            _data = localeInfo;
            UpdateSelection();
            _text.text = localeInfo.LocaleId.ToUpper();
        }
        private void UpdateSelection() {
            var isSelected = LocalizationManager.I.LocaleKey == _data.LocaleId;
            _selector.SetActive(isSelected);
        }

        public void OnSelected() {
            _onSelected?.Invoke(_data.LocaleId);
        }

        private void OnDestroy() {
            LocalizationManager.I.OnLocalChanged -= UpdateSelection;
        }
    }

    [Serializable]
    public class SelectLocale : UnityEvent<string> {
    }

    public class LocaleInfo {
        public string LocaleId;
    }
}