using System;
using Assets.PixelCrew.Model.Data.Properties;
using UnityEngine;

namespace Assets.PixelCrew.Model.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventory;
        public IntProperty Hp = new IntProperty();

        public InventoryData Inventory => _inventory;

        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}