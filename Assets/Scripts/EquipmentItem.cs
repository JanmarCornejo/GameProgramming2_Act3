using UnityEngine;

namespace GP2.Inventory
{
    [System.Serializable]
    public class EquipmentItem : Item
    {
        public EquipmentItem(ItemBag itemBag) : base(itemBag)
        {
            IsStackable = false;
            IsClickable = true; 
            Quantity = 1;
        }

        public override void StackItem(Item item)
        {
            if (!IsStackable)
            {
                Debug.Log($"{Name} cannot be stacked");
            }
        }

        public override void ShowItemInfo()
        {
            Debug.Log($"Equipment Name: {Name} : {Description}");
        }
    }
}