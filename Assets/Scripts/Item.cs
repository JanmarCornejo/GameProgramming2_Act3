using System;
using UnityEngine;


public abstract class Item : IItemHandler
{
    public event Action<Item> OnRemoveItem;
    public event Action<Item> OnUpdateQuantity; 

    protected Item(ItemBag itemBag)
    {
        Name = itemBag.ItemName;
        Type = itemBag.Type;
        Kind = itemBag.Kind;
        Description = itemBag.Description;
        Quantity = itemBag.Quantity;
        Sprite = itemBag.Sprite;
        var dragItemUI = itemBag.DraggableItem;
        bool addedToList = InventoryManager.Instance.AddItemToList(this);
        dragItemUI.Initialize(this, addedToList);
    }

    public string Name { get; protected set; }
    public ItemType Type { get; protected set; }
    public ItemKind Kind { get; protected set; }
    public Sprite Sprite { get; protected set; }
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
    public abstract void InteractItem();
    
    public void InvokeOnRemoveItem()
    {
        OnRemoveItem?.Invoke(this);
    }

    protected void InvokeOnUpdateQuantity()
    {
        OnUpdateQuantity?.Invoke(this);
    }
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
    void InteractItem();
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