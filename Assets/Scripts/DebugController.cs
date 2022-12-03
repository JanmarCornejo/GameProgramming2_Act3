using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _createBtn;

    private void Awake()
    {
        _createBtn.onClick.AddListener(() => DebugCreateItem(_inputField.text));
    }

    private void OnDestroy()
    {
        _createBtn.onClick.RemoveListener(() => DebugCreateItem(_inputField.text));
    }

    private void DebugCreateItem(string input)
    {
        input = input.Replace(" ", string.Empty);
        if (Enum.TryParse(input, true, out ItemType type))
        {
            InventoryManager.Instance.CreateItem(type);
        }
        else
        {
            InventoryManager.Instance.ShowNotification("Please enter valid Item ID");
        }
        _inputField.text = string.Empty;
    }

    [SerializeField] private ItemType _itemDrop;

    private void Update()
    {
        // Debug only
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InventoryManager.Instance.CreateItem(_itemDrop);
        }
    }
}