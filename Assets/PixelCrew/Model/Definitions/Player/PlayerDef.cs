using UnityEngine;

namespace Assets.PixelCrew.Model.Definitions.Player {

    [CreateAssetMenu(menuName = "Defs/PlayerDef", fileName = "PlayerDef")]

    public class PlayerDef : ScriptableObject {

        [SerializeField] private int _inventorySize;
        [SerializeField] private StatDef[] _stats;

        public int InventorySize => _inventorySize;
        public StatDef[] Stats => _stats;

        public StatDef GetStat(StatId id) {

            foreach (var statDef in _stats) { 
                if(statDef.Id == id)
                    return statDef;
            }
            return default;
        }
    }
}