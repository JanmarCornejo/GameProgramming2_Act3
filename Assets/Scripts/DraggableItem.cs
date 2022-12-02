using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerExitHandler
{
    private Item _item;
    private bool _addedToList;
    private int _pointClicks = 0;
    
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _quantityUI;
    private InventorySlot _parentAfterDrag;
    //[SerializeField] private Transform _parentAfterDrag;
    public Transform GetParent() => _parentAfterDrag.transform;
    public void SetParent(InventorySlot slot) => _parentAfterDrag = slot;

    public void Initialize(Item item, bool addedToList)
    {
        var canvas = InventoryManager.Instance.GetSceneCanvas();
        var dragItem = Instantiate(this, canvas.transform, false);
        dragItem._item = item;
        dragItem._addedToList = addedToList;
    }
    
    private void Start()
    {
        if (!_addedToList)
        {
            Destroy(gameObject);
            return;
        }
        
        //TODO check if inventory is full
        var slot = InventoryManager.Instance.GetInventoryGrid().GetSlot();
        this.transform.SetParent(slot.transform,false);

        _parentAfterDrag = this.GetComponentInParent<InventorySlot>();
        _quantityUI = this.GetComponentInChildren<TextMeshProUGUI>();
        OnUpdateQuantityUI(_item);

        _image = this.GetComponent<Image>();
        _image.sprite = _item.Sprite;

        _item.OnUpdateQuantity += OnUpdateQuantityUI;
        _item.OnRemoveItem += OnRemoveItem;
    }

    public void DropItem()
    {
        _item.InvokeOnRemoveItem();
    }

    private void OnRemoveItem(Item item)
    {
        _item.OnUpdateQuantity -= OnUpdateQuantityUI;
        _item.OnRemoveItem -= OnRemoveItem;
        Destroy(this.gameObject);
    }

    private void OnUpdateQuantityUI(Item item)
    {
        _quantityUI.text = item.Quantity.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // _parentAfterDrag.transform.parent = transform.parent;
        _parentAfterDrag.transform.SetParent(transform.parent, false);
        transform.SetParent(transform.root);
        // transform.SetAsLastSibling();
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_parentAfterDrag.transform);
        _image.raycastTarget = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_item.IsClickable) return;

        _pointClicks++;
        if (_pointClicks >= 2)
        {
            _item.InteractItem();
            ResetPointClicks();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetPointClicks();
    }

    private void ResetPointClicks() => _pointClicks = 0;
}