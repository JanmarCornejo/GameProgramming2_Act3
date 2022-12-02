using UnityEngine;


[System.Serializable]
public class EquipmentItem : Item
{
    public EquipmentItem(ItemBag itemBag) : base(itemBag)
    {
        IsStackable = false;
        IsClickable = true;
    }

    public override void StackItem(Item itemToGive)
    {
        if (!IsStackable)
        {
            Debug.Log($"{Name} cannot be stacked");
        }
    }

    public override void InteractItem()
    {
        Debug.Log($"{nameof(Name)} : {Name} " +
                  $"{nameof(Description)} : {Description}");
    }
}