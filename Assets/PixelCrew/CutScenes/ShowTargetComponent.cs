using UnityEngine;
using UnityEngine.Events;

namespace Assets.PixelCrew.CutScenes {

    public class ShowTargetComponent : MonoBehaviour {

        [SerializeField] private Transform _target;
        [SerializeField] private CameraStateController _controller;
        [SerializeField] private float _delay = 0.5f;
        [SerializeField] private UnityEvent _onDelay;

        private void OnValidate() {
            if (_controller == null) {
                _controller = FindObjectOfType<CameraStateController>();
            }
        }

        private void Awake() {
            _controller = FindObjectOfType<CameraStateController>();
        }

        public void ShowTarget() {
            _controller.SetPosition(_target.position);
            _controller.SetState(true);
            Invoke(nameof(MoveBack), _delay);
        }

        private void MoveBack() {
            _controller.SetState(false);
            _onDelay?.Invoke();
        }
    }
}