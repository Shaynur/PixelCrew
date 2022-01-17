using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.PixelCrew.UI.Widgets {
    public class PredefinedDataGroup<TDataType, TItemType> : DataGroup<TDataType, TItemType>
        where TItemType : MonoBehaviour, IItemRenderer<TDataType> {

        public PredefinedDataGroup(Transform container) : base(null, container) {
            var items = container.GetComponentsInChildren<TItemType>();
            CreatedItem.AddRange(items);
        }

        public override void SetData(IList<TDataType> data) {
            if (data.Count > CreatedItem.Count)
                throw new IndexOutOfRangeException();
            base.SetData(data);
        }
    }
}