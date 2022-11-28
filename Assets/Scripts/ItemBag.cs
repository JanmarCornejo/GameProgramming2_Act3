
using UnityEngine;

namespace GP2.Inventory
{
    [CreateAssetMenu(order = 1, fileName = "ItemInfo", menuName = "Inventory/Add Item")]
    public sealed class ItemBag : ScriptableObject
    {
        public string ItemName;
        public ItemType Type;
        public ItemKind Kind;
        public int Quantity = 1;
        public Sprite Sprite;
        [TextArea]
        public string Description;
    }
}