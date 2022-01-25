using Assets.PixelCrew.Components.Health;
using Assets.PixelCrew.Utils;
using Assets.PixelCrew.Utils.Disposables;
using UnityEngine;

namespace Assets.PixelCrew.UI.Widgets {

    public class BossHpWidget : MonoBehaviour {

        [SerializeField] private HealthComponent _health;
        [SerializeField] private ProgressbarWidget _hpBar;
        [SerializeField] private CanvasGroup _canvasGroup;

        private float _maxHealth;
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private void Start() {
            _maxHealth = _health.Health;
            _trash.Retain(_health.OnChange.Subscribe(OnHpChanged));
            _trash.Retain(_health.OnDie.Subscribe(HideUI));
        }

        [ContextMenu("Show")]
        public void ShowUI() {
            this.LerpAnimated(0, 1, 1, SetAlpha);
        }

        private void SetAlpha(float alpha) {
            _canvasGroup.alpha = alpha;
        }

        [ContextMenu("Hide")]
        public void HideUI() {
            this.LerpAnimated(1, 0, 1, SetAlpha);
        }

        private void OnHpChanged(int hp) {
            _hpBar.SetProgress(hp / _maxHealth);
        }

        private void OnDestroy() {
            _trash.Dispose();
        }
    }
}