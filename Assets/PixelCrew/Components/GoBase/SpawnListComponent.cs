using System;
using System.Linq;
using UnityEngine;

namespace Assets.PixelCrew.Components.GoBase
{
    public class SpawnListComponent : MonoBehaviour
    {
        [SerializeField] private SpawnData[] _spawners;

        public void Spawn(string id)
        {
            var spawner = _spawners.FirstOrDefault(s => s.Id == id);
            spawner?.Component.Spawn();
        }

        [Serializable]
        public class SpawnData
        {
            public string Id;
            public SpawnComponent Component;
        }
    }
}