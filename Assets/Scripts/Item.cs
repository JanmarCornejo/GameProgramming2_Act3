using UnityEngine;

namespace GP2.Inventory
{
    public abstract class Item : IItemHandler
    {
        protected Item(ItemBag itemBag)
        {
            Name = itemBag.ItemName;
            Type = itemBag.Type;
            Kind = itemBag.Kind;
            Description = itemBag.Description;
        }

        public string Name { get; protected set; }
        public ItemType Type { get; protected set; }
        public ItemKind Kind { get; protected set; }
        public int Quantity { get; protected set; }
        public string Description { get; protected set; }
        public bool IsStackable { get; protected set; }
        public bool IsClickable { get; protected set; }
        
        /// <summary>
        /// To implement stacking quantity of item
        /// </summary>
        /// <param name="itemToGive"></param>
        public abstract void StackItem(Item itemToGive);
        
        /// <summary>
        /// To implement display item information e.g. Name & Description 
        /// </summary>
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
        void StackItem(Item itemToGive);
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