using Assets.PixelCrew.Components.Health;
using Assets.PixelCrew.Utils.Disposables;
using UnityEngine;

namespace Assets.PixelCrew.UI.Widgets {
    public class LifeBarWidget : MonoBehaviour {

        [SerializeField] private ProgressbarWidget _lifeBar;
        [SerializeField] private HealthComponent _hp;

        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private int _maxHp;

        private void Start() {
            if (_hp == null) {
                _hp = GetComponentInParent<HealthComponent>();
            }
            _maxHp = _hp.Health;
            _trash.Retain(_hp.OnDie.Subscribe(OnDie));
            _trash.Retain(_hp.OnChange.Subscribe(OnHpChanged));
        }

        private void OnDie() {
            Destroy(gameObject);
        }

        private void OnHpChanged(int hp) {
            var progress = (float)hp / _maxHp;
            _lifeBar.SetProgress(progress);
        }

        private void OnDestroy() {
            _trash.Dispose();
        }
    }
}