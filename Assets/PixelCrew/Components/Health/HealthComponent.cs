using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.PixelCrew.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private HealthChangeEvent _onChange;

        private void Start()
        {
            SetHealth(_maxHealth);
        }

        public void SetHealth(int hp)
        {
            _health = hp;
            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }
        }

        public void ModifyHealth(int healthDelta)
        {
            if (_health <= 0) return;

            SetHealth(_health + healthDelta);
            _onChange?.Invoke(_health);

            if (healthDelta < 0)
            {
                _onDamage?.Invoke();
            }
            else
            {
                _onHeal?.Invoke();
            }

            if (_health <= 0)
            {
                _onDie?.Invoke();
            }
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int>
        { }
    }
}
