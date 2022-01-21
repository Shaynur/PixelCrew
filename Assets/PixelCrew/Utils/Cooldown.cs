using System;
using UnityEngine;

namespace Assets.PixelCrew.Utils {

    [Serializable]
    public class Cooldown {

        [SerializeField] private float _value;

        public float Value { get => _value; set => _value = value; }
        public bool IsReady => (_timesUp <= Time.time);
        public float RemainingTime => Mathf.Max(_timesUp - Time.time, 0);

        private float _timesUp;

        public void Reset() {
            _timesUp = Time.time + _value;
        }

    }
}
