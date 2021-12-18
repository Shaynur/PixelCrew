using System.Collections;
using UnityEngine;

namespace Assets.PixelCrew.Components.Movement
{
    public class VerticalLevitationComponent : MonoBehaviour
    {
        [SerializeField] private float _frequency = 1f;
        [SerializeField] private float _amplitude = 1f;
        [SerializeField] private bool _randomize;

        private float _originalY;
        private Rigidbody2D _rigitbody;
        private float _seed;

        private void Awake()
        {
            _rigitbody = GetComponent<Rigidbody2D>();
            _originalY = _rigitbody.position.y;
            if(_randomize)
            {
                _seed = Random.value * Mathf.PI * 2;
            }
        }

        private void Update()
        {
            var pos = _rigitbody.position;
            pos.y = _originalY + Mathf.Sin(_seed + Time.time * _frequency) * _amplitude;
            _rigitbody.MovePosition(pos);
        }
    }
}