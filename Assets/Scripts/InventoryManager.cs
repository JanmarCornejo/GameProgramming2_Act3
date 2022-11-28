using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GP2.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        private List<Item> _itemList = new List<Item>();
        [SerializeField] private string ItemInfoPath = "Items";
        private Dictionary<ItemType, ItemBag> _itemsInfoDictionary = new Dictionary<ItemType, ItemBag>();
        
        [SerializeField] private ItemType[] ItemDrops;
        
        private void Awake()
        {
            ItemBag[] itemBags = Resources.LoadAll<ItemBag>(ItemInfoPath);
            foreach (var itemBag in itemBags)
            {
                _itemsInfoDictionary[itemBag.Type] = itemBag;
            }
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

        private Item FindStackableItem(ItemType type)
        {
            return _itemList.Where(item => item.Kind == ItemKind.Consumable).
                FirstOrDefault(item => item.Type == type);
        }

        private Item CheckItemKind(ItemType type)
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

        private ItemBag GetItemInfo(ItemType type)
        {
            return _itemsInfoDictionary[type];
        }
        
        //Debugging functions
        private void DebugInitialSampleItems()
        {
            foreach (var type in ItemDrops)
            {
                var item = CheckItemKind(type);
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
            var healingPotion = FindStackableItem(anotherHealingPotion.Type);
            healingPotion.StackItem(anotherHealingPotion);
        }
    }
}
