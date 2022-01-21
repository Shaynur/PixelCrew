using Assets.PixelCrew.Model.Definitions.Player;
using Assets.PixelCrew.Model.Definitions.Repository;
using Assets.PixelCrew.Model.Definitions.Repository.Items;
using UnityEngine;

namespace Assets.PixelCrew.Model.Definitions {

    [CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade")]
    public class DefsFacade : ScriptableObject {

        [SerializeField] private ItemsRepository _items;
        [SerializeField] private ThrowableRepository _throwableItems;
        [SerializeField] private PotionRepository _potions;
        [SerializeField] private PerkRepository _perks;
        [SerializeField] private PlayerDef _player;

        public ItemsRepository Items => _items;
        public ThrowableRepository Throwable => _throwableItems;
        public PotionRepository Potions => _potions;
        public PerkRepository Perks => _perks;
        public PlayerDef Player => _player;
        public static DefsFacade I => _instance == null ? LoadDefs() : _instance;

        private static DefsFacade _instance;

        private static DefsFacade LoadDefs() {
            return _instance = Resources.Load<DefsFacade>("DefsFacade");
        }
    }
}