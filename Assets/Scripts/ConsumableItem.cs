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
        Quantity += itemToGive.Quantity;
        var msg = $"{nameof(Name)}: {Name} \n {nameof(Description)}: {Description} \n New {nameof(Quantity)}: {Quantity}";
        Debug.Log(msg);
        InventoryManager.Instance.ShowNotification(msg);
        InvokeOnUpdateQuantity();
    }

    public override void InteractItem()
    {
        Quantity -= 1;
        var msg = $"{nameof(Name)}: {Name} \n {nameof(Description)}: {Description} \n {nameof(Quantity)}: {Quantity}";
        Debug.Log(msg);
        InventoryManager.Instance.ShowNotification(msg);
        InvokeOnUpdateQuantity();
        if(Quantity <= 0)
            InvokeOnRemoveItem();
    }
}