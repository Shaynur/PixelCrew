using System.Collections;
using UnityEngine;

namespace Assets.PixelCrew.Effects {

    public class CameraShaker : MonoBehaviour {

        [SerializeField] private float _time = 0.2f;
        [SerializeField] private float _maxDelta = 0.1f;

        private Vector3 _defaultPosition;
        private float _animationTime;
        private Coroutine _coroutine;

        private void Awake() {
            _defaultPosition = transform.position;
        }

        public void ShakeIt() {
            if (_coroutine != null) {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(StartAnimation());
        }

        private IEnumerator StartAnimation() {
            _animationTime = 0f;
            while (_animationTime < _time) {
                _animationTime += Time.deltaTime;
                Vector3 delta = Random.insideUnitCircle.normalized * _maxDelta;
                transform.position = _defaultPosition + delta;
                yield return null;
            }
            transform.position = _defaultPosition;
        }
    }
}