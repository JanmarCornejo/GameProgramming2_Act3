using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropConfirmationPanel : MonoBehaviour
{
    [SerializeField] private Button _yesBtn;
    [SerializeField] private Button _noBtn;

    private Action _yesEvent;

    private void OnDestroy()
    {
        _yesBtn.onClick.RemoveListener(() => _yesEvent());
        _yesBtn.onClick.RemoveListener(CloseUI);
        _noBtn.onClick.RemoveListener(CloseUI);
    }

    public void Initialize(Action yesEvent)
    {
        _yesEvent = yesEvent;
        _yesBtn.onClick.AddListener(() => _yesEvent());
        _yesBtn.onClick.AddListener(CloseUI);
        _noBtn.onClick.AddListener(CloseUI);
    }

    private void CloseUI()
    {
        Destroy(gameObject);
    }
}
