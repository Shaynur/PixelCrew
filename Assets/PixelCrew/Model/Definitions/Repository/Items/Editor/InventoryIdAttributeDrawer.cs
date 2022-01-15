using System.Collections.Generic;
using Assets.PixelCrew.Model.Definitions.Repository.Items;
using UnityEditor;
using UnityEngine;

namespace Assets.PixelCrew.Model.Definitions.DefRepository.Items.Editor {

    [CustomPropertyDrawer(typeof(InventoryIdAttribute))]
    public class InventoryIdAttributeDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var defs = DefsFacade.I.Items.ItemsForEditor;
            var ids = new List<string>();
            foreach (var itemDef in defs) {
                ids.Add(itemDef.Id);
            }
            var index = Mathf.Max(ids.IndexOf(property.stringValue), 0);
            index = EditorGUI.Popup(position, property.displayName, index, ids.ToArray());
            property.stringValue = ids[index];
        }
    }
}