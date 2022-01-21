using System;
using Assets.PixelCrew.Model.Data;
using Assets.PixelCrew.Model.Data.Properties;
using Assets.PixelCrew.Model.Definitions;
using Assets.PixelCrew.Utils;
using Assets.PixelCrew.Utils.Disposables;

namespace Assets.PixelCrew.Model.Models {

    public class PerksModel : IDisposable {

        public string Used => _data.Perks.Used.Value;
        public bool IsSuperThrowSupported => _data.Perks.Used.Value == "super-throw" && Cooldown.IsReady;
        public bool IsDoubleJumpSupported => _data.Perks.Used.Value == "double-jump" && Cooldown.IsReady;
        public bool IsShieldSupported => _data.Perks.Used.Value == "shield" && Cooldown.IsReady;

        public event Action OnChanged;
        public readonly StringProperty InterfaceSelection = new StringProperty();
        public readonly Cooldown Cooldown = new Cooldown();
        private readonly PlayerData _data;
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        public PerksModel(PlayerData data) {
            _data = data;
            InterfaceSelection.Value = DefsFacade.I.Perks.All[0].Id;
            _trash.Retain(_data.Perks.Used.Subscribe((x, y) => OnChanged?.Invoke()));
            _trash.Retain(InterfaceSelection.Subscribe((x, y) => OnChanged?.Invoke()));
        }

        public IDisposable Subscribe(Action call) {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public void Unlock(string id) {
            var def = DefsFacade.I.Perks.Get(id);
            var isEnoughResources = _data.Inventory.IsEnough(def.Price);
            if (isEnoughResources) {
                _data.Inventory.Remove(def.Price.ItemId, def.Price.Count);
                _data.Perks.AddPerk(id);
                OnChanged?.Invoke();
            }
        }

        internal void SelectPerk(string id) {
            var perkDef = DefsFacade.I.Perks.Get(id);
            Cooldown.Value = perkDef.Cooldown;
            _data.Perks.Used.Value = id;
        }

        public bool IsUsed(string id) {
            return _data.Perks.Used.Value == id;
        }
        internal bool IsUnlocked(string id) {
            return _data.Perks.IsUnlocked(id);
        }

        public bool CanBuy(string id) {
            var def = DefsFacade.I.Perks.Get(id);
            return _data.Inventory.IsEnough(def.Price);
        }

        public void Dispose() {
            _trash.Dispose();
        }
    }
}
