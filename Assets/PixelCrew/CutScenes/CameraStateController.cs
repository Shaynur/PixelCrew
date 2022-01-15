using Cinemachine;
using UnityEngine;

namespace Assets.PixelCrew.CutScenes {

    public class CameraStateController : MonoBehaviour {
        [SerializeField] private Animator _animator;
        [SerializeField] private CinemachineVirtualCamera _camera;

        private static readonly int ShowTargetKey = Animator.StringToHash("ShowTarget");

        public void SetPosition(Vector3 targetPosition) {
            targetPosition.z = _camera.transform.position.z;
            _camera.transform.position = targetPosition;
        }

        public void SetState(bool state) {
            _animator.SetBool(ShowTargetKey, state);
        }
    }
}
