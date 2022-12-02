using UnityEngine;


[System.Serializable]
public class ConsumableItem : Item
{
    public ConsumableItem(ItemBag itemBag) : base(itemBag)
    {
        IsStackable = true;
        IsClickable = true;
    }

    public override void StackItem(Item itemToGive)
    {
        Debug.Log($"{nameof(Name)} : {Name} previous Quantity: {Quantity}");
        Quantity += itemToGive.Quantity;
        Debug.Log($"{nameof(Name)} : new quantity: {Quantity}");
        InvokeOnUpdateQuantity();
    }

    public override void InteractItem()
    {
        Debug.Log($"{nameof(Name)} : {Name} previous quantity: {Quantity}");
        Quantity -= 1;
        Debug.Log($"{nameof(Name)} : {Name} consume 1 {nameof(Quantity)}");
        Debug.Log($"{nameof(Name)} : {Name} new {nameof(Quantity)} : {Quantity}");
        InvokeOnUpdateQuantity();
        if(Quantity <= 0)
            InvokeOnRemoveItem();
    }
}