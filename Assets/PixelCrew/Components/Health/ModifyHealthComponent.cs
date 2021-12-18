using UnityEngine;

namespace Assets.PixelCrew.Components.Health
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _hpDelta;
        [SerializeField] private float _timeout;

        private float _lastTime;

        private void Start()
        {
            _lastTime = Time.time;
        }

        public void ChangeHP(GameObject target)
        {
            if (Time.time - _lastTime > _timeout)
            {
                _lastTime = Time.time;
                var healthComponent = target.GetComponent<HealthComponent>();
                if (healthComponent != null)
                {
                    healthComponent?.ModifyHealth(_hpDelta);
                }
            }
        }
    }
}
