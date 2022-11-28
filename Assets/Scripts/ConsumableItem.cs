using UnityEngine;

namespace GP2.Inventory
{
    [System.Serializable]
    public class ConsumableItem : Item  
    {
        public ConsumableItem(ItemBag itemBag) : base(itemBag)
        {
            IsStackable = true;
            IsClickable = true;
            Quantity = itemBag.Quantity;
        }
        
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