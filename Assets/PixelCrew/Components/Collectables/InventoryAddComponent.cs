using System;
using System.Collections;
using Assets.PixelCrew.Creatures.Hero;
using Assets.PixelCrew.Model.Definitions;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.PixelCrew.Components.Collectables
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private int _count;
        [SerializeField] private GoEvent _onSuccess;
        [SerializeField] private GoEvent _onFail;

        public void Add(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            if (hero != null)
            {
                if (hero.AddInInventory(_id, _count))
                {
                    _onSuccess?.Invoke(go);
                }
                else
                {
                    _onFail.Invoke(go);
                }
            }
        }
    }

    [Serializable]
    public class GoEvent : UnityEvent<GameObject>
    { }
}