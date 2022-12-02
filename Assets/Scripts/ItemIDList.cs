using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemIDList : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textUI;
    
    // Start is called before the first frame update
    void Start()
    {
        var itemIDArray = Enum.GetValues(typeof(ItemType));
        foreach (var item in itemIDArray)
        {
            if(item.ToString() == ItemType.Unassigned.ToString())
                continue;
            var msg = $"{item} / {(int)item}";
            Debug.Log(msg);
            var ui = Instantiate(_textUI, this.transform, false);
            ui.text = msg;
        }
    }
    
}
