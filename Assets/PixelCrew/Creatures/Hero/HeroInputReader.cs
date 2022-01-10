using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.PixelCrew.Creatures.Hero {
    public class HeroInputReader : MonoBehaviour {
        [SerializeField] private Hero _hero;

        public void OnMovement(InputAction.CallbackContext context) {
            var movement = context.ReadValue<Vector2>();
            _hero.SetDirection(movement);
        }

        public void OnInteract(InputAction.CallbackContext context) {
            if (context.canceled) {
                _hero.Interact();
            }
        }
        public void OnAttack(InputAction.CallbackContext context) {
            if (context.canceled) {
                _hero.Attack();
            }
        }

        public void OnThrow(InputAction.CallbackContext context) {
            if (context.performed) {
                StartCoroutine(_hero.SuperThrowAbility());
            }
            else if (context.canceled && context.duration < 1) {
                _hero.Throw();
            }
        }

        public void OnUseHPpotion(InputAction.CallbackContext context) {
            if (context.canceled) {
//                _hero.UseHPpotion();
            }
        }

        public void OnNextItem(InputAction.CallbackContext context) {
            if (context.canceled) {
                _hero.NextItem();
            }
        }
    }
}
