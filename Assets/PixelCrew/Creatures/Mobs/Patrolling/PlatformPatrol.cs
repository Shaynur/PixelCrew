using System.Collections;
using Assets.PixelCrew.Components.ColliderBased;
using UnityEngine;

namespace Assets.PixelCrew.Creatures.Mobs.Patrolling {

    public class PlatformPatrol : Patrol {

        [SerializeField] private LayerCheck _platformChecker;

        private Creature _creature;

        private void Awake() {
            _creature = GetComponent<Creature>();
        }

        public override IEnumerator DoPatrol() {
            while (enabled) {
                if (_creature._isGrounded) {
                    if (!_platformChecker.IsTouchingLayer) {
                        _creature.SetDirection(Vector2.zero);
                        yield return new WaitForSeconds(0.3f);
                        _creature.transform.localScale = new Vector3(-1f * transform.localScale.x, 1f, 1f);
                        yield return new WaitForSeconds(0.2f);
                    }
                    else {
                        _creature.SetDirection(new Vector2(transform.localScale.x, 0f));
                    }
                }
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }
    }
}