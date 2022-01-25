using UnityEngine;

namespace Assets.PixelCrew.Creatures.Weapons {

    public class DirectionalProjectile : BaseProjectile {

        public void Launch(Vector2 direction) {

            Rigidbody = GetComponent<Rigidbody2D>();
            Rigidbody.AddForce(direction * _speed, ForceMode2D.Impulse);
        }
    }
}