using System;
using System.Collections.Generic;
using System.Linq;
using Assets.PixelCrew.Model.Definitions;
using Assets.PixelCrew.Utils.Disposables;
using UnityEngine;

namespace Assets.PixelCrew.Model.Data {

    [Serializable]
    public class InventoryData {
        [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();

        public delegate void OnInventoryChanged(string id, int value);
        public OnInventoryChanged OnChanged;
        private int _inventoryCount = 0;
        public int InventoryCount => _inventoryCount;

        public IDisposable Subscribe(OnInventoryChanged call) {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public bool Add(string id, int value) {
            if (_inventoryCount + value > DefsFacade.I.Player.InventorySize) {
                value = DefsFacade.I.Player.InventorySize - _inventoryCount;
            }
            if (value <= 0) return false;

            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return false;

            if (!itemDef.HasTag(ItemTag.Stackable)) {
                AddUnstackable(id, value);
                return true;
            }

            var item = GetItem(id);
            if (item == null) {
                item = new InventoryItemData(id);
                _inventory.Add(item);
            }
            item.Value += value;
            _inventoryCount += value;
            OnChanged?.Invoke(id, Count(id));
            Debug.Log("Inventory count = " + _inventoryCount);
            return true;
        }

        public InventoryItemData[] GetAll(params ItemTag[] tags) {
            var retValue = new List<InventoryItemData>();
            foreach (var item in _inventory) {
                var itemDef = DefsFacade.I.Items.Get(item.Id);
                var isAllRequirementMet = tags.All(x => itemDef.HasTag(x));
                if (isAllRequirementMet)
                    retValue.Add(item);
            }
            return retValue.ToArray();
        }

        private void AddUnstackable(string id, int value) {
            for (int i = 0; i < value; i++) {
                var item = new InventoryItemData(id);
                _inventory.Add(item);
                item.Value += 1;
                _inventoryCount++;
                OnChanged?.Invoke(id, Count(id));
            }
            Debug.Log("Inventory count = " + _inventoryCount);
        }

        public void Remove(string id, int value) {
            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return;

            var item = GetItem(id);
            if (item == null) return;

            item.Value -= value;
            _inventoryCount -= value;
            if (_inventoryCount < 0) {
                _inventoryCount = 0;
            }
            if (item.Value <= 0) {
                _inventory.Remove(item);
            }
            OnChanged?.Invoke(id, Count(id));
            Debug.Log("Inventory count = " + _inventoryCount);
        }

        private InventoryItemData GetItem(string id) {
            foreach (var itemData in _inventory) {
                if (itemData.Id == id) {
                    return itemData;
                }
            }
            return null;
        }

        public int Count(string id) {
            var count = 0;
            foreach (var item in _inventory) {
                if (item.Id == id) {
                    count += item.Value;
                }
            }
            return count;
        }
    }

    [Serializable]
    public class InventoryItemData {
        [InventoryId] public string Id;
        public int Value;
        public InventoryItemData(string id) {
            Id = id;
        }
    }
}