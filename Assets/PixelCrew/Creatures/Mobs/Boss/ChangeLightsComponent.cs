using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Assets.PixelCrew.Creatures.Mobs.Boss {

    public class ChangeLightsComponent : MonoBehaviour {

        [SerializeField] private Light2D[] _lights;
        [ColorUsage(true, true)]
        [SerializeField] private Color _color;

        [ContextMenu("Setup")]
        public void SetColor() {
            foreach (var light2D in _lights) {
                light2D.color = _color;
            }
        }
    }
}