using System;
using System.Linq;
using UnityEngine;

namespace Assets.PixelCrew.Components.GoBase {

    public class SpawnListComponent : MonoBehaviour {

        [SerializeField] private SpawnData[] _spawners;

        public void Spawn(string id) {

            foreach (var spawner in _spawners) {
                if (spawner.Id == id)
                    spawner.Component.Spawn();
            }
        }

        [Serializable]
        public class SpawnData {
            public string Id;
            public SpawnComponent Component;
        }
    }
}