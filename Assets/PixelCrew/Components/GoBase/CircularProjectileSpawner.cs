using System;
using System.Collections;
using Assets.PixelCrew.Creatures.Weapons;
using Assets.PixelCrew.Utils;
using UnityEngine;

namespace Assets.PixelCrew.Components.GoBase {

    public class CircularProjectileSpawner : MonoBehaviour {

        [SerializeField] private CircularProjectileSettings[] _settings;

        public int Stage { get; set; }

        [ContextMenu("Launch!")]
        public void LaunchProjectiles() {
            StartCoroutine(SpawnProjectiles());
        }

        private IEnumerator SpawnProjectiles() {

            var setting = _settings[Stage];
            var sectorStep = 2 * Mathf.PI / setting.BurstCount;
            for (int n = 0; n < setting.ItemsPerBurst; n++) {
                for (int i = 0; i < setting.BurstCount; i++) {
                    var angle = sectorStep * i;
                    var direction = new Vector2((float)Mathf.Cos(angle), (float)Mathf.Sin(angle));
                    var instance = SpawnUtils.Spawn(setting.Prefab, transform.position);
                    var projectile = instance.GetComponent<DirectionalProjectile>();
                    projectile.Launch(direction);
                    yield return new WaitForSeconds(setting.Delay);
                }
            }
        }
    }

    [Serializable]
    public struct CircularProjectileSettings {

        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _burstCount;
        [SerializeField] private int _itemsPerBurst;
        [SerializeField] private float _delay;

        public GameObject Prefab => _prefab;
        public int BurstCount => _burstCount;
        public int ItemsPerBurst => _itemsPerBurst;
        public float Delay => _delay;
    }
}