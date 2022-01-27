using Assets.PixelCrew.Model;
using Assets.PixelCrew.Model.Definitions;
using Assets.PixelCrew.Model.Definitions.Player;
using Assets.PixelCrew.UI.Widgets;
using Assets.PixelCrew.Utils;
using Assets.PixelCrew.Utils.Disposables;
using UnityEngine;

namespace Assets.PixelCrew.UI.Hud {
    public class HudController : MonoBehaviour {

        [SerializeField] private ProgressbarWidget _healthBar;
        [SerializeField] private CurrerntPerkWidget _currentPerk;

        private GameSession _session;
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private void Start() {
            _session = GameSession.Instance;
            _trash.Retain(_session.Data.Hp.SubscribeAndInvoke(OnHealthChanged));
            //OnHealthChanged(_session.Data.Hp.Value, 0);
            _trash.Retain(_session.PerksModel.Subscribe(OnPerkChanged));
            OnPerkChanged();
        }

        private void OnPerkChanged() {
            var usedPerkId = _session.PerksModel.Used;
            var hasPerk = !string.IsNullOrEmpty(usedPerkId);
            if (hasPerk) {
                var perkDef = DefsFacade.I.Perks.Get(usedPerkId);
                _currentPerk.Set(perkDef);
            }
            _currentPerk.gameObject.SetActive(hasPerk);
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
            _trash.Dispose();
        }

        public void OnShowDebug() {
            WindowUtils.CreateWindow("UI/PlayerStatsWindow");
        }
    }
}