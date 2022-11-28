using UnityEngine;

namespace GP2.Inventory
{
    [System.Serializable]
    public class MiscItem : Item
    {
        public MiscItem(ItemBag itemBag) : base(itemBag)
        {
            IsStackable = false;
            IsClickable = false;
            Quantity = 1;
        }

        public override void StackItem(Item itemToGive)
        {
            if (!IsStackable)
            {
                Debug.Log($"{Name} cannot be stacked");
            }
        }

        public override void ShowItemInfo()
        {
            if (!IsClickable)
            {
                Debug.Log($"{Name} cannot be clicked");
            }
        }
    }
}