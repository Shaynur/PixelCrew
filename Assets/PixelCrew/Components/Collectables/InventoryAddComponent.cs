using System;
using Assets.PixelCrew.Model.Data;
using Assets.PixelCrew.Model.Definitions.Repository.Items;
using Assets.PixelCrew.Utils;
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
            var hero = go.GetInterface<ICanAddInInventory>();
            if (hero?.AddInInventory(_id, _count) == true)
            {
                _onSuccess?.Invoke(go);
            }
            else
            {
                _onFail.Invoke(go);
            }
        }
    }

    [Serializable]
    public class GoEvent : UnityEvent<GameObject>
    { }
}