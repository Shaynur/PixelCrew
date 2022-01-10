using System.Collections.Generic;
using Assets.PixelCrew.Model;
using Assets.PixelCrew.Utils.Disposables;
using UnityEngine;

namespace Assets.PixelCrew.UI.Hud.QuickInventory {
    public class QuickInventoryController : MonoBehaviour {

        [SerializeField] private Transform _container;
        [SerializeField] private InventoryItemWidget _prefab;

        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private GameSession _session;
        private List<InventoryItemWidget> _createdItem = new List<InventoryItemWidget>();

        private void Start() {
            _session = FindObjectOfType<GameSession>();
            _trash.Retain(_session.QuickInventory.Subscribe(Rebuild));
            Rebuild();
        }

        private void Rebuild() {
            var inventory = _session.QuickInventory.Inventory;
            for (var i = _createdItem.Count; i < inventory.Length; i++) {
                var item = Instantiate(_prefab, _container);
                _createdItem.Add(item);
            }

            for (var i = 0; i < inventory.Length; i++) {
                _createdItem[i].SetData(inventory[i], i);
                _createdItem[i].gameObject.SetActive(true);
            }

            for( var i = inventory.Length; i < _createdItem.Count; i++) {
                _createdItem[i].gameObject.SetActive(false);
            }
        }

        private void OnDestroy() {
            _trash.Dispose();
        }
    }
}