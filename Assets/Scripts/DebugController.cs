using System;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    [SerializeField] private ItemType _itemDrop;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InventoryManager.Instance.CreateItem(_itemDrop);
        }
    }
}