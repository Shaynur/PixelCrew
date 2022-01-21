﻿using Assets.PixelCrew.Model;
using Assets.PixelCrew.Model.Definitions.Player;
using Assets.PixelCrew.UI.Widgets;
using Assets.PixelCrew.Utils;
using UnityEngine;

namespace Assets.PixelCrew.UI.Hud {
    public class HudController : MonoBehaviour {

        [SerializeField] private ProgressbarWidget _healthBar;

        private GameSession _session;

        private void Start() {
            _session = FindObjectOfType<GameSession>();
            _session.Data.Hp.OnChanged += OnHealthChanged;
            OnHealthChanged(_session.Data.Hp.Value, 0);
        }

        private void OnHealthChanged(int newValue, int oldValue) {
            var maxHealth = _session.StatsModel.GetValue(StatId.Hp);
            var value = (float)newValue / maxHealth;
            _healthBar.SetProgress(value);
        }

        public void OnSettings() {
            WindowUtils.CreateWindow("UI/InGameMenuWindow");
        }

        private void OnDestroy() {
            _session.Data.Hp.OnChanged -= OnHealthChanged;
        }

        public void OnShowDebug() {
            WindowUtils.CreateWindow("UI/PlayerStatsWindow");
        }
    }
}