using Assets.PixelCrew.Model;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Assets.PixelCrew.Creatures.Hero.Features {

    public class HeroFlashlight : MonoBehaviour {

        [SerializeField] private float _consumerPerSecond;
        [SerializeField] private Light2D _light;

        private float _defaultIntensity;

        private void Start() {
            _defaultIntensity = _light.intensity;
        }

        private void Update() {
            var consumed = Time.deltaTime * _consumerPerSecond;
            var currentValue = GameSession.Instance.Data.Fuel.Value;
            var nextValue = currentValue - consumed;
            nextValue = Mathf.Max(nextValue, 0);
            GameSession.Instance.Data.Fuel.Value = nextValue;

            var progress = Mathf.Min(nextValue / 20, 1);
            _light.intensity = _defaultIntensity * progress;
        }
    }
}