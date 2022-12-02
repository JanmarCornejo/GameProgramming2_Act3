using System.Linq;
using UnityEngine;

public class InventoryGrid : MonoBehaviour
{
    [SerializeField] private InventorySlot[] _slots;

    public InventorySlot GetSlot()
    {
        return _slots.FirstOrDefault(s => !s.SlotIsFull);
    }
}