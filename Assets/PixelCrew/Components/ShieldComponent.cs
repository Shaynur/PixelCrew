using Assets.PixelCrew.Components.Health;
using Assets.PixelCrew.Utils;
using UnityEngine;

namespace Assets.PixelCrew.Components {

    public class ShieldComponent : MonoBehaviour {

        [SerializeField] private HealthComponent _health;
        [SerializeField] private Cooldown _cooldown;

        public void Use() {
            _health.Immune.Retain(this);
            _cooldown.Reset();
            gameObject.SetActive(true);
        }

        private void Update() {
            if (_cooldown.IsReady)
                gameObject.SetActive(false);
        }

        private void OnDisable() {
            _health.Immune.Release(this);
        }
    }
}