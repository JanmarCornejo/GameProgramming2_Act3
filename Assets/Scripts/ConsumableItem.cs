using UnityEngine;

namespace GP2.Inventory
{
    [System.Serializable]
    public class ConsumableItem : Item  
    {
        public ConsumableItem(ItemBag itemBag) : base(itemBag)
        {
            Name = itemBag.ItemName;
            Type = itemBag.Type;
            Kind = itemBag.Kind;
            Description = itemBag.Description;
            IsStackable = true;
            IsClickable = true;
            Quantity = itemBag.Quantity;
        }

        public sealed override string Name { get; protected set; }
        public sealed override ItemType Type { get; protected set; }
        public sealed override ItemKind Kind { get; protected set; }
        public sealed override int Quantity { get; protected set; }
        public sealed override string Description { get; protected set; }
        public sealed override bool IsStackable { get; protected set; }
        public sealed override bool IsClickable { get; protected set; }
        
        public override void StackItem(Item item)
        {
            Debug.Log($"{Name} previous quantity: {Quantity}");
            Quantity += item.Quantity;
            Debug.Log($"{Name} new quantity: {Quantity}");
        }

        public override void ShowItemInfo()
        {
            Debug.Log($"Equipment Name: {Name} : {Description}");
        }
    }
}