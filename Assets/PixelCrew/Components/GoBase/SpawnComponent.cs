using Assets.PixelCrew.Utils;
using Assets.PixelCrew.Utils.ObjectPool;
using UnityEngine;

namespace Assets.PixelCrew.Components.GoBase {

    public class SpawnComponent : MonoBehaviour {

        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private bool _spawnOnParent;
        [SerializeField] private bool _usePool;

        [ContextMenu("Spawn")]
        public void Spawn() {
            SpawnInstance();
        }

        public GameObject SpawnInstance() {

            GameObject instantiate;
            var targetPosition = _target.position;

            if (_spawnOnParent) {
                instantiate = Instantiate(_prefab, targetPosition, Quaternion.identity, transform);
            }
            else {
                instantiate = _usePool ?
                    Pool.Instance.Get(_prefab, targetPosition)
                    : SpawnUtils.Spawn(_prefab, targetPosition);
            }
            instantiate.transform.localScale = _target.lossyScale;
            instantiate.SetActive(true);
            return instantiate;
        }

        public void SetPrefab(GameObject prefab) {
            _prefab = prefab;
        }
    }
}