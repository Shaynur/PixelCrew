using UnityEngine;

namespace Assets.PixelCrew.Creatures.Weapons
{
    public class SinusoidalProjectile : BaseProjectile
    {
        [SerializeField] private float _frequency = 1f;
        [SerializeField] private float _amplitude = 1f;

        private float _originalY;
        private float _time;

        protected override void Start()
        {
            base.Start();
            _originalY = Rigibbody.position.y;
        }

        private void FixedUpdate()
        {
            var position = Rigibbody.position;
            position.x += Direction * _speed;
            position.y = _originalY + Mathf.Sin(_time * _frequency) * _amplitude;
            Rigibbody.MovePosition(position);
            _time += Time.fixedDeltaTime;
        }
    }
}