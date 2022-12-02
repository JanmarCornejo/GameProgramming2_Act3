using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropPanel : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject itemToDrop = eventData.pointerDrag;
        var panel = InventoryManager.Instance.CreateDropConfirmPanel();
        panel.Initialize(() => ProceedToDrop(itemToDrop));
    }

    private void ProceedToDrop(GameObject dropItem)
    {
        DraggableItem item = dropItem.GetComponent<DraggableItem>();
        item.DropItem();
    }
}
