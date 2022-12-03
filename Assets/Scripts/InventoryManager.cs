using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    //To store item database scriptable objects
    private string _itemInfoPath = "Items";
    private Dictionary<ItemType, ItemBag> _itemsInfoDictionary = new Dictionary<ItemType, ItemBag>();

    //To record items on runtime
    private List<Item> _itemList = new List<Item>();

    [SerializeField] private int _itemSlots = 16;
    private int _currentSlots;
    
    //UI 
    [SerializeField] private DropConfirmationPanel _dropPanelPrefab;
    [SerializeField] private Notification _notificationPrefab;
    [SerializeField] private InventoryGrid _grid;
    [SerializeField] private NotificationPanel _notificationPanel;
    [SerializeField] private Canvas _sceneCanvas;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        ItemBag[] itemBags = Resources.LoadAll<ItemBag>(_itemInfoPath);
        foreach (var itemBag in itemBags)
        {
            _itemsInfoDictionary[itemBag.Type] = itemBag;
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    /// <summary>
    /// Finding consumable items with same type
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private Item FindConsumeItem(ItemType type)
    {
        return _itemList.Where(item => item.Kind == ItemKind.Consumable).FirstOrDefault(item => item.Type == type);
    }

    /// <summary>
    /// Creating new items based on the item type
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Item CreateItem(ItemType type)
    {
        ItemBag itemBag = GetItemInfo(type);
        switch (itemBag.Kind)
        {
            case ItemKind.Equipment:
                EquipmentItem equipItem = new EquipmentItem(itemBag);
                return equipItem;
            case ItemKind.Miscellaneous:
                MiscItem miscItem = new MiscItem(itemBag);
                return miscItem;
            case ItemKind.Consumable:
                ConsumableItem consumableItem = new ConsumableItem(itemBag);
                return consumableItem;
            case ItemKind.Unassigned:
                throw new Exception($"{type} is still unassigned");
        }
        return null;
    }

    public Canvas GetSceneCanvas() => _sceneCanvas;
    public InventoryGrid GetInventoryGrid() => _grid;
    private ItemBag GetItemInfo(ItemType type) => _itemsInfoDictionary[type];

    /// <summary>
    /// Add Item to list
    /// </summary>
    /// <param name="item">List from scriptable objects</param>
    /// <returns></returns>
    public bool AddItemToList(Item item)
    {
        if (_currentSlots >= _itemSlots)
        {
            ShowNotification($"Inventory is Full");
            return false;
        }

        var consumeItem = FindConsumeItem(item.Type);
        if (consumeItem != null)
        {
            consumeItem.StackItem(item);
            return false;
        }

        _currentSlots++;
        _itemList.Add(item);
        item.OnRemoveItem += RemoveItem;
        return true;
    }

    private void RemoveItem(Item item)
    {
        _itemList.Remove(item);
        _currentSlots--;
    }

    /// <summary>
    /// Show Notification Popup 
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Notification ShowNotification(string msg)
    {
       var notif = Instantiate(_notificationPrefab, _sceneCanvas.transform, false);
       _notificationPanel.AddNotification(notif, msg);
       return notif;
    }

    public DropConfirmationPanel CreateDropConfirmPanel() =>
        Instantiate(_dropPanelPrefab, _sceneCanvas.transform, false);
}