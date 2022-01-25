using System;
using Assets.PixelCrew.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.PixelCrew.Components.Health {

    public class HealthComponent : MonoBehaviour {

        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] public UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private HealthChangeEvent _onChange;

        public int Health => _health;
        public Lock Immune => _immune;
        public UnityEvent OnDie => _onDie;
        public HealthChangeEvent OnChange => _onChange;

        private Lock _immune = new Lock();

        public void SetHealth(int newHealth) {
            if (newHealth == _health) return;
            _health = newHealth;
            _onChange?.Invoke(_health);
        }

        public void ModifyHealth(int healthDelta) {
            if (healthDelta == 0) return;
            if (healthDelta < 0 && Immune.IsLocked) return;

            _health += healthDelta;
            _onChange?.Invoke(_health);

            if (healthDelta < 0) {
                _onDamage?.Invoke();
            }
            else {
                _onHeal?.Invoke();
            }

            if (_health <= 0) {
                _onDie?.Invoke();
            }
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int> { }
    }
}
