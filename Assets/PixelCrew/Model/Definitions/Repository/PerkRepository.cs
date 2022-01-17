using System;
using Assets.PixelCrew.Model.Definitions.Repository.Items;
using UnityEngine;

namespace Assets.PixelCrew.Model.Definitions.Repository {

    [CreateAssetMenu(menuName = "Defs/Perks", fileName = "Perks")]

    public class PerkRepository : DefRepository<PerkDef> {
    }

    [Serializable]
    public struct PerkDef : IHaveId {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _info;
        [SerializeField] private ItemWithCount _price;

        public string Id => _id;
        public Sprite Icon => _icon;
        public string Info => _info;
        public ItemWithCount Price => _price;
    }

    [Serializable]
    public struct ItemWithCount {
        [InventoryId] [SerializeField] private string _itemId;
        [SerializeField] private int _count;
        public string ItemId => _itemId;
        public int Count => _count;
    }
}
