using System;
using Assets.PixelCrew.Utils;
using UnityEngine;

namespace Assets.PixelCrew.Components.GoBase
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private bool _spawnOnParent;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            SpawnInstance();
        }

        public GameObject SpawnInstance() {
            GameObject instantiate;
            if (_spawnOnParent)
            {
                instantiate = Instantiate(_prefab, _target.position, Quaternion.identity, transform);
            }
            else
            {
                instantiate = SpawnUtils.Spawn(_prefab, _target.position);
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