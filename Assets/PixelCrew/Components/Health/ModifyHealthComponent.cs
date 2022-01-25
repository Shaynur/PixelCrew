using UnityEngine;

namespace Assets.PixelCrew.Components.Health {

    public class ModifyHealthComponent : MonoBehaviour {

        [SerializeField] private int _hpDelta;

        public void SetHpDelta(int hpDelta) {
            _hpDelta = hpDelta;
        }

        public void ChangeHP(GameObject target) {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null) {
                healthComponent.ModifyHealth(_hpDelta);
            }
        }
    }
}
