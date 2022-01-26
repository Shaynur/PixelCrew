using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.PixelCrew.Creatures.Hero {

    public class InputEnableComponent : MonoBehaviour {

        private PlayerInput _input;

        void Start() {
            _input = FindObjectOfType<PlayerInput>();
        }

        public void SetInput(bool isEnabled) {
            _input.enabled = isEnabled;
        }

    }
}