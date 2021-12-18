using System.Collections;
using UnityEngine;

namespace Assets.PixelCrew.Components.Interaction
{
    public class SwitchComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private bool _state;
        [SerializeField] private string _animationKey;

        public void Switch()
        {
            _state = !_state;
            _animator.SetBool(_animationKey, _state);
        }

        [ContextMenu("Switch")]
        private void SwitchIt()
        {
            Switch();
        }
    }
}