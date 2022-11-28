using UnityEngine;

namespace GP2.Inventory
{
    public abstract class Item : IItemHandler
    {
        protected Item(ItemBag itemBag)
        {
        }

        public abstract string Name { get; protected set; }
        public abstract ItemType Type { get; protected set; }
        public abstract ItemKind Kind { get; protected set; }
        public abstract int Quantity { get; protected set; }
        public abstract string Description { get; protected set; }
        public abstract bool IsStackable { get; protected set; }
        public abstract bool IsClickable { get; protected set; }
        public abstract void StackItem(Item item);
        public abstract void ShowItemInfo();
    }

    public interface IItemHandler
    {
        string Name { get; }
        ItemType Type { get; }
        ItemKind Kind { get; }
        int Quantity { get; }
        string Description { get; }
        bool IsStackable { get; }
        bool IsClickable { get; }
        void StackItem(Item item);
        void ShowItemInfo();
    }
    
    public enum ItemKind
    {
        Unassigned,
        Equipment,
        Miscellaneous,
        Consumable,
    }

    
    public enum ItemType
    {
        Unassigned = 0,
        //Equipment
        Sword = 1,
        Breastplate,
        
        //Misc
        Stone = 300,
        
        //Consumables
        HealingPotion = 600,
        ManaPotion,
    }
}