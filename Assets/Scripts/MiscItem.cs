using UnityEngine;


[System.Serializable]
public class MiscItem : Item
{
    public MiscItem(ItemBag itemBag) : base(itemBag)
    {
        IsStackable = false;
        IsClickable = false;
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
        if (!IsClickable)
        {
            Debug.Log($"{Name} cannot be clicked");
        }
    }
}