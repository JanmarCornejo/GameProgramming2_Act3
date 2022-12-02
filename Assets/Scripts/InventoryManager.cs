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

    //Sample Item drops 
    [SerializeField] private ItemType[] _itemDrops;

    [SerializeField] private InventoryGrid _grid;
    [SerializeField] private Canvas _sceneCanvas;

    private void Awake()
    {
        Instance = this;

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

    // Start is called before the first frame update
    void Start()
    {
        DebugInitialSampleItems();
    }

    // Update is called once per frame
    void Update()
    {
        //Debugging
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DebugStackItem();
        }
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
    private Item CreateItem(ItemType type)
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

    public bool AddItemToList(Item item)
    {
        var consumeItem = FindConsumeItem(item.Type);
        if (consumeItem != null)
        {
            consumeItem.StackItem(item);
            return false;
        }

        _itemList.Add(item);
        return true;
    }

    private void RemoveItem(Item item)
    {
        //TODO action event
        _itemList.Remove(item);
        _itemSlots--;
    }

    //Below are debugging functions
    private void DebugInitialSampleItems()
    {
        foreach (var type in _itemDrops)
        {
            var item = CreateItem(type);
            //TODO check if inventory is full
            _itemList.Add(item);
            Debug.Log($"{nameof(item.Name)}:{item.Name} " +
                      $"{nameof(item.Type)}:{item.Type} " +
                      $"{nameof(item.Kind)}:{item.Kind} " +
                      $"{nameof(item.Quantity)}:{item.Quantity} " +
                      $"{nameof(item.Description)}:{item.Description} " +
                      $"{nameof(item.IsStackable)}:{item.IsStackable} " +
                      $"{nameof(item.IsClickable)}:{item.IsClickable} ");
        }
    }

    private void DebugStackItem()
    {
        var potionBag = GetItemInfo(ItemType.HealingPotion);
        var anotherHealingPotion = new ConsumableItem(potionBag);
        var healingPotion = FindConsumeItem(anotherHealingPotion.Type);
        healingPotion.StackItem(anotherHealingPotion);
    }
}