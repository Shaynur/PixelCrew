using System.Collections;
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
            GameObject instantiate;
            if (_spawnOnParent)
            {
                instantiate = Instantiate(_prefab, _target.position, Quaternion.identity, transform);
            }
            else
            {
                instantiate = Instantiate(_prefab, _target.position, Quaternion.identity);
            }
            instantiate.transform.localScale = _target.lossyScale;
        }
    }
}