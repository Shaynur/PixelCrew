using System.Collections.Generic;
using UnityEngine;

namespace Assets.PixelCrew.Model.Definitions.Repository {
    public class DefRepository<TDefType> : ScriptableObject where TDefType : IHaveId {

        [SerializeField] protected TDefType[] _collection;

        public TDefType[] All => new List<TDefType>(_collection).ToArray();
        
        public TDefType Get(string id) {
            if (!string.IsNullOrEmpty(id)) {
                foreach (var itemDef in _collection) {
                    if (itemDef.Id == id) {
                        return itemDef;
                    }
                }
            }
            return default;
        }
    }
}
