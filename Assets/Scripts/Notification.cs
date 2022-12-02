using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textUI;
    
    // Start is called before the first frame update
    void Start()
    {
        _textUI = GetComponent<TextMeshProUGUI>();
        Destroy(this.gameObject, 1.5f);
    }

    public void ShowMessage(string msg)
    {
        _textUI.text = msg;
    }
}
