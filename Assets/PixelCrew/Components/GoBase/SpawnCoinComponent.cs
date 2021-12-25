using Assets.PixelCrew.Utils;
using UnityEngine;

namespace Assets.PixelCrew.Components.GoBase
{
    public class SpawnCoinComponent : MonoBehaviour
    {
        [SerializeField] [Range(0, 100)] private int _goldCoinProbability;
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _goldCoinPrefab;
        [SerializeField] private GameObject _silverCoinPrefab;

        public void Awake()
        {
            Spawn();
        }

        public void Spawn()
        {
            var rand = UnityEngine.Random.Range(0, 100);
            if (_goldCoinProbability > rand)
            {
                SpawnUtils.Spawn(_goldCoinPrefab, _target.position);
            }
            else
            {
                SpawnUtils.Spawn(_silverCoinPrefab, _target.position);
            }
        }
    }
}

