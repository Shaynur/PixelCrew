using UnityEngine;
using UnityEngine.Events;

namespace Assets.PixelCrew.Utils.ObjectPool {

    public class PoolItem : MonoBehaviour {

        [SerializeField] private UnityEvent _onRestart;

        private int _id;
        private Pool _pool;

        public void Restart() {
            _onRestart?.Invoke();
        }

        public void Release() {
            _pool?.Release(_id, this);
        }

        public void Retain(int id, Pool pool) {
            _id = id;
            _pool = pool;
        }
    }
}